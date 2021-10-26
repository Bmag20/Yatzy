using Xunit;
using Yatzy.InputOutput;
using Yatzy.Player;

namespace YatzyTests.Player_Tests
{
    public class PlayerTests
    {
        private static readonly IInteractor HumanResponder = new HumanInteractor(new ConsoleReader(), new ConsoleDisplay());
        [Fact]
        public void GetScore_Initially_Returns0()
        {
            YatzyPlayer player = new YatzyPlayer("Bhuvana", new ScoreCard(), HumanResponder);
            Assert.Equal(0, player.Score);
        }
    }
}