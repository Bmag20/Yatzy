using Xunit;
using Yatzy.CategoryStrategy;

namespace YatzyTests.CategoryStrategyTests
{
    public class CategoryScorerTests
    {
        private readonly CategoryScorerStrategy _categoryHandler = new();
        
        [Fact]
        public void  Chance_ReturnsSumOfAllDice()
        {
            var testRoll = new[]{1, 2, 3, 4, 5}; // not dice 
            const int expectedScore = 15;
            _categoryHandler.SetCategory(new ChanceCategory());
            Assert.Equal(expectedScore, _categoryHandler.GetScore(testRoll));
        }

        [Fact]
        public void YatzyWithAllDiceSameValue_Returns50()
        {
            var testRoll = new[]{1, 1, 1, 1, 1};
            _categoryHandler.SetCategory(new YatzyCategory());
            Assert.Equal(50, _categoryHandler.GetScore(testRoll));
        }
        
        [Fact]
        public void YatzyWithDifferentValuedDice_Returns0()
        {
            var testRoll = new[]{1, 1, 1, 1, 5};
            _categoryHandler.SetCategory(new YatzyCategory());
            Assert.Equal(0, _categoryHandler.GetScore(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{1, 2, 1, 1, 5}, 3)]
        [InlineData(new[]{5, 2, 6, 3, 6}, 0)]
        public void OnesCategory_ReturnsSumOf1s(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new OnesCategory());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{1, 2, 1, 1, 5}, 2)]
        [InlineData(new[]{5, 1, 6, 3, 6}, 0)]
        public void TwosCategory_ReturnsSumOf2s(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new TwosCategory());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{1, 2, 3, 3, 5}, 6)]
        [InlineData(new[]{5, 2, 6, 2, 6}, 0)]
        public void ThreesCategory_ReturnsSumOf3s(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new ThreesCategory());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{1, 2, 4, 4, 5}, 8)]
        [InlineData(new[]{5, 2, 6, 3, 6}, 0)]
        public void FoursCategory_ReturnsSumOf4s(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new FoursCategory());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{1, 2, 1, 1, 5}, 5)]
        [InlineData(new[]{1, 2, 6, 3, 6}, 0)]
        public void FivesCategory_ReturnsSumOf5s(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new FivesCategory());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{5, 2, 6, 3, 6}, 12)]
        [InlineData(new[]{1, 2, 1, 1, 5}, 0)]
        public void SixesCategory_ReturnsSumOf6s(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new SixesCategory());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{1, 2, 1, 1, 5}, 2)]
        [InlineData(new[]{1, 2, 6, 1, 6}, 12)]
        public void Pair_ReturnsSumOf2HighestMatchingDice(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new PairCategory());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }

        [Fact]
        public void Pair_WithNoPairs_Returns0()
        {
            var testRoll = new[] {1, 2, 3, 4, 5};
            _categoryHandler.SetCategory(new PairCategory());
            Assert.Equal(0, _categoryHandler.GetScore(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{1, 2, 1, 1, 2}, 6)]
        [InlineData(new[]{1, 2, 6, 1, 6}, 14)]
        public void TwoPairs_ReturnsSumOf2Pairs(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new TwoPairsCategory());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{1, 2, 3, 4, 5}, 0)]
        [InlineData(new[]{1, 2, 1, 1, 1}, 0)]
        [InlineData(new[]{1, 1, 2, 3, 4}, 0)]
        public void TwoPairs_Without2Pairs_Returns0(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new TwoPairsCategory());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{3, 3, 3, 4, 5}, 9)]
        [InlineData(new[]{3, 3, 3, 3, 4}, 9)]
        [InlineData(new[]{3, 3, 4, 5, 6}, 0)]
        public void ThreeOfAKind_ReturnsSumOf3MatchingDice(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new ThreeOfAKindCategory());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{3, 3, 3, 3, 4}, 12)]
        [InlineData(new[]{3, 3, 3, 4, 5}, 0)]
        [InlineData(new[]{2, 3, 4, 5, 6}, 0)]
        public void FourOfAKind_ReturnsSumOf4MatchingDice(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new FourOfAKindCategory());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }

        [Theory]
        [InlineData(new[]{2, 3, 5, 5, 4}, 30)]
        [InlineData(new[]{2, 3, 4, 5, 6}, 30)]
        [InlineData(new[]{6, 3, 4, 2, 1}, 30)]
        [InlineData(new[]{2, 2, 4, 5, 6}, 0)]
        public void SmallStraight_With4ConsecutiveNumbers_Returns30(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new SmallStraightCategory());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{2, 3, 4, 5, 6}, 40)]
        [InlineData(new[]{1, 2, 3, 5, 4}, 40)]
        [InlineData(new[]{2, 3, 5, 5, 4}, 0)]
        [InlineData(new[]{2, 2, 4, 5, 6}, 0)]
        public void LargeStraight_With5ConsecutiveNumbers_Returns40(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new LargeStraightCategory());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{2, 2, 4, 4, 4}, 16)]
        [InlineData(new[]{2, 2, 5, 5, 4}, 0)]
        [InlineData(new[]{4, 4, 4, 4, 4}, 0)]
        public void FullHouse_With3OfAKind2OfAKind_ReturnsSum(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new FullHouseCategory());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }
    }
}