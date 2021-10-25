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
            inputReaderMock.Setup(i => i.GetNumericInput()).Returns(1);
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
            inputReaderMock.Setup(i => i.GetNumericInput()).Returns(1);
            inputReaderMock.Setup(i => i.GetPlayerInput()).Returns(name);
            GameBuilder gameBuilder = new GameBuilder(inputReaderMock.Object, OutputWriter);
            var actualGame = gameBuilder.SetUpGame();
            Assert.Equal(name, actualGame.Players[0].PlayerName);
        }


        [Fact]
        public void SetUpGame_TakesPlayerName_ReturnsGameObjectWithPlayerHaving15CategoryScoreCard()
        {
            var inputReaderMock = new Mock<IInputHandler>();
            const string name = "Bhuvana";
            inputReaderMock.Setup(i => i.GetNumericInput()).Returns(1);
            inputReaderMock.Setup(i => i.GetPlayerInput()).Returns(name);
            GameBuilder gameBuilder = new GameBuilder(inputReaderMock.Object, OutputWriter);
            var actualGame = gameBuilder.SetUpGame();
            Assert.Equal(15, actualGame.Players[0].ScoreCard.Categories.Count);
        }

        // Multi player game
        [Fact]
        public void SetUpGame_TakesPlayerCount2_ReturnsGameObjectWith2Players()
        {
            var inputReaderMock = new Mock<IInputHandler>();
            var playerCount = 2;
            inputReaderMock.SetupSequence(i => i.GetNumericInput()).Returns(playerCount);
            inputReaderMock.SetupSequence(i => i.GetPlayerInput()).Returns("Randy").Returns("Brody");
            GameBuilder gameBuilder = new GameBuilder(inputReaderMock.Object, OutputWriter);
            var actualGame = gameBuilder.SetUpGame();
            Assert.Equal(playerCount, actualGame.Players.Count);
        }
        
        [Fact]
        public void SetUpGame_TakesPlayerCountGreaterThan10_PromptsForPlayerCountAgain()
        {
            var inputReaderMock = new Mock<IInputHandler>();
            inputReaderMock.SetupSequence(i => i.GetNumericInput()).Returns(11).Returns(1);
            inputReaderMock.SetupSequence(i => i.GetPlayerInput()).Returns("Randy").Returns("Brody");
            var outputReaderMock = new Mock<IOutputHandler>();
            GameBuilder gameBuilder = new GameBuilder(inputReaderMock.Object, outputReaderMock.Object);
            var actualGame = gameBuilder.SetUpGame();
            outputReaderMock.Verify(o => o.Display(GameInstructions.ReEnterNumberOfPlayers()), Times.Once);
        }
        
        [Fact]
        public void SetUpGame_TakesPlayerCount2_PromptsForPlayerNameTwice()
        {
            var inputReaderMock = new Mock<IInputHandler>();
            var playerCount = 2;
            inputReaderMock.SetupSequence(i => i.GetNumericInput()).Returns(playerCount);
            inputReaderMock.SetupSequence(i => i.GetPlayerInput()).Returns("Randy").Returns("Brody");
            var outputReaderMock = new Mock<IOutputHandler>();
            GameBuilder gameBuilder = new GameBuilder(inputReaderMock.Object, outputReaderMock.Object);
            var actualGame = gameBuilder.SetUpGame();
            outputReaderMock.Verify(o => o.Display(GameInstructions.PlayerNamePrompt()), Times.Exactly(playerCount));
        }
    }
}