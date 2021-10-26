using System.Collections.Generic;
using Yatzy.CategoryStrategy;
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
        private const int NumberOfDicePerRoll = 5;
        private const int MaximumRolls = 3;
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
            var players = new List<IPlayer>();
            for (var i = 0; i < numberOfPlayers; i++)
            {
                var playerName = gameSetUpHelper.GetPlayerName();
                var scoreCard = new ScoreCard(GenerateCategoryRecords());
                IInteractor humanInteractor = new HumanInteractor(_inputReader, _outputWriter);
                IPlayer player = new YatzyPlayer(playerName, scoreCard, humanInteractor);
                players.Add(player);
            }

            var game = new Game(players, new YatzyTurn(NumberOfDicePerRoll, MaximumRolls));
            return game;
        }
        
        private List<CategoryRecord> GenerateCategoryRecords()
        {
            return new List<CategoryRecord>
            {
                new("ONES", new OnesCategory()),
                new("TWOS", new TwosCategory()),
                new("THREES", new ThreesCategory()),
                new("FOURS", new FoursCategory()),
                new("FIVES", new FivesCategory()),
                new("SIXES", new SixesCategory()),
                new("ONE PAIR", new PairCategory()),
                new("TWO PAIRS", new TwoPairsCategory()),
                new("THREE OF A KIND", new ThreeOfAKindCategory()),
                new("FOUR OF A KIND", new FourOfAKindCategory()),
                new("FULL HOUSE", new FullHouseCategory()),
                new("SMALL STRAIGHT", new SmallStraightCategory()),
                new("LARGE STRAIGHT", new LargeStraightCategory()),
                new("CHANCE", new ChanceCategory()),
                new("YATZY", new YatzyCategory())
            };
        }
    }
}