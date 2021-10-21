using System;
using Yatzy.Control;
using Yatzy.InputOutput;

namespace Yatzy
{
    class Program
    {
        static void Main(string[] args)
        {
            IInputHandler inputHandler = new YatzyReader();
            IOutputHandler outputHandler = new YatzyDisplay();
            GameBuilder gameBuilder = new GameBuilder(inputHandler, outputHandler);
            GameEngine game = gameBuilder.SetUpGame();
            var gameController = new Controller(game, inputHandler, outputHandler);
            gameController.ConductGame();
        }
    }
}