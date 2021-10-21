using Moq;
using Xunit;
using Yatzy.Control;
using Yatzy.InputOutput;

namespace YatzyTests.ControlTests
{
    public class GameBuilderTests
    {
        private static readonly IOutputHandler OutputWriter = new YatzyDisplay();

        [Fact]
        public void SetUpGame_TakesPlayerName_ReturnsNonNullGameObject()
        {
            var inputReaderMock = new Mock<IInputHandler>();
            const string name = "Bhuvana";
            inputReaderMock.Setup(i => i.GetPlayerInput()).Returns(name);
            GameBuilder gameBuilder = new GameBuilder(inputReaderMock.Object, OutputWriter);
            var actualGame = gameBuilder.SetUpGame();
            Assert.NotNull(actualGame);
        }

        [Fact]
        public void SetUpGame_TakesPlayerName_ReturnsGameObjectWithPlayerHavingSameName()
        {
            var inputReaderMock = new Mock<IInputHandler>();
            const string name = "Bhuvana";
            inputReaderMock.Setup(i => i.GetPlayerInput()).Returns(name);
            GameBuilder gameBuilder = new GameBuilder(inputReaderMock.Object, OutputWriter);
            var actualGame = gameBuilder.SetUpGame();
            Assert.Equal(name, actualGame.Player.PlayerName);
        }


        [Fact]
        public void SetUpGame_TakesPlayerName_ReturnsGameObjectWithPlayerHaving15CategoryScoreCard()
        {
            var inputReaderMock = new Mock<IInputHandler>();
            const string name = "Bhuvana";
            inputReaderMock.Setup(i => i.GetPlayerInput()).Returns(name);
            GameBuilder gameBuilder = new GameBuilder(inputReaderMock.Object, OutputWriter);
            var actualGame = gameBuilder.SetUpGame();
            Assert.Equal(15, actualGame.Player.ScoreCard.Categories.Count);
        }

    }
}