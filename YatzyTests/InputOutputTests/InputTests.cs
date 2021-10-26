using System;
using System.IO;
using Xunit;
using Yatzy.InputOutput;

namespace YatzyTests.InputOutputTests
{
    public class InputTests
    {
        [Fact]
        public void GetPlayerInput_TakesValueFromConsole_ReturnsSameValue()
        {
            var input = new StringReader("y");
            Console.SetIn(input);
            var testReader = new ConsoleReader();
            var actualValue = testReader.GetStringInput();
            Assert.Equal("y", actualValue);
        }
        
        [Fact]
        public void GetNumericInput_TakesNumericValueFromConsole_ReturnsInt()
        {
            var input = new StringReader("123");
            Console.SetIn(input);
            var testReader = new ConsoleReader();
            var actualValue = testReader.GetNumericInput();
            Assert.Equal(123, actualValue);
        }
        
        [Theory]
        [InlineData("abc")]
        [InlineData("")]
        [InlineData("!")]
        public void GetNumericInput_TakesNonNumericValueFromConsole_ReturnsNegative1(string testInput)
        {
            var input = new StringReader(testInput);
            Console.SetIn(input);
            var testReader = new ConsoleReader();
            var actualValue = testReader.GetNumericInput();
            Assert.Equal(-1, actualValue);
        }
    }
}