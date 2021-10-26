using System.Collections.Generic;
using Xunit;
using Yatzy.CategoryStrategy;
using Yatzy.Control;
using Yatzy.Dice;
using Yatzy.InputOutput;
using Yatzy.Player;
using YatzyTests.UtilityClasses;

namespace YatzyTests.ControlTests
{
    public class GameEngineTests
    {
        private const int NumberOfDicePerRoll = 5;
        private const int MaximumRolls = 3;
        private static readonly IInteractor HumanResponder = new HumanInteractor(new ConsoleReader(), new ConsoleDisplay());
        
        [Fact]
        public void CalculateUnPlayedCategoryScores_UpdatesScoreForAllUnPlayedCategories()
        {
            //Arrange
            ScoreCard testScoreCard = SmallSCoreCard.GetOnesTwosThreesScoreCard();
            var player = new YatzyPlayer("Bhuvana", testScoreCard, HumanResponder);
            var gameEngine = new Game(new List<IPlayer>(){player}, new YatzyTurn(NumberOfDicePerRoll, MaximumRolls));
            gameEngine.YatzyTurn.RollDice();
            //Act
            gameEngine.CalculateUnPlayedCategoryScores(player);
            //Assert
            Assert.Equal(new OnesCategory().Score(gameEngine.GetCurrentRoll()),
                testScoreCard.Categories[0].Score);
            Assert.Equal(new TwosCategory().Score(gameEngine.GetCurrentRoll()),
                testScoreCard.Categories[1].Score);
            Assert.Equal(new ThreesCategory().Score(gameEngine.GetCurrentRoll()),
                testScoreCard.Categories[2].Score);
        }

        [Fact]
        public void CalculateUnPlayedCategoryScores_PlayedSetToTrue_DoesNotUpdateScore()
        {
            //Arrange
            ScoreCard testScoreCard = SmallSCoreCard.GetOnesTwosThreesScoreCard();
            testScoreCard.Categories[0].Played = true;
            var initialScore = testScoreCard.Categories[0].Score;
            var player = new YatzyPlayer("Bhuvana", testScoreCard, HumanResponder);
            var gameEngine = new Game(new List<IPlayer>(){player}, new YatzyTurn(NumberOfDicePerRoll, MaximumRolls));
            gameEngine.YatzyTurn.RollDice();
            //Act
            gameEngine.CalculateUnPlayedCategoryScores(player);
            //Assert
            Assert.Equal(initialScore, testScoreCard.Categories[0].Score);
        }

        [Fact]
        public void CalculateUnPlayedCategoryScores_PlayedSetToTrue_UpdatesScoreForUnSetCategories()
        {
            //Arrange
            ScoreCard testScoreCard =SmallSCoreCard.GetOnesTwosThreesScoreCard();
            testScoreCard.Categories[0].Played = true;
            var player = new YatzyPlayer("Bhuvana", testScoreCard, HumanResponder);
            var gameEngine = new Game(new List<IPlayer>(){player}, new YatzyTurn(NumberOfDicePerRoll, MaximumRolls));
            gameEngine.YatzyTurn.RollDice();
            //Act
            gameEngine.CalculateUnPlayedCategoryScores(player);
            //Assert
            Assert.Equal(new TwosCategory().Score(gameEngine.YatzyTurn.GetDiceValues()),
                testScoreCard.Categories[1].Score);
            Assert.Equal(new ThreesCategory().Score(gameEngine.YatzyTurn.GetDiceValues()),
                testScoreCard.Categories[2].Score);
        }

        // locking category in the category list updates the score of the player to that score and is considered played

        [Fact]
        public void LockCategory_TakesCategory_SetsCategoryPlayedAsTrue()
        {
            //Arrange
            ScoreCard testScoreCard = SmallSCoreCard.GetOnesTwosThreesScoreCard();
            var player = new YatzyPlayer("Bhuvana", testScoreCard, HumanResponder);
            var gameEngine = new Game(new List<IPlayer>(){player}, new YatzyTurn(NumberOfDicePerRoll, MaximumRolls));
            //Act
            gameEngine.LockCategory(testScoreCard.Categories[0], player);
            //Assert
            Assert.True(testScoreCard.Categories[0].Played);
        }

        [Fact]
        public void LockCategory_TakesIndex_AddsCategoryScoreToPlayerScore()
        {
            //Arrange
            ScoreCard testScoreCard = SmallSCoreCard.GetOnesTwosThreesScoreCard();
            const int testScore = 5;
            var onesCategory = testScoreCard.Categories[0];
            onesCategory.Score = testScore;
            var player = new YatzyPlayer("Bhuvana", testScoreCard, HumanResponder);
            var players = new List<IPlayer>(){player};
            var gameEngine = new Game(players, new YatzyTurn(NumberOfDicePerRoll, MaximumRolls));
            //Act
            gameEngine.LockCategory(onesCategory, player);
            //Assert
            Assert.Equal(testScore, player.Score);
        }

        // Game ends once the player has played all categories

        [Fact]
        public void IsGameEnded_WithCategoriesUnPlayed_ReturnsFalse()
        {
            //Arrange
            ScoreCard testScoreCard =SmallSCoreCard.GetOnesTwosThreesScoreCard();
            testScoreCard.Categories[0].Played = false;
            var player = new YatzyPlayer("Bhuvana", testScoreCard, HumanResponder);
            var gameEngine = new Game(new List<IPlayer>(){player}, new YatzyTurn(NumberOfDicePerRoll, MaximumRolls));
            //Act
            var gameEnded = gameEngine.IsGameEnded();
            //Assert
            Assert.False(gameEnded);
        }

