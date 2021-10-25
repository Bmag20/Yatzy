using Xunit;
using Yatzy.Player;

namespace YatzyTests.Player_Tests
{
    public class PlayerTests
    {
        [Fact]
        public void GetScore_Initially_Returns0()
        {
            YatzyPlayer player = new YatzyPlayer("Bhuvana", new ScoreCard());
            Assert.Equal(0, player.Score);
        }
    }
}