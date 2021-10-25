using System.Collections.Generic;
using Yatzy.InputOutput;
using Yatzy.Player;

namespace Yatzy.Control
{
    public class Controller
    {
        private const int NumberOfDicePerRoll = 5;
        private readonly IInputHandler _inputReader;
        private readonly IOutputHandler _outputWriter;
        private Game Game { get; }
        private List<CategoryRecord> _unPlayedCategories;
        private bool _playerQuit;
        private IPlayer _currentPlayer;

        public Controller(Game game, IInputHandler inputReader, IOutputHandler outputWriter)
        {
            Game = game;
            _inputReader = inputReader;
            _outputWriter = outputWriter;
            _playerQuit = false;
        }

        public void ConductGame()
        {
            while (!_playerQuit && !Game.IsGameEnded())
            {
                foreach (var player in Game.Players)
                {
                    _currentPlayer = player;
                    _outputWriter.PlayerTurn(_currentPlayer);
                    CheckIfPlayerWantsToQuit();
                    if (_playerQuit)
                    {
                        _outputWriter.PlayerAbandoned(_currentPlayer);
                        break;
                    }
                    DisplayCategoriesState();
                    ConductTurn();
                }
            }
            _outputWriter.DisplayFinalScores(Game.Players);
            _outputWriter.Display(GameInstructions.GameEnded());
        }

        private void ConductTurn()
        {
            Game.YatzyTurn.RollDice();
            _outputWriter.DisplayDice(Game.YatzyTurn.GetDiceValues());
            ConductReRoll();
            Game.CalculateUnPlayedCategoryScores(_currentPlayer);
            _unPlayedCategories = Game.GetUnPlayedCategories(_currentPlayer);
            _outputWriter.DisplayCategoryScores(_unPlayedCategories);
            var categoryIndex = GetValidCategoryIndex();
            Game.LockCategory(_unPlayedCategories[categoryIndex], _currentPlayer);
            _outputWriter.PlayerScore(_currentPlayer);
        }

        private void ConductReRoll()
        {
            bool userWantsToReRoll = true;  
            while (userWantsToReRoll && Game.YatzyTurn.CanBeRolled())
            {
                _outputWriter.Display(GameInstructions.ReRollPrompt());
                userWantsToReRoll = InputValidator.IsYes(_inputReader.GetPlayerInput());
                if (userWantsToReRoll)
                {
                    var reRollDice = GetDiceIndicesToReRoll();
                    Game.ReRollDice(reRollDice);
                }
                _outputWriter.DisplayDice(Game.YatzyTurn.GetDiceValues());
            }
        }

        private int[] GetDiceIndicesToReRoll()
        {
            List<int> reRollDice = new List<int>();
            while (true)
            {
                _outputWriter.Display(GameInstructions.NumberToReRoll());
                var playerInput = _inputReader.GetNumericInput();
                if (InputValidator.IsLessThan(playerInput, NumberOfDicePerRoll))
                    reRollDice.Add(playerInput);
                else
                    return reRollDice.ToArray();
            }
        }
        private void DisplayCategoriesState()
        {
            _outputWriter.Display("Played Categories : ");
            _outputWriter.DisplayCategories(Game.GetPlayedCategories(_currentPlayer));
            _outputWriter.Display("Categories to be played: ");
            _outputWriter.DisplayCategories(Game.GetUnPlayedCategories(_currentPlayer));
        }

        private int GetValidCategoryIndex()
        {
            while(true)
            {
                _outputWriter.Display(GameInstructions.AskCategory());
                var playerInput = _inputReader.GetNumericInput();
                if (InputValidator.IsLessThan(playerInput, _unPlayedCategories.Count))
                    return playerInput - 1;
                _outputWriter.Display(GameInstructions.InValidCategory());
            }
        }

        private void CheckIfPlayerWantsToQuit()
        {
            _outputWriter.Display(GameInstructions.WantsToQuit());
            var playerInput = _inputReader.GetPlayerInput();
            _playerQuit = InputValidator.IsQuit(playerInput);
        }

    }
}