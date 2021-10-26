using System.Collections.Generic;
using System.Linq;
using Yatzy.CategoryStrategy;
using Yatzy.Dice;
using Yatzy.Player;

namespace Yatzy.Control
{
    public class Game
    {
        public List<IPlayer> Players { get; }
        public YatzyTurn YatzyTurn { get; }
        
        private readonly CategoryScorerStrategy _categoryScorer;
        
        public Game(List<IPlayer> players, YatzyTurn yatzyTurn)
        {
            Players = players;
            YatzyTurn = yatzyTurn;
            _categoryScorer = new CategoryScorerStrategy();
        }

        public void RollDice() => YatzyTurn.RollDice();
        public int[] GetCurrentRoll() => YatzyTurn.GetDiceValues();
        public bool CanBeReRolled() => YatzyTurn.CanBeRolled();
        public void ReRollDice(int[] reRollDice) =>  YatzyTurn.ReRoll(reRollDice);
        public int GetNumberOfDicePerRoll() =>  YatzyTurn.Dice.Length;
        public bool IsGameEnded() => Players.All(p => p.ScoreCard.AreAllCategoriesPlayed());

        public void CalculateUnPlayedCategoryScores(IPlayer player)
        {
            foreach (var categoryRecord in player.ScoreCard.Categories.Where(categoryRecord => !categoryRecord.Played))
            {
                _categoryScorer.SetCategory(categoryRecord.Category);
                categoryRecord.Score = _categoryScorer.GetScore(YatzyTurn.GetDiceValues());
            }
        }
        
        public void LockCategory(CategoryRecord categoryRecord, IPlayer player)
        {
            categoryRecord.Played = true;
            player.Score += categoryRecord.Score;
        }
        
        public List<CategoryRecord> GetPlayedCategories(IPlayer player)
        {
            return player.ScoreCard.Categories.Where(categoryRecord => categoryRecord.Played).ToList();
        }
        
        public List<CategoryRecord> GetUnPlayedCategories(IPlayer player)
        {
            return player.ScoreCard.Categories.Where(categoryRecord => !categoryRecord.Played).ToList();
        }

        public List<IPlayer> DetermineWinner()
        {
            var highestScore = Players.Select(player => player.Score).Max();
            return highestScore > 0 ? Players.Where(player => player.Score == highestScore).ToList() : new List<IPlayer>();
        }
    }
}