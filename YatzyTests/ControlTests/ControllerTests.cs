using System.Collections.Generic;
using Moq;
using Xunit;
using Yatzy.Control;
using Yatzy.Dice;
using Yatzy.InputOutput;
using Yatzy.Player;
using YatzyTests.UtilityClasses;

namespace YatzyTests.ControlTests
{
    public class ControllerTests
    {
        private const int NumberOfDicePerRoll = 5;
        private const int MaximumRolls = 3;
        
        // Dice are displayed at the beginning of each turn

        [Fact]
        public void ConductGame_DisplaysDiceValues_AtTheBeginningOfTheTurn()
        {
            //Arrange
            var outputMock = new Mock<IOutputHandler>();
            var controller = new Controller(GetGameEngineWithExitSequence(), outputMock.Object);
            //Act
            controller.ConductGame();
            //Assert
            outputMock.Verify(o => o.DisplayDice(It.IsAny<int[]>()), Times.AtLeastOnce);
        }
        
        // Player can re roll selected dice
        
        [Fact]
        public void ConductGame_AfterDisplayingDice_PromptsPlayerIfHeWantsToReRoll()
        {
            //Arrange
            var outputMock = new Mock<IOutputHandler>();
            var controller = new Controller(GetGameEngineWithExitSequence(), outputMock.Object);
            //Act
            controller.ConductGame();
            //Assert
            outputMock.Verify(o => o.DisplayReRollPrompt(), Times.AtLeastOnce);
        }
        
        // Player inputs q to quit the game 
        [Fact]
        public void ConductGame_PlayerInputsQ_GameEndedMessageIsDisplayed()
        {
            //Arrange
            var outputMock = new Mock<IOutputHandler>();
            var controller = new Controller(GetGameEngineWithExitSequence(), outputMock.Object);
            //Act
            controller.ConductGame();
            //Assert
            outputMock.Verify(o => o.DisplayGameEnded(), Times.Once);
        }
        
        [Fact]
        public void ConductGame_PlayerInputsQ_PlayerAbandonedMessageIsDisplayed()
        {
            //Arrange
            var outputMock = new Mock<IOutputHandler>();
            var game = GetGameEngineWithExitSequence();
            var controller = new Controller(game, outputMock.Object);
            //Act
            controller.ConductGame();
            //Assert
            outputMock.Verify(o => o.PlayerAbandoned(game.Players[0]), Times.Once);
        }
        
        [Fact]
        public void ConductGame_PromptsUserToReRollTwicePerTurn()
        {
            //Arrange
            var oMock = new Mock<IOutputHandler>();
            var humanResponseMock = new Mock<IInteractor>();
            humanResponseMock.SetupSequence(r => r.PlayerWantsToQuit()).Returns(false).Returns(true);
            humanResponseMock.Setup(r => r.PlayerWantsToReRoll()).Returns(true);
            humanResponseMock.Setup(r => r.DiceIndicesToReRoll(It.IsAny<int>())).Returns(new[] {1});
            humanResponseMock.Setup(r => r.CategoryIndexToPlaceRoll(It.IsAny<int>())).Returns(1);
            var testScoreCard = SmallSCoreCard.GetOnesTwosThreesScoreCard();;
            var bhuvana = new YatzyPlayer("Bhuvana", testScoreCard, humanResponseMock.Object);
            var game = new Game(new List<IPlayer>() {bhuvana}, new YatzyTurn(NumberOfDicePerRoll, MaximumRolls));
            var controller = new Controller(game, oMock.Object);
            //Act
            controller.ConductGame();
            //Assert
            oMock.Verify(o => o.DisplayReRollPrompt(), Times.Exactly(2));
        }


        [Fact]
        public void ConductGame_UserSelectsValidCategory_SetsCategoryAsPlayed()
        {
            //Arrange
            var humanResponseMock = new Mock<IInteractor>();
            humanResponseMock.SetupSequence(r => r.PlayerWantsToQuit()).Returns(false).Returns(true);
            humanResponseMock.Setup(r => r.PlayerWantsToReRoll()).Returns(false);
            humanResponseMock.Setup(r => r.CategoryIndexToPlaceRoll(It.IsAny<int>())).Returns(0);
            var testScoreCard = SmallSCoreCard.GetOnesTwosThreesScoreCard();
            var bhuvana = new YatzyPlayer("Bhuvana", testScoreCard, humanResponseMock.Object);
            var game = new Game(new List<IPlayer>() {bhuvana}, new YatzyTurn(NumberOfDicePerRoll, MaximumRolls));
            var controller = new Controller(game, new ConsoleDisplay());
            //Act
            controller.ConductGame();
            //Assert
            Assert.True(testScoreCard.Categories[0].Played);
        }

        [Fact]
        public void ConductGame_RollPlacedInAllCategories_EndsTheGame()
        {
            //Arrange
            var oMock = new Mock<IOutputHandler>();
            var testScoreCard = SmallSCoreCard.GetOnesTwosThreesScoreCard();
            foreach (var categoryRecord in testScoreCard.Categories)
            {
                categoryRecord.Played = true;
            }
            var humanResponseMock = new Mock<IInteractor>();
            humanResponseMock.SetupSequence(r => r.PlayerWantsToQuit()).Returns(false).Returns(true);
            humanResponseMock.Setup(r => r.PlayerWantsToReRoll()).Returns(false);
            humanResponseMock.Setup(r => r.CategoryIndexToPlaceRoll(15)).Returns(1);
            IPlayer bhuvana = new YatzyPlayer("Bhuvana", testScoreCard, humanResponseMock.Object);
            var players = new List<IPlayer> {bhuvana};
            var game = new Game(players, new YatzyTurn(NumberOfDicePerRoll, MaximumRolls));
            var controller = new Controller(game, oMock.Object);
            //Act
            controller.ConductGame();
            //Assert
            oMock.Verify(o => o.DisplayGameEnded(), Times.Once);
        }

        [Fact]
        public void ConductGame_CallsDisplayWinnersAtTheEndOfGame()
        {
            //Arrange
            var outputMock = new Mock<IOutputHandler>();
            var controller = new Controller(GetGameEngineWithCompletePlaySequence(), outputMock.Object);
            //Act
            controller.ConductGame();
            //Assert
            outputMock.Verify(o => o.DisplayWinners(It.IsAny<List<IPlayer>>()), Times.AtLeastOnce);
        }
        
        private static Game GetGameEngineWithExitSequence()
        {
            var testScoreCard = SmallSCoreCard.GetOnesTwosThreesScoreCard();
            var humanResponseMock = new Mock<IInteractor>();
            humanResponseMock.SetupSequence(r => r.PlayerWantsToQuit()).Returns(false).Returns(true);
            humanResponseMock.Setup(r => r.PlayerWantsToReRoll()).Returns(false);
            humanResponseMock.Setup(r => r.CategoryIndexToPlaceRoll(15)).Returns(1);
            IPlayer bhuvana = new YatzyPlayer("Bhuvana", testScoreCard, humanResponseMock.Object);
            var players = new List<IPlayer> {bhuvana};
            return new Game(players, new YatzyTurn(NumberOfDicePerRoll, MaximumRolls));
        }
        
        private static Game GetGameEngineWithCompletePlaySequence()
        {
            var testScoreCard = SmallSCoreCard.GetOnesTwosThreesScoreCard();
            var humanResponseMock = new Mock<IInteractor>();
            humanResponseMock.Setup(r => r.PlayerWantsToQuit()).Returns(false);
            humanResponseMock.Setup(r => r.PlayerWantsToReRoll()).Returns(false);
            humanResponseMock.Setup(r => r.CategoryIndexToPlaceRoll(It.IsAny<int>())).Returns(0);
            IPlayer bhuvana = new YatzyPlayer("Bhuvana", testScoreCard, humanResponseMock.Object);
            var players = new List<IPlayer> {bhuvana};
            return new Game(players, new YatzyTurn(NumberOfDicePerRoll, MaximumRolls));
        }
    }
}