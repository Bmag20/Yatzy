using Xunit;
using Yatzy.Categories;

namespace YatzyTests
{
    public class UnitTest1
    {
        private readonly CategoryHandler _categoryHandler = new();
        
        [Fact]
        public void  Chance_ReturnsSumOfAllDice()
        {
            var testRoll = new[]{1, 2, 3, 4, 5}; // not dice 
            const int expectedScore = 15;
            _categoryHandler.SetCategory(new Chance());
            Assert.Equal(expectedScore, _categoryHandler.GetScore(testRoll));
        }

        [Fact]
        public void YatzyWithAllDiceSameValue_Returns50()
        {
            var testRoll = new[]{1, 1, 1, 1, 1};
            _categoryHandler.SetCategory(new Yatzy.Categories.Yatzy());
            Assert.Equal(50, _categoryHandler.GetScore(testRoll));
        }
        
        [Fact]
        public void YatzyWithDifferentValuedDice_Returns0()
        {
            var testRoll = new[]{1, 1, 1, 1, 5};
            _categoryHandler.SetCategory(new Yatzy.Categories.Yatzy());
            Assert.Equal(0, _categoryHandler.GetScore(testRoll));
        }
        
        [Theory]
        [InlineData(1, new[]{1, 2, 1, 1, 5}, 3)]
        //[InlineData(3, new[]{1, 2, 1, 1, 5}, 0)]
        //[InlineData(6, new[]{1, 2, 6, 1, 6}, 12)]
        public void SumOfFaceInDice_ReturnsSumOfDiceWithGivenFaceValue(int faceValue, int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new Ones());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{1, 2, 1, 1, 5}, 2)]
        [InlineData(new[]{1, 2, 6, 1, 6}, 12)]
        public void Pair_ReturnsSumOf2HighestMatchingDice(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new Pair());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }

        [Fact]
        public void Pair_WithNoPairs_Returns0()
        {
            var testRoll = new[] {1, 2, 3, 4, 5};
            _categoryHandler.SetCategory(new Pair());
            Assert.Equal(0, _categoryHandler.GetScore(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{1, 2, 1, 1, 2}, 6)]
        [InlineData(new[]{1, 2, 6, 1, 6}, 14)]
        public void TwoPairs_ReturnsSumOf2Pairs(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new TwoPairs());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{1, 2, 3, 4, 5}, 0)]
        [InlineData(new[]{1, 2, 1, 1, 1}, 0)]
        [InlineData(new[]{1, 1, 2, 3, 4}, 0)]
        public void TwoPairs_Without2Pairs_Returns0(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new TwoPairs());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{3, 3, 3, 4, 5}, 9)]
        [InlineData(new[]{3, 3, 3, 3, 4}, 9)]
        [InlineData(new[]{3, 3, 4, 5, 6}, 0)]
        public void ThreeOfAKind_ReturnsSumOf3MatchingDice(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new ThreeOfAKind());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{3, 3, 3, 3, 4}, 12)]
        [InlineData(new[]{3, 3, 3, 4, 5}, 0)]
        [InlineData(new[]{2, 3, 4, 5, 6}, 0)]
        public void FourOfAKind_ReturnsSumOf4MatchingDice(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new FourOfAKind());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }
        
        // Generic tests for OfAKind - Three of a kind and four of a kind
        
        // [Theory]
        // [InlineData(new[]{3, 3, 3, 4, 5}, 9)]
        // [InlineData(new[]{3, 3, 3, 3, 4}, 9)]
        // [InlineData(new[]{3, 3, 4, 5, 6}, 0)]
        // public void OfAKind_WithValue3_ReturnsSumOf3MatchingDice(int[] testRoll, int expectedSum)
        // {
        //     Assert.Equal(expectedSum, Scorer.OfAKind(testRoll, 3));
        // }
        
        // [Theory]
        // [InlineData(new[]{3, 3, 3, 3, 4}, 12)]
        // [InlineData(new[]{3, 3, 3, 4, 5}, 0)]
        // [InlineData(new[]{2, 3, 4, 5, 6}, 0)]
        // public void OfAKind_WithValue4_ReturnsSumOf4MatchingDice(int[] testRoll, int expectedSum)
        // {
        //     Assert.Equal(expectedSum, Scorer.OfAKind(testRoll, 4));
        // }

        [Theory]
        [InlineData(new[]{2, 3, 5, 5, 4}, 30)]
        [InlineData(new[]{2, 3, 4, 5, 6}, 30)]
        [InlineData(new[]{2, 2, 4, 5, 6}, 0)]
        public void SmallStraight_With4ConsecutiveNumbers_Returns30(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new SmallStraight());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));

        }
        
        [Theory]
        [InlineData(new[]{2, 3, 4, 5, 6}, 40)]
        [InlineData(new[]{1, 2, 3, 5, 4}, 40)]
        [InlineData(new[]{2, 3, 5, 5, 4}, 0)]
        [InlineData(new[]{2, 2, 4, 5, 6}, 0)]
        public void LargeStraight_With5ConsecutiveNumbers_Returns40(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new LargeStraight());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{2, 2, 4, 4, 4}, 16)]
        [InlineData(new[]{2, 2, 5, 5, 4}, 0)]
        [InlineData(new[]{4, 4, 4, 4, 4}, 0)]
        public void FullHouse_With3OfAKind2OfAKind_ReturnsSum(int[] testRoll, int expectedSum)
        {
            _categoryHandler.SetCategory(new FullHouse());
            Assert.Equal(expectedSum, _categoryHandler.GetScore(testRoll));
        }
    }
}