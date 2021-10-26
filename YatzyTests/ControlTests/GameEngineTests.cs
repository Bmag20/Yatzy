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
        private static readonly IInteractor HumanResponder = new HumanInteractor(new ConsoleReader(), new ConsoleDisplay());
        
        [Fact]
        public void CalculateUnPlayedCategoryScores_UpdatesScoreForAllUnPlayedCategories()
        {
            //Arrange
            IScoreCard testScoreCard = new OnesTwosThreesScoreCard();
            var player = new YatzyPlayer("Bhuvana", testScoreCard, HumanResponder);
            Game gameEngine = new Game(new List<IPlayer>(){player}, new YatzyTurn());
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
            IScoreCard testScoreCard = new OnesTwosThreesScoreCard();
            testScoreCard.Categories[0].Played = true;
            var initialScore = testScoreCard.Categories[0].Score;
            var player = new YatzyPlayer("Bhuvana", testScoreCard, HumanResponder);
            Game gameEngine = new Game(new List<IPlayer>(){player}, new YatzyTurn());
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
            IScoreCard testScoreCard = new OnesTwosThreesScoreCard();
            testScoreCard.Categories[0].Played = true;
            var player = new YatzyPlayer("Bhuvana", testScoreCard, HumanResponder);
            Game gameEngine = new Game(new List<IPlayer>(){player}, new YatzyTurn());
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
            IScoreCard testScoreCard = new OnesTwosThreesScoreCard();
            var player = new YatzyPlayer("Bhuvana", testScoreCard, HumanResponder);
            Game gameEngine = new Game(new List<IPlayer>(){player}, new YatzyTurn());
            //Act
            gameEngine.LockCategory(testScoreCard.Categories[0], player);
            //Assert
            Assert.True(testScoreCard.Categories[0].Played);
        }

        [Fact]
        public void LockCategory_TakesIndex_AddsCategoryScoreToPlayerScore()
        {
            //Arrange
            IScoreCard testScoreCard = new OnesTwosThreesScoreCard();
            const int testScore = 5;
            var onesCategory = testScoreCard.Categories[0];
            onesCategory.Score = testScore;
            var player = new YatzyPlayer("Bhuvana", testScoreCard, HumanResponder);
            var players = new List<IPlayer>(){player};
            Game gameEngine = new Game(players, new YatzyTurn());
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
            IScoreCard testScoreCard = new OnesTwosThreesScoreCard();
            testScoreCard.Categories[0].Played = false;
            var player = new YatzyPlayer("Bhuvana", testScoreCard, HumanResponder);
            Game gameEngine = new Game(new List<IPlayer>(){player}, new YatzyTurn());
            //Act
            var gameEnded = gameEngine.IsGameEnded();
            //Assert
            Assert.False(gameEnded);
        }

        [Fact]
        public void IsGameEnded_WithAllCategoriesPlayed_ReturnsFalse()
        {
            //Arrange
            IScoreCard testScoreCard = new OnesTwosThreesScoreCard();
            testScoreCard.Categories[0].Played = true;
            testScoreCard.Categories[1].Played = true;
            testScoreCard.Categories[2].Played = true;
            var player = new YatzyPlayer("Bhuvana", testScoreCard, HumanResponder);
            Game gameEngine = new Game(new List<IPlayer>(){player}, new YatzyTurn());
            //Act
            var gameEnded = gameEngine.IsGameEnded();
            //Assert
            Assert.True(gameEnded);
        }
        
        // Multi player game
        [Fact]
        public void IsGameEnded_WithAllCategoriesPlayedForOnly1Player_ReturnsFalse()
        {
            //Arrange
            IScoreCard testScoreCard1 = new OnesTwosThreesScoreCard();
            testScoreCard1.Categories[0].Played = true;
            testScoreCard1.Categories[1].Played = true;
            testScoreCard1.Categories[2].Played = true;
            var player1 = new YatzyPlayer("Bhuvana", testScoreCard1, HumanResponder);
            IScoreCard testScoreCard2 = new OnesTwosThreesScoreCard();
            testScoreCard2.Categories[0].Played = true;
            testScoreCard2.Categories[1].Played = true;
            testScoreCard2.Categories[2].Played = false;
            var player2 = new YatzyPlayer("Sandy", testScoreCard2, HumanResponder);
            Game gameEngine = new Game(new List<IPlayer>(){player1, player2}, new YatzyTurn());
            //Act
            var gameEnded = gameEngine.IsGameEnded();
            //Assert
            Assert.False(gameEnded);
        }
        
        [Fact]
        public void IsGameEnded_WithAllCategoriesPlayedForAllPlayer_ReturnsTrue()
        {
            //Arrange
            IScoreCard testScoreCard1 = new OnesTwosThreesScoreCard();
            testScoreCard1.Categories[0].Played = true;
            testScoreCard1.Categories[1].Played = true;
            testScoreCard1.Categories[2].Played = true;
            var player1 = new YatzyPlayer("Bhuvana", testScoreCard1, HumanResponder);
            IScoreCard testScoreCard2 = new OnesTwosThreesScoreCard();
            testScoreCard2.Categories[0].Played = true;
            testScoreCard2.Categories[1].Played = true;
            testScoreCard2.Categories[2].Played = true;
            var player2 = new YatzyPlayer("Sandy", testScoreCard2, HumanResponder);
            Game gameEngine = new Game(new List<IPlayer>(){player1, player2}, new YatzyTurn());
            //Act
            var gameEnded = gameEngine.IsGameEnded();
            //Assert
            Assert.True(gameEnded);
        }
    }
}