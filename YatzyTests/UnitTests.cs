using System;
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
        
        // Selected dice can be rerolled
        [Fact]
        public void ReRoll_ChangesFaceValueOfSelectedDice()
        {
            DiceBunch diceBunch = new DiceBunch();
            var initialDiceValues = diceBunch.GetDiceValues();
            var diceToReRoll = new[] {1, 3, 5};
            diceBunch.ReRoll(diceToReRoll);
            var reRolledDiceValues = diceBunch.GetDiceValues();
            Assert.NotEqual(initialDiceValues, reRolledDiceValues);
        }
        
        
        [Fact]
        public void ReRoll_DoesNotChangeNonSelectedDiceValues()
        {
            // Arrange
            DiceBunch diceBunch = new DiceBunch();
            var initialDiceValues = diceBunch.GetDiceValues();
            var diceToReRoll = new[] {4, 5};
            // Act
            diceBunch.ReRoll(diceToReRoll);
            var reRolledDiceValues = diceBunch.GetDiceValues();
            // Assert
            for (int i = 0; i < 3; i++)
            {
                Assert.Equal(initialDiceValues[i], reRolledDiceValues[i]);
            }
        }

        // Dice can be rolled / re rolled thrice
        [Fact]
        public void Rolls_ForANewDiceBunch_Returns1()
        {
            // Arrange
            DiceBunch diceBunch = new DiceBunch();
            Assert.Equal(1, diceBunch.Rolls);
        }
        
        [Fact]
        public void Rolls_After1ReRoll_Returns2()
        {
            // Arrange
            DiceBunch diceBunch = new DiceBunch();
            diceBunch.ReRoll(new int[]{1});
            Assert.Equal(2, diceBunch.Rolls);
        }
        
        // Maximum Rolls should be handled by Engine?
        [Fact]
        public void MoreThan2ReRolls_ThrowsException()
        {
            DiceBunch bunch = new DiceBunch();
            bunch.ReRoll(new int[]{1});
            bunch.ReRoll(new int[]{1});
            Assert.Throws<InvalidOperationException>(() => bunch.ReRoll(new int[]{1}));
        }
        
        // Split into 2 tests
        [Fact]
        public void CanBeRolled_Before2ReRolls_ReturnsTrue()
        {
            DiceBunch bunch = new DiceBunch();
            Assert.True(bunch.CanBeRolled());
            bunch.ReRoll(new int[]{1});
            Assert.True(bunch.CanBeRolled());
        }
        
        [Fact]
        public void CanBeRolled_After2ReRolls_ReturnsFalse()
        {
            DiceBunch bunch = new DiceBunch();
            bunch.ReRoll(new int[]{1});
            bunch.ReRoll(new int[]{1});
            Assert.False(bunch.CanBeRolled());
        }
    }
}