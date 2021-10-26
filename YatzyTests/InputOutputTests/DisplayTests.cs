using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Yatzy.InputOutput;
using Yatzy.Player;
using YatzyTests.UtilityClasses;

namespace YatzyTests.InputOutputTests
{
    public class DisplayTests
    {
        
        [Fact]
        public void DisplayDice_OutputsDiceToConsoleInDesiredFormat()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            var outputHandlerTest = new ConsoleDisplay();
            outputHandlerTest.DisplayDice(new []{5, 6});
            const string expectedOutput = "\nDice:\n#1    #2    \n[5]   [6]   \n";
            Assert.Equal(expectedOutput, output.ToString());
        }
        
        [Fact]
        public void DisplayWinners_OutputsWinnerToConsoleInDesiredFormat()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            var outputHandlerTest = new ConsoleDisplay();
            var player = new YatzyPlayer("Bhuvana", SmallSCoreCard.GetOnesTwosThreesScoreCard(),
                new HumanInteractor(new ConsoleReader(), new ConsoleDisplay()));
            var winners = new List<IPlayer>(){player};
            outputHandlerTest.DisplayWinners(winners);
            const string expectedOutput = "\nWinner of the game is :\nBhuvana with score 0\n";
            Assert.Equal(expectedOutput, output.ToString());
        }
        
        [Fact]
        public void DisplayWinners_OutputsWinnersToConsoleInDesiredFormat()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            var outputHandlerTest = new ConsoleDisplay();
            var player1 = new YatzyPlayer("Bhuvana", SmallSCoreCard.GetOnesTwosThreesScoreCard(),
                new HumanInteractor(new ConsoleReader(), new ConsoleDisplay()));
            var player2 = new YatzyPlayer("Sandy", SmallSCoreCard.GetOnesTwosThreesScoreCard(),
                new HumanInteractor(new ConsoleReader(), new ConsoleDisplay()));
            var winners = new List<IPlayer>(){player1, player2};
            outputHandlerTest.DisplayWinners(winners);
            const string expectedOutput = "\nWinners of the game are :\nBhuvana with score 0\nSandy with score 0\n";
            Assert.Equal(expectedOutput, output.ToString());
        }
        
    }
}