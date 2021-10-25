using System;
using Yatzy.Control;
using Yatzy.InputOutput;
using Yatzy.Player;

namespace Yatzy
{
    static class Program
    {
        static void Main(string[] args)
        {
            IInputHandler inputHandler = new YatzyReader();
            IOutputHandler outputHandler = new YatzyDisplay();
            IGameBuilder gameBuilder = new GameBuilder(inputHandler, outputHandler);
            Game game = gameBuilder.SetUpGame();
            var gameController = new Controller(game, inputHandler, outputHandler);
            gameController.ConductGame();
        }
    }
}