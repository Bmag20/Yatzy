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
        private GameEngine Game { get; }
        private List<CategoryRecord> _unPlayedCategories;
        private bool _playerQuit;

        public Controller(GameEngine game, IInputHandler inputReader, IOutputHandler outputWriter)
        {
            Game = game;
            _inputReader = inputReader;
            _outputWriter = outputWriter;
            _playerQuit = false;
        }

        public void ConductGame()
        {
             while(!_playerQuit && !Game.IsGameEnded())
                ConductTurn();
             _outputWriter.Display(GameInstructions.GameEnded());
        }

        private void ConductTurn()
        {
            _outputWriter.PlayerTurn(Game.Player);
            DisplayCategoriesState();
            Game.YatzyTurn.RollDice();
            _outputWriter.DisplayDice(Game.YatzyTurn.GetDiceValues());
            ConductReRoll();
            Game.CalculateUnPlayedCategoryScores();
            _unPlayedCategories = Game.GetUnPlayedCategories();
            _outputWriter.DisplayCategoryScores(_unPlayedCategories);
            var categoryIndex = GetValidCategoryIndex();
            Game.LockCategory(Game.GetUnPlayedCategories()[categoryIndex]);
            _outputWriter.PlayerScore(Game.Player);
            CheckIfPlayerWantsToQuit();
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
                if (InputValidator.IsValidValue(playerInput, NumberOfDicePerRoll))
                    reRollDice.Add(playerInput);
                else
                    return reRollDice.ToArray();
            }
        }
        private void DisplayCategoriesState()
        {
            _outputWriter.Display("Played Categories : ");
            _outputWriter.DisplayCategories(Game.GetPlayedCategories());
            _outputWriter.Display("Categories to be played: ");
            _outputWriter.DisplayCategories(Game.GetUnPlayedCategories());
        }

        private int GetValidCategoryIndex()
        {
            while(true)
            {
                _outputWriter.Display(GameInstructions.AskCategory());
                var playerInput = _inputReader.GetNumericInput();
                if (InputValidator.IsValidValue(playerInput, _unPlayedCategories.Count + 1))
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