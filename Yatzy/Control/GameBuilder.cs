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
            _outputWriter.DisplayWelcomeMessage();
            var gameSetUpHelper = new GameSetUpHelper(_inputReader, _outputWriter);
            var numberOfPlayers = gameSetUpHelper.GetPlayerCount(MaximumPlayers);
            List<IPlayer> players = new List<IPlayer>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                var playerName = gameSetUpHelper.GetPlayerName();
                ScoreCard scoreCard = new ScoreCard();
                IInteractor humanInteractor = new HumanInteractor(_inputReader, _outputWriter);
                IPlayer player = new YatzyPlayer(playerName, scoreCard, humanInteractor);
                players.Add(player);
            }

            var game = new Game(players, new YatzyTurn());
            return game;
        }
    }
}