        [Fact]
        public void IsGameEnded_WithAllCategoriesPlayed_ReturnsFalse()
        {
            //Arrange
            ScoreCard testScoreCard = SmallSCoreCard.GetOnesTwosThreesScoreCard();
            testScoreCard.Categories[0].Played = true;
            testScoreCard.Categories[1].Played = true;
            testScoreCard.Categories[2].Played = true;
            var player = new YatzyPlayer("Bhuvana", testScoreCard, HumanResponder);
            var gameEngine = new Game(new List<IPlayer>(){player}, new YatzyTurn(NumberOfDicePerRoll, MaximumRolls));
            //Act
            var gameEnded = gameEngine.IsGameEnded();
            //Assert
            Assert.True(gameEnded);
        }

        [Fact]
        public void GetNumberOfDicePerRoll_ReturnsNumberOfDiceInYatzyTurn()
        {
            //Arrange
            ScoreCard testScoreCard = SmallSCoreCard.GetOnesTwosThreesScoreCard();
            var player = new YatzyPlayer("Bhuvana", testScoreCard, HumanResponder);
            var gameEngine = new Game(new List<IPlayer>(){player}, new YatzyTurn(NumberOfDicePerRoll, MaximumRolls));
            //Act
            var actualValue = gameEngine.GetNumberOfDicePerRoll();
            //Assert
            Assert.Equal(NumberOfDicePerRoll, actualValue);
        }
        
        [Fact]
        public void ReRoll_ChangesFaceValueOfSelectedDice()
        {
            //Arrange
            ScoreCard testScoreCard = SmallSCoreCard.GetOnesTwosThreesScoreCard();
            var player = new YatzyPlayer("Bhuvana", testScoreCard, HumanResponder);
            var yatzyTurn = new YatzyTurn(NumberOfDicePerRoll, MaximumRolls);
            var gameEngine = new Game(new List<IPlayer>(){player},yatzyTurn);
            gameEngine.RollDice();
            var initialDiceValues = gameEngine.GetCurrentRoll();
            var diceToReRoll = new[] {1, 3, 5};
            //Act
            gameEngine.ReRollDice(diceToReRoll);
            //Assert
            var reRolledDiceValues = gameEngine.GetCurrentRoll();
            Assert.NotEqual(initialDiceValues, reRolledDiceValues);
        }
        
        [Fact]
        public void ReRoll_DoesNotChangeNonSelectedDiceValues()
        {
            // Arrange
            ScoreCard testScoreCard = SmallSCoreCard.GetOnesTwosThreesScoreCard();
            var player = new YatzyPlayer("Bhuvana", testScoreCard, HumanResponder);
            var yatzyTurn = new YatzyTurn(NumberOfDicePerRoll, MaximumRolls);
            var gameEngine = new Game(new List<IPlayer>(){player},yatzyTurn);
            gameEngine.RollDice();
            var initialDiceValues = gameEngine.GetCurrentRoll();
            var diceToReRoll = new[] {4, 5};
            // Act
            gameEngine.ReRollDice(diceToReRoll);
            var reRolledDiceValues = gameEngine.GetCurrentRoll();
            // Assert
            for (var i = 0; i < 3; i++)
            {
                Assert.Equal(initialDiceValues[i], reRolledDiceValues[i]);
            }
        }
        
        // Multi player game
        [Fact]
        public void IsGameEnded_WithAllCategoriesPlayedForOnly1Player_ReturnsFalse()
        {
            //Arrange
            ScoreCard testScoreCard1 = SmallSCoreCard.GetOnesTwosThreesScoreCard();
            testScoreCard1.Categories[0].Played = true;
            testScoreCard1.Categories[1].Played = true;
            testScoreCard1.Categories[2].Played = true;
            var player1 = new YatzyPlayer("Bhuvana", testScoreCard1, HumanResponder);
            ScoreCard testScoreCard2 = SmallSCoreCard.GetOnesTwosThreesScoreCard();
            testScoreCard2.Categories[0].Played = true;
            testScoreCard2.Categories[1].Played = true;
            testScoreCard2.Categories[2].Played = false;
            var player2 = new YatzyPlayer("Sandy", testScoreCard2, HumanResponder);
            var gameEngine = new Game(new List<IPlayer>(){player1, player2}, new YatzyTurn(NumberOfDicePerRoll, MaximumRolls));
            //Act
            var gameEnded = gameEngine.IsGameEnded();
            //Assert
            Assert.False(gameEnded);
        }
        
        [Fact]
        public void IsGameEnded_WithAllCategoriesPlayedForAllPlayer_ReturnsTrue()
        {
            //Arrange
            ScoreCard testScoreCard1 = SmallSCoreCard.GetOnesTwosThreesScoreCard();
            testScoreCard1.Categories[0].Played = true;
            testScoreCard1.Categories[1].Played = true;
            testScoreCard1.Categories[2].Played = true;
            var player1 = new YatzyPlayer("Bhuvana", testScoreCard1, HumanResponder);
            ScoreCard testScoreCard2 = SmallSCoreCard.GetOnesTwosThreesScoreCard();
            testScoreCard2.Categories[0].Played = true;
            testScoreCard2.Categories[1].Played = true;
            testScoreCard2.Categories[2].Played = true;
            var player2 = new YatzyPlayer("Sandy", testScoreCard2, HumanResponder);
            var gameEngine = new Game(new List<IPlayer>(){player1, player2}, new YatzyTurn(NumberOfDicePerRoll, MaximumRolls));
            //Act
            var gameEnded = gameEngine.IsGameEnded();
            //Assert
            Assert.True(gameEnded);
        }
    }
}