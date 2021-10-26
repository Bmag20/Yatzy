using System.Linq;
using Xunit;
using Yatzy.Player;
using YatzyTests.UtilityClasses;

namespace YatzyTests.Player_Tests
{
    public class ScoreCardTests
    {

        [Fact]
        public void AreAllCategoriesPlayed_ForNewScoreCard_ReturnsFalse()
        {
            var scoreCard = SmallSCoreCard.GetOnesTwosThreesScoreCard();
            Assert.False(scoreCard.AreAllCategoriesPlayed());
        }
        
        [Fact]
        public void AreAllCategoriesPlayed_WhenAllCategoriesAreSetAsPlayed_ReturnsTrue()
        {
            var scoreCard = SmallSCoreCard.GetOnesTwosThreesScoreCard();
            SetCategoriesAsPlayed(scoreCard, scoreCard.Categories.Count);
            Assert.True(scoreCard.AreAllCategoriesPlayed());
        }
        
        [Fact]
        public void AreAllCategoriesPlayed_WhenFewCategoriesAreSetAsPlayed_ReturnsFalse()
        {
            var scoreCard = SmallSCoreCard.GetOnesTwosThreesScoreCard();
            SetCategoriesAsPlayed(scoreCard, scoreCard.Categories.Count - 1);
            Assert.False(scoreCard.AreAllCategoriesPlayed());
        }

        private void SetCategoriesAsPlayed(ScoreCard scoreCard, int numberOfCategoriesToSet)
        {
            for (var i = 0; i < numberOfCategoriesToSet && i < scoreCard.Categories.Count; i++)
            {
                scoreCard.Categories[i].Played = true;
            }
        }
    }
}