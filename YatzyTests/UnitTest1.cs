using Xunit;
using Yatzy;

namespace YatzyTests
{
    public class UnitTest1
    {
        
        [Fact]
        public void  Chance_ReturnsSumOfAllDice()
        {
            var testRoll = new[]{1, 2, 3, 4, 5}; // not dice 
            var expectedScore = 15;
            Assert.Equal(expectedScore, Scorer.Chance(testRoll));
        }

        [Fact]
        public void YatzyWithAllDiceSameValue_Returns50()
        {
            var testRoll = new[]{1, 1, 1, 1, 1};
            Assert.Equal(50, Scorer.Yatzy(testRoll));
        }
        
        [Fact]
        public void YatzyWithDifferentValuedDice_Returns0()
        {
            var testRoll = new[]{1, 1, 1, 1, 5};
            Assert.Equal(0, Scorer.Yatzy(testRoll));
        }
        
        [Theory]
        [InlineData(1, new[]{1, 2, 1, 1, 5}, 3)]
        [InlineData(3, new[]{1, 2, 1, 1, 5}, 0)]
        [InlineData(6, new[]{1, 2, 6, 1, 6}, 12)]
        public void SumOfFaceInDice_ReturnsSumOfDiceWithGivenFaceValue(int faceValue, int[] testRoll, int expectedSum)
        {
            Assert.Equal(expectedSum, Scorer.SumOfFaceInDice(faceValue, testRoll));
        }
        
        [Theory]
        [InlineData(new[]{1, 2, 1, 1, 5}, 2)]
        [InlineData(new[]{1, 2, 6, 1, 6}, 12)]
        public void Pair_ReturnsSumOf2HighestMatchingDice(int[] testRoll, int expectedSum)
        {
            Assert.Equal(expectedSum, Scorer.Pair(testRoll));
        }

        [Fact]
        public void Pair_WithNoPairs_Returns0()
        {
            var testRoll = new[] {1, 2, 3, 4, 5};
            Assert.Equal(0, Scorer.Pair(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{1, 2, 1, 1, 2}, 6)]
        [InlineData(new[]{1, 2, 6, 1, 6}, 14)]
        public void TwoPairs_ReturnsSumOf2Pairs(int[] testRoll, int expectedSum)
        {
            Assert.Equal(expectedSum, Scorer.TwoPairs(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{1, 2, 3, 4, 5}, 0)]
        [InlineData(new[]{1, 2, 1, 1, 1}, 0)]
        [InlineData(new[]{1, 1, 2, 3, 4}, 0)]
        public void TwoPairs_Without2Pairs_Returns0(int[] testRoll, int expectedSum)
        {
            Assert.Equal(expectedSum, Scorer.TwoPairs(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{3, 3, 3, 4, 5}, 9)]
        [InlineData(new[]{3, 3, 3, 3, 4}, 9)]
        [InlineData(new[]{3, 3, 4, 5, 6}, 0)]
        public void ThreeOfAKind_ReturnsSumOf3MatchingDice(int[] testRoll, int expectedSum)
        {
            Assert.Equal(expectedSum, Scorer.ThreeOfAKind(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{3, 3, 3, 3, 4}, 12)]
        [InlineData(new[]{3, 3, 3, 4, 5}, 0)]
        [InlineData(new[]{2, 3, 4, 5, 6}, 0)]
        public void FourOfAKind_ReturnsSumOf4MatchingDice(int[] testRoll, int expectedSum)
        {
            Assert.Equal(expectedSum, Scorer.FourOfAKind(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{3, 3, 3, 4, 5}, 9)]
        [InlineData(new[]{3, 3, 3, 3, 4}, 9)]
        [InlineData(new[]{3, 3, 4, 5, 6}, 0)]
        public void OfAKind_WithValue3_ReturnsSumOf3MatchingDice(int[] testRoll, int expectedSum)
        {
            Assert.Equal(expectedSum, Scorer.OfAKind(testRoll, 3));
        }
        
        [Theory]
        [InlineData(new[]{3, 3, 3, 3, 4}, 12)]
        [InlineData(new[]{3, 3, 3, 4, 5}, 0)]
        [InlineData(new[]{2, 3, 4, 5, 6}, 0)]
        public void OfAKind_WithValue4_ReturnsSumOf4MatchingDice(int[] testRoll, int expectedSum)
        {
            Assert.Equal(expectedSum, Scorer.OfAKind(testRoll, 4));
        }

        [Theory]
        [InlineData(new[]{2, 3, 5, 5, 4}, 30)]
        [InlineData(new[]{2, 3, 4, 5, 6}, 30)]
        [InlineData(new[]{2, 2, 4, 5, 6}, 0)]
        public void SmallStraight_With4ConsecutiveNumbers_Returns30(int[] testRoll, int expectedSum)
        {
            Assert.Equal(expectedSum, Scorer.SmallStraight(testRoll));

        }
        
        [Theory]
        [InlineData(new[]{2, 3, 4, 5, 6}, 40)]
        [InlineData(new[]{1, 2, 3, 5, 4}, 40)]
        [InlineData(new[]{2, 3, 5, 5, 4}, 0)]
        [InlineData(new[]{2, 2, 4, 5, 6}, 0)]
        public void LargeStraight_With5ConsecutiveNumbers_Returns40(int[] testRoll, int expectedSum)
        {
            Assert.Equal(expectedSum, Scorer.LargeStraight(testRoll));
        }
        
        [Theory]
        [InlineData(new[]{2, 2, 4, 4, 4}, 16)]
        [InlineData(new[]{2, 2, 5, 5, 4}, 0)]
        [InlineData(new[]{4, 4, 4, 4, 4}, 0)]
        public void FullHouse_With3OfAKind2OfAKind_ReturnsSum(int[] testRoll, int expectedSum)
        {
            Assert.Equal(expectedSum, Scorer.FullHouse(testRoll));
        }
    }
}