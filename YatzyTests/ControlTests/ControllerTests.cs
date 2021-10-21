using Moq;
using Xunit;
using Yatzy.Control;
using Yatzy.InputOutput;
using Yatzy.Player;
using YatzyTests.UtilityClasses;

namespace YatzyTests.ControlTests
{
    public class ControllerTests
    {
        // Dice are displayed at the beginning of each turn

        private static readonly IScoreCard TestScoreCard = new OnesTwosThreesScoreCard();
        private static readonly Player Bhuvana = new("Bhuvana", TestScoreCard);

        private static IInputHandler InputMockWithGameExitSequence()
        {
            Mock<IInputHandler> inputMock = new Mock<IInputHandler>();
            inputMock.SetupSequence(i => i.GetPlayerInput()).Returns("n").Returns("q");
            inputMock.Setup(i => i.GetNumericInput()).Returns(1);
            return inputMock.Object;
        }

        private static GameEngine GetNewGameEngine()
        {
            IScoreCard testScoreCard = new OnesTwosThreesScoreCard();
            Player bhuvana = new Player("Bhuvana", testScoreCard);
            return new GameEngine(bhuvana);
        }

        [Fact]
        public void ConductGame_DisplaysDiceValues_AtTheBeginningOfTheTurn()
        {
            //Arrange
            var outputMock = new Mock<IOutputHandler>();
            Controller controller = new Controller(GetNewGameEngine(), InputMockWithGameExitSequence(), outputMock.Object);
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
            Controller controller = new Controller(GetNewGameEngine(), InputMockWithGameExitSequence(), outputMock.Object);
            //Act
            controller.ConductGame();
            //Assert
            var expectedMessage = GameInstructions.ReRollPrompt();
            outputMock.Verify(o => o.Display(expectedMessage), Times.AtLeastOnce);
        }

        // Player inputs q to quit the game 
        [Fact]
        public void ConductGame_PlayerInputsQ_GameEndedMessageIsDisplayed()
        {
            //Arrange
            var outputMock = new Mock<IOutputHandler>();
            Controller controller = new Controller(GetNewGameEngine(), InputMockWithGameExitSequence(), outputMock.Object);
            //Act
            controller.ConductGame();
            //Assert
            var expectedMessage = GameInstructions.GameEnded();
            outputMock.Verify(o => o.Display(expectedMessage), Times.Once);
        }

        [Fact]
        public void ConductGame_TakeDiceNumbersToReRoll_ReRollsThePickedDice()
        {
            //Arrange
            var iMock = new Mock<IInputHandler>();
            // Inputs sets to Y- yes to re roll prompt, n - no to second re roll prompt, q - quit the game
            iMock.SetupSequence(i => i.GetPlayerInput()).Returns("y").Returns("n").Returns("q");
            // Inputs set to 1: Re roll 1st dice, -1: exit re roll, 1: place roll in 1st category
            iMock.SetupSequence(i => i.GetNumericInput()).Returns(1).Returns(-1).Returns(1);
            IScoreCard testScoreCard = new OnesTwosThreesScoreCard();
            var bhuvana = new Player("Bhuvana", testScoreCard);
            var gameMock = new Mock<GameEngine>(bhuvana);
            Controller controller = new Controller(gameMock.Object, iMock.Object, new YatzyDisplay());
            //Act
            controller.ConductGame();
            //Assert
            gameMock.Verify(o => o.ReRollDice(new[] {1}), Times.AtLeastOnce);
        }

        [Fact]
        public void ConductGame_ReRollPromptIsCalledMaximumTwicePerTurn()
        {
            //Arrange
            Mock<IInputHandler> iMock = new Mock<IInputHandler>();
            // Inputs sets to Y- yes to re roll prompt, y - to second re roll prompt, q - quit the game
            iMock.SetupSequence(i => i.GetPlayerInput()).Returns("y").Returns("y").Returns("q");
            // Inputs set to -1: to exit re roll, -1: exit re roll, 1: place roll in 1st category
            iMock.SetupSequence(i => i.GetNumericInput()).Returns(-1).Returns(-1).Returns(1);
            Mock<IOutputHandler> oMock = new Mock<IOutputHandler>();
            Controller controller = new Controller(GetNewGameEngine(), iMock.Object, oMock.Object);
            //Act
            controller.ConductGame();
            //Assert
            oMock.Verify(o => o.Display(GameInstructions.ReRollPrompt()), Times.AtMost(2));
        }

        [Fact]
        public void ConductGame_UserSelectsValidCategory_LockCategoryIsCalledWithSelectedCategory()
        {
            //Arrange
            var gameMock = new Mock<GameEngine>(Bhuvana);
            Controller controller = new Controller(gameMock.Object, InputMockWithGameExitSequence(), new YatzyDisplay());
            //Act
            controller.ConductGame();
            //Assert
            gameMock.Verify(o => o.LockCategory(Bhuvana.ScoreCard.Categories[0]), Times.Once);
        }

        [Fact]
        public void ConductGame_UserSelectsInValidCategory_InValidCategoryPromptIsDisplayed()
        {
            //Arrange
            Mock<IInputHandler> iMock = new Mock<IInputHandler>();
            // Inputs sets to n- no to re roll prompt, q - quit the game
            iMock.SetupSequence(i => i.GetPlayerInput()).Returns("n").Returns("q");
            // Inputs set to -1: invalid category, 1: Valid category after re enter prompt
            iMock.SetupSequence(i => i.GetNumericInput()).Returns(-1).Returns(1);
            Mock<IOutputHandler> oMock = new Mock<IOutputHandler>();
            Controller controller = new Controller(GetNewGameEngine(), iMock.Object, oMock.Object);
            //Act
            controller.ConductGame();
            //Assert
            oMock.Verify(o => o.Display(GameInstructions.InValidCategory()), Times.Once);
        }

        [Fact]
        public void ConductGame_RollPlacedInAllCategories_EndsTheGame()
        {
            //Arrange
            var oMock = new Mock<IOutputHandler>();
            IScoreCard testScoreCard = new OnesTwosThreesScoreCard();
            foreach (var category in testScoreCard.Categories)
            {
                category.Played = true;
            }
            Player bhuvana = new Player("Bhuvana", testScoreCard);
            GameEngine gameEngine = new GameEngine(bhuvana);
            Controller controller = new Controller(gameEngine, InputMockWithGameExitSequence(), oMock.Object);
            //Act
            controller.ConductGame();
            //Assert
            var expectedMessage = GameInstructions.GameEnded();
            oMock.Verify(o => o.Display(expectedMessage), Times.Once);
        }
    }
}