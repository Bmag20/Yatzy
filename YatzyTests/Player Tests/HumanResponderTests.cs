using Moq;
using Xunit;
using Yatzy.InputOutput;
using Yatzy.Player;

namespace YatzyTests.Player_Tests
{
    public class HumanResponderTests
    {
        private static readonly IOutputHandler OutputWriter = new ConsoleDisplay();

        [Theory]
        [InlineData("y")]
        [InlineData("Y")]
        public void PlayerWantsToReRoll_UserInputsY_ReturnsTrue(string inputValue)
        {
            var inputMock = new Mock<IInputHandler>();
            inputMock.Setup(i => i.GetStringInput()).Returns(inputValue);
            var responseHandler = new HumanInteractor(inputMock.Object, OutputWriter);
            Assert.True(responseHandler.PlayerWantsToReRoll());
        }
        
        [Theory]
        [InlineData("")]
        [InlineData("n")]
        [InlineData("123")]
        [InlineData("!")]
        public void PlayerWantsToReRoll_UserInputsAnyValueOtherThanY_ReturnsFalse(string inputValue)
        {
            var inputMock = new Mock<IInputHandler>();
            inputMock.Setup(i => i.GetStringInput()).Returns(inputValue);
            var responseHandler = new HumanInteractor(inputMock.Object, OutputWriter);
            Assert.False(responseHandler.PlayerWantsToReRoll());
        }
        
        [Fact]
        public void DiceIndicesToReRoll_TakesDiceValuesLessThanDicePerRoll_ReturnsDice()
        {
            //Arrange
            var inputMock = new Mock<IInputHandler>();
            inputMock.SetupSequence(i => i.GetNumericInput()).Returns(1);
            var responseHandler = new HumanInteractor(inputMock.Object, OutputWriter);
            const int dicePerRoll = 5;
            //Act
            var actualValue = responseHandler.DiceIndicesToReRoll(dicePerRoll);
            //Assert
            var expectedValue = new[] {1};
            Assert.Equal(expectedValue, actualValue);
        }
        
        [Fact]
        public void CategoryIndexToPlaceRoll_TakesValuesLessThanNumberOfCategories_ReturnsCategoryIndex()
        {
            //Arrange
            var inputMock = new Mock<IInputHandler>();
            inputMock.SetupSequence(i => i.GetNumericInput()).Returns(1);
            var responseHandler = new HumanInteractor(inputMock.Object, OutputWriter);
            const int categoryCount = 5;
            //Act
            var actualValue = responseHandler.CategoryIndexToPlaceRoll(categoryCount);
            //Assert
            Assert.Equal(0, actualValue);
        }
        
        [Fact]
        public void CategoryIndexToPlaceRoll_TakesValuesGreaterThanNumberOfCategories_PromptsInvalidCategory()
        {
            //Arrange
            var inputMock = new Mock<IInputHandler>();
            inputMock.SetupSequence(i => i.GetNumericInput()).Returns(10).Returns(1);
            var oMock = new Mock<IOutputHandler>();
            var responseHandler = new HumanInteractor(inputMock.Object, oMock.Object);
            const int categoryCount = 5;
            //Act
            responseHandler.CategoryIndexToPlaceRoll(categoryCount);
            //Assert
            oMock.Verify(o => o.Display(GameInstructions.InValidCategory()), Times.Once);
        }
        
        [Theory]
        [InlineData("Q")]
        [InlineData("q")]
        public void PlayerWantsToQuit_UserInputsQ_ReturnsTrue(string inputValue)
        {
            var inputMock = new Mock<IInputHandler>();
            inputMock.Setup(i => i.GetStringInput()).Returns(inputValue);
            var responseHandler = new HumanInteractor(inputMock.Object, OutputWriter);
            Assert.True(responseHandler.PlayerWantsToQuit());
        }
        
        [Theory]
        [InlineData("")]
        [InlineData("n")]
        [InlineData("123")]
        [InlineData("!")]
        public void PlayerWantsToQuit_UserInputsAnyValueOtherThanQ_ReturnsFalse(string inputValue)
        {
            var inputMock = new Mock<IInputHandler>();
            inputMock.Setup(i => i.GetStringInput()).Returns(inputValue);
            var responseHandler = new HumanInteractor(inputMock.Object, OutputWriter);
            Assert.False(responseHandler.PlayerWantsToQuit());
        }
    }
}