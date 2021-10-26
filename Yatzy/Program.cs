using Yatzy.Control;
using Yatzy.InputOutput;

namespace Yatzy
{
    static class Program
    {
        static void Main(string[] args)
        {
            IInputHandler inputHandler = new ConsoleReader();
            IOutputHandler outputHandler = new ConsoleDisplay();
            IGameBuilder gameBuilder = new GameBuilder(inputHandler, outputHandler);
            var game = gameBuilder.SetUpGame();
            var gameController = new Controller(game, outputHandler);
            gameController.ConductGame();
        }
    }
}