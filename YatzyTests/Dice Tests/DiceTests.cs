using Xunit;
using Yatzy.Dice;

namespace YatzyTests.Dice_Tests
{
    public class DiceTests{
            
    [Fact]
    public void Roll_GeneratesNonZeroFaceValue()
    {
        var die = new Die();
        die.Roll();
        var rolledFaceValue = die.FaceValue;
        Assert.NotEqual(0, rolledFaceValue);
    }
        
    [Fact]
    public void DieFaceValue_IsGreaterThan0()
    {
        var die = new Die();
        die.Roll();
        Assert.True(die.FaceValue > 0);
    }
        
    [Fact]
    public void DieFaceValue_IsLessThan7()
    {
        var die = new Die();
        die.Roll();
        Assert.True(die.FaceValue < 7);
    }
    
    }
}