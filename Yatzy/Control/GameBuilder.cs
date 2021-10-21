using Yatzy.InputOutput;
using Yatzy.Player;

namespace Yatzy.Control
{
    public class GameBuilder
    {
        private readonly IInputHandler _inputReader;
        private readonly IOutputHandler _outputWriter;

        public GameBuilder(IInputHandler inputReader, IOutputHandler outputWriter)
        {
            _inputReader = inputReader;
            _outputWriter = outputWriter;
        }

        public GameEngine SetUpGame()
        {
            _outputWriter.Display(GameInstructions.WelcomeMessage());
            _outputWriter.Display(GameInstructions.PlayerNamePrompt());
            var playerName = _inputReader.GetPlayerInput();
            ScoreCard scoreCard = new ScoreCard();
            Player.Player player = new Player.Player(playerName, scoreCard);
            var game = new GameEngine(player);
            return game;
        }
    }
}