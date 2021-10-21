using Xunit;
using Yatzy.CategoryStrategy;
using Yatzy.Control;
using Yatzy.Player;
using YatzyTests.UtilityClasses;

namespace YatzyTests.ControlTests
{
    public class GameEngineTests
    {
        
        [Fact]
        public void CalculateUnPlayedCategoryScores_UpdatesScoreForAllCategories()
        {
            //Arrange
            IScoreCard testScoreCard = new OnesTwosThreesScoreCard();
            GameEngine gameEngine = new GameEngine(new Player("Bhuvana", testScoreCard));
            gameEngine.YatzyTurn.RollDice();
            //Act
            gameEngine.CalculateUnPlayedCategoryScores();
            Assert.Equal(new OnesCategory().Score(gameEngine.YatzyTurn.GetDiceValues()),
                testScoreCard.Categories[0].Score);
            Assert.Equal(new TwosCategory().Score(gameEngine.YatzyTurn.GetDiceValues()),
                testScoreCard.Categories[1].Score);
            Assert.Equal(new ThreesCategory().Score(gameEngine.YatzyTurn.GetDiceValues()),
                testScoreCard.Categories[2].Score);
        }

        [Fact]
        public void CalculateUnPlayedCategoryScores_PlayedSetToTrue_DoesNotUpdateScore()
        {
            //Arrange
            IScoreCard testScoreCard = new OnesTwosThreesScoreCard();
            testScoreCard.Categories[0].Played = true;
            var initialScore = testScoreCard.Categories[0].Score;
            GameEngine gameEngine = new GameEngine(new Player("Bhuvana", testScoreCard));
            gameEngine.YatzyTurn.RollDice();
            //Act
            gameEngine.CalculateUnPlayedCategoryScores();
            Assert.Equal(initialScore, testScoreCard.Categories[0].Score);
        }

        [Fact]
        public void CalculateUnPlayedCategoryScores_PlayedSetToTrue_UpdatesScoreForUnSetCategories()
        {
            //Arrange
            IScoreCard testScoreCard = new OnesTwosThreesScoreCard();
            testScoreCard.Categories[0].Played = true;
            GameEngine gameEngine = new GameEngine(new Player("Bhuvana", testScoreCard));
            gameEngine.YatzyTurn.RollDice();
            //Act
            gameEngine.CalculateUnPlayedCategoryScores();
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
            GameEngine gameEngine = new GameEngine(new Player("Bhuvana", testScoreCard));
            //Act
            gameEngine.LockCategory(testScoreCard.Categories[0]);
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
            Player player = new Player("Bhuvana", testScoreCard);
            GameEngine gameEngine = new GameEngine(player);
            //Act
            gameEngine.LockCategory(onesCategory);
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
            Player player = new Player("Bhuvana", testScoreCard);
            GameEngine gameEngine = new GameEngine(player);
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
            Player player = new Player("Bhuvana", testScoreCard);
            GameEngine gameEngine = new GameEngine(player);
            //Act
            var gameEnded = gameEngine.IsGameEnded();
            //Assert
            Assert.True(gameEnded);
        }
    }
}