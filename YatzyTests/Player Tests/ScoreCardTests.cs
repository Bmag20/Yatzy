using Xunit;
using Yatzy.Player;

namespace YatzyTests.Player_Tests
{
    public class ScoreCardTests
    {
        [Fact]
        public void ScoreCard_ConsistsOf15Categories()
        {
            var scoreCard = new ScoreCard();
            Assert.Equal(15, scoreCard.Categories.Count);
        }
        
        [Fact]
        public void AreAllCategoriesPlayed_ForNewScoreCard_ReturnsFalse()
        {
            var scoreCard = new ScoreCard();
            Assert.False(scoreCard.AreAllCategoriesPlayed());
        }
        
        [Fact]
        public void AreAllCategoriesPlayed_WhenAllCategoriesAreSetAsPlayed_ReturnsTrue()
        {
            var scoreCard = new ScoreCard();
            SetCategoriesAsPlayed(scoreCard, 15);
            Assert.True(scoreCard.AreAllCategoriesPlayed());
        }
        
        [Fact]
        public void AreAllCategoriesPlayed_WhenFewCategoriesAreSetAsPlayed_ReturnsFalse()
        {
            var scoreCard = new ScoreCard();
            SetCategoriesAsPlayed(scoreCard, 10);
            Assert.False(scoreCard.AreAllCategoriesPlayed());
        }

        private void SetCategoriesAsPlayed(ScoreCard scoreCard, int numberOfCategoriesToSet)
        {
            for (int i = 0; i < numberOfCategoriesToSet && i < scoreCard.Categories.Count; i++)
            {
                scoreCard.Categories[i].Played = true;
            }
        }
    }
}