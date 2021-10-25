using System.Collections.Generic;
using Yatzy.Dice;
using Yatzy.InputOutput;
using Yatzy.Player;

namespace Yatzy.Control
{
    public class GameBuilder : IGameBuilder
    {
        private readonly IInputHandler _inputReader;
        private readonly IOutputHandler _outputWriter;
        private const int MaximumPlayers = 10;

        public GameBuilder(IInputHandler inputReader, IOutputHandler outputWriter)
        {
            _inputReader = inputReader;
            _outputWriter = outputWriter;
        }

        public Game SetUpGame()
        {
            _outputWriter.Display(GameInstructions.WelcomeMessage());
            var numberOfPlayers = GetPlayerCount();
            List<IPlayer> players = new List<IPlayer>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                var playerName = GetPlayerName();
                ScoreCard scoreCard = new ScoreCard();
                Player.IPlayer player = new Player.YatzyPlayer(playerName, scoreCard);
                players.Add(player);
            }

            var game = new Game(players, new YatzyTurn());
            return game;
        }

        private int GetPlayerCount()
        {
            _outputWriter.Display(GameInstructions.NumberOfPlayersPrompt());
            var input = _inputReader.GetNumericInput();
            while (!InputValidator.IsLessThan(input, MaximumPlayers))
            {
                _outputWriter.Display(GameInstructions.ReEnterNumberOfPlayers());
                input = _inputReader.GetNumericInput();
            }
            return input;
        }

        private string GetPlayerName()
        {
            _outputWriter.Display(GameInstructions.PlayerNamePrompt());
            return _inputReader.GetPlayerInput();
        }
    }
}