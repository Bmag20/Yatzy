using System.Collections.Generic;
using Yatzy.InputOutput;
using Yatzy.Player;

namespace Yatzy.Control
{
    public class Controller
    {
        private readonly IOutputHandler _outputWriter;
        private Game Game { get; }
        private List<CategoryRecord> _unPlayedCategories;
        private IPlayer _currentPlayer;

        public Controller(Game game, IOutputHandler outputWriter)
        {
            Game = game;
            _outputWriter = outputWriter;
        }

        public void ConductGame()
        {
            while (!Game.IsGameEnded())
            {
                foreach (var player in Game.Players)
                {
                    _currentPlayer = player;
                    _outputWriter.PlayerTurn(_currentPlayer);
                    if (_currentPlayer.ResponseHandler.PlayerWantsToQuit())
                    {
                        AbandonGame();
                        return;
                    }
                    ConductTurn();
                }
            }
            EndGame();
        }

        private void ConductTurn()
        {
            DisplayCategoriesState();
            Game.RollDice();
            _outputWriter.DisplayDice(Game.GetCurrentRoll());
            ConductReRoll();
            Game.CalculateUnPlayedCategoryScores(_currentPlayer);
            _unPlayedCategories = Game.GetUnPlayedCategories(_currentPlayer);
            _outputWriter.DisplayCategoryScores(_unPlayedCategories);
            var categoryIndex = _currentPlayer.ResponseHandler.CategoryIndexToPlaceRoll(_unPlayedCategories.Count);
            Game.LockCategory(_unPlayedCategories[categoryIndex], _currentPlayer);
            _outputWriter.PlayerScore(_currentPlayer);
        }

        private void ConductReRoll()
        {
            var userWantsToReRoll = true;
            while (userWantsToReRoll && Game.CanBeReRolled())
            {
                _outputWriter.DisplayReRollPrompt();
                userWantsToReRoll = _currentPlayer.ResponseHandler.PlayerWantsToReRoll();
                if (userWantsToReRoll)
                {
                    var reRollDice = _currentPlayer.ResponseHandler.DiceIndicesToReRoll(Game.GetNumberOfDicePerRoll());
                    Game.ReRollDice(reRollDice);
                }
                _outputWriter.DisplayDice(Game.GetCurrentRoll());
            }
        }

        private void DisplayCategoriesState()
        {
            _outputWriter.DisplayPlayedCategories(Game.GetPlayedCategories(_currentPlayer));
            _outputWriter.DisplayUnPlayedCategories(Game.GetUnPlayedCategories(_currentPlayer));
        }

        private void AbandonGame()
        {
            _outputWriter.PlayerAbandoned(_currentPlayer);
            _outputWriter.DisplayFinalScores(Game.Players);
            _outputWriter.DisplayGameEnded();
        }
        
        private void EndGame()
        {
            _outputWriter.DisplayFinalScores(Game.Players);
            var winners = Game.DetermineWinner();
            _outputWriter.DisplayWinners(winners);
            _outputWriter.DisplayGameEnded();
        }
    }
}