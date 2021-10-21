using System;
using Xunit;
using Yatzy.Dice;

namespace YatzyTests.Dice_Tests
{
    public class YatzyRollTests
    {
        
        // Roll consists of 5 dice
        [Fact]
        public void YatzyTurn_ConsistsOf5Dice()
        {
            var yatzyTurn = new YatzyTurn();
            var expectedSize = 5;
            var actualSize = yatzyTurn.Dice.Length;
            Assert.Equal(expectedSize, actualSize);
        }
        
        // Dice in a roll have values between 1 and 6
        
        [Fact]
        public void DiceFaceValueInYatzyTurn_IsGreaterThan0()
        {
            var yatzyTurn = new YatzyTurn();
            yatzyTurn.RollDice();
            foreach (var die in yatzyTurn.Dice)
            {
                Assert.True(die.FaceValue > 0);
            }
        }
        
        [Fact]
        public void DiceFaceValueInYatzyTurn_IsLessThan7()
        {
            var yatzyTurn = new YatzyTurn();
            foreach (var die in yatzyTurn.Dice)
            {
                Assert.True(die.FaceValue < 7);
            }
        }
        
        // Selected dice can be rerolled
        [Fact]
        public void ReRoll_ChangesFaceValueOfSelectedDice()
        {
            YatzyTurn yatzyTurn = new YatzyTurn();
            var initialDiceValues = yatzyTurn.GetDiceValues();
            var diceToReRoll = new[] {1, 3, 5};
            yatzyTurn.ReRoll(diceToReRoll);
            var reRolledDiceValues = yatzyTurn.GetDiceValues();
            Assert.NotEqual(initialDiceValues, reRolledDiceValues);
        }
        
        
        [Fact]
        public void ReRoll_DoesNotChangeNonSelectedDiceValues()
        {
            // Arrange
            YatzyTurn yatzyTurn = new YatzyTurn();
            var initialDiceValues = yatzyTurn.GetDiceValues();
            var diceToReRoll = new[] {4, 5};
            // Act
            yatzyTurn.ReRoll(diceToReRoll);
            var reRolledDiceValues = yatzyTurn.GetDiceValues();
            // Assert
            for (int i = 0; i < 3; i++)
            {
                Assert.Equal(initialDiceValues[i], reRolledDiceValues[i]);
            }
        }

        // Dice can be rolled / re rolled thrice
        [Fact]
        public void Rolls_ForANewYatzyTurn_Returns1()
        {
            // Arrange
            YatzyTurn yatzyTurn = new YatzyTurn();
            yatzyTurn.RollDice();
            Assert.Equal(1, yatzyTurn.Rolls);
        }
        
        [Fact]
        public void Rolls_After1ReRoll_Returns2()
        {
            // Arrange
            YatzyTurn yatzyTurn = new YatzyTurn();
            yatzyTurn.RollDice();
            yatzyTurn.ReRoll(new int[]{1});
            Assert.Equal(2, yatzyTurn.Rolls);
        }
        
        // Maximum Rolls should be handled by Engine?
        [Fact]
        public void MoreThan2ReRolls_ThrowsException()
        {
            YatzyTurn bunch = new YatzyTurn();
            bunch.RollDice();
            bunch.ReRoll(new int[]{1});
            bunch.ReRoll(new int[]{1});
            Assert.Throws<InvalidOperationException>(() => bunch.ReRoll(new int[]{1}));
        }
        
        // Split into 2 tests
        [Fact]
        public void CanBeRolled_Before2ReRolls_ReturnsTrue()
        {
            YatzyTurn bunch = new YatzyTurn();
            Assert.True(bunch.CanBeRolled());
            bunch.ReRoll(new int[]{1});
            Assert.True(bunch.CanBeRolled());
        }
        
        [Fact]
        public void CanBeRolled_After2ReRolls_ReturnsFalse()
        {
            YatzyTurn bunch = new YatzyTurn();
            bunch.RollDice();
            bunch.ReRoll(new int[]{1});
            bunch.ReRoll(new int[]{1});
            Assert.False(bunch.CanBeRolled());
        }
    }
}