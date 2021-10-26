using Moq;
using Xunit;
using Yatzy.Control;
using Yatzy.InputOutput;

namespace YatzyTests.ControlTests
{
    public class GameBuilderTests
    {
        private static readonly IOutputHandler OutputWriter = new ConsoleDisplay();

        [Fact]
        public void SetUpGame_TakesPlayerCountAndName_ReturnsNonNullGameObject()
        {
            //Arrange
            var inputReaderMock = new Mock<IInputHandler>();
            const string name = "Bhuvana";
            inputReaderMock.Setup(i => i.GetNumericInput()).Returns(1);
            inputReaderMock.Setup(i => i.GetStringInput()).Returns(name);
            GameBuilder gameBuilder = new GameBuilder(inputReaderMock.Object, OutputWriter);
            //Act
            var actualGame = gameBuilder.SetUpGame();
            //Assert
            Assert.NotNull(actualGame);
        }

        [Fact]
        public void SetUpGame_TakesPlayerName_ReturnsGameObjectWithPlayerHavingSameName()
        {
            //Arrange
            var inputReaderMock = new Mock<IInputHandler>();
            const string name = "Bhuvana";
            inputReaderMock.Setup(i => i.GetNumericInput()).Returns(1);
            inputReaderMock.Setup(i => i.GetStringInput()).Returns(name);
            GameBuilder gameBuilder = new GameBuilder(inputReaderMock.Object, OutputWriter);
            //Act
            var actualGame = gameBuilder.SetUpGame();
            //Assert
            Assert.Equal(name, actualGame.Players[0].PlayerName);
        }


        [Fact]
        public void SetUpGame_TakesPlayerName_ReturnsGameObjectWithPlayerHaving15CategoryScoreCard()
        {
            //Arrange
            var inputReaderMock = new Mock<IInputHandler>();
            const string name = "Bhuvana";
            inputReaderMock.Setup(i => i.GetNumericInput()).Returns(1);
            inputReaderMock.Setup(i => i.GetStringInput()).Returns(name);
            GameBuilder gameBuilder = new GameBuilder(inputReaderMock.Object, OutputWriter);
            //Act
            var actualGame = gameBuilder.SetUpGame();
            //Assert
            Assert.Equal(15, actualGame.Players[0].ScoreCard.Categories.Count);
        }

        // Multi player game
        [Fact]
        public void SetUpGame_TakesPlayerCount2_ReturnsGameObjectWith2Players()
        {
            //Arrange
            var inputReaderMock = new Mock<IInputHandler>();
            var playerCount = 2;
            inputReaderMock.SetupSequence(i => i.GetNumericInput()).Returns(playerCount);
            inputReaderMock.SetupSequence(i => i.GetStringInput()).Returns("Randy").Returns("Brody");
            GameBuilder gameBuilder = new GameBuilder(inputReaderMock.Object, OutputWriter);
            //Act
            var actualGame = gameBuilder.SetUpGame();
            //Assert
            Assert.Equal(playerCount, actualGame.Players.Count);
        }
        
        [Fact]
        public void SetUpGame_TakesPlayerCountGreaterThan10_PromptsForPlayerCountAgain()
        {
            //Arrange
            var inputReaderMock = new Mock<IInputHandler>();
            inputReaderMock.SetupSequence(i => i.GetNumericInput()).Returns(11).Returns(1);
            inputReaderMock.SetupSequence(i => i.GetStringInput()).Returns("Randy").Returns("Brody");
            var outputReaderMock = new Mock<IOutputHandler>();
            GameBuilder gameBuilder = new GameBuilder(inputReaderMock.Object, outputReaderMock.Object);
            //Act
            var actualGame = gameBuilder.SetUpGame();
            //Assert
            outputReaderMock.Verify(o => o.Display(GameInstructions.ReEnterNumberOfPlayers()), Times.Once);
        }
        
        [Fact]
        public void SetUpGame_TakesPlayerCount2_PromptsForPlayerNameTwice()
        {
            //Arrange
            var inputReaderMock = new Mock<IInputHandler>();
            var playerCount = 2;
            inputReaderMock.SetupSequence(i => i.GetNumericInput()).Returns(playerCount);
            inputReaderMock.SetupSequence(i => i.GetStringInput()).Returns("Randy").Returns("Brody");
            var outputReaderMock = new Mock<IOutputHandler>();
            GameBuilder gameBuilder = new GameBuilder(inputReaderMock.Object, outputReaderMock.Object);
            //Act
            var actualGame = gameBuilder.SetUpGame();
            //Assert
            outputReaderMock.Verify(o => o.Display(GameInstructions.PlayerNamePrompt()), Times.Exactly(playerCount));
        }
    }
}