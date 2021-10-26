using Xunit;
using Yatzy.InputOutput;
using Yatzy.Player;
using YatzyTests.UtilityClasses;

namespace YatzyTests.Player_Tests
{
    public class PlayerTests
    {
        private static readonly IInteractor HumanResponder = new HumanInteractor(new ConsoleReader(), new ConsoleDisplay());
        [Fact]
        public void GetScore_ForNewYatzyPlayer_Returns0()
        {
            var scoreCard = SmallSCoreCard.GetOnesTwosThreesScoreCard();
            var player = new YatzyPlayer("Bhuvana", scoreCard, HumanResponder);
            Assert.Equal(0, player.Score);
        }
    }
}