using Xunit;
using Yatzy;

namespace YatzyTests
{
    public class UnitTests
    {
        // A roll consists of 5 dice
        // Dice? - create Die class
        // Face value of DIe should be between 1-6
        [Fact]
        public void DieFaceValue_IsGreaterThan0()
        {
            var die = new Die();
            Assert.True(die.FaceValue > 0);
        }
        
        [Fact]
        public void DieFaceValue_IsLessThan7()
        {
            var die = new Die();
            Assert.True(die.FaceValue < 7);
        }

        [Fact]
        public void Roll_GeneratesRandomFaceValue()
        {
            var die = new Die();
            var initialFaceValue = die.FaceValue;
            die.Roll();
            var rolledFaceValue = die.FaceValue;
            Assert.NotEqual(initialFaceValue, rolledFaceValue);
        }
        
        // Roll consists of 5 dice
        [Fact]
        public void DiceBunch_ConsistsOf5Dice()
        {
            var diceBunch = new DiceBunch();
            var expectedSize = 5;
            var actualSize = diceBunch.Dice.Length;
            Assert.Equal(expectedSize, actualSize);
        }
        
        // Dice in a roll have values between 1 and 6
        
        [Fact]
        public void DiceFaceValueInDiceBunch_IsGreaterThan0()
        {
            var diceBunch = new DiceBunch();
            foreach (var die in diceBunch.Dice)
            {
                Assert.True(die.FaceValue > 0);
            }
        }
        
        [Fact]
        public void DiceFaceValueInDiceBunch_IsLessThan7()
        {
            var diceBunch = new DiceBunch();
            foreach (var die in diceBunch.Dice)
            {
                Assert.True(die.FaceValue < 7);
            }
        }
    }
}