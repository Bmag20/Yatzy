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
        public IYatzyTurn YatzyTurn { get; }
        private readonly CategoryScorerStrategy _categoryHandler;
        
        public Game(List<IPlayer> players, IYatzyTurn yatzyTurn)
        {
            Players = players;
            YatzyTurn = yatzyTurn;
            _categoryHandler = new CategoryScorerStrategy();
        }

        public void CalculateUnPlayedCategoryScores(IPlayer player)
        {
            foreach (var categoryRecord in player.ScoreCard.Categories.Where(categoryRecord => !categoryRecord.Played))
            {
                _categoryHandler.SetCategory(categoryRecord.Category);
                categoryRecord.Score = _categoryHandler.GetScore(YatzyTurn.GetDiceValues());
            }
        }

        public List<CategoryRecord> GetPlayedCategories(IPlayer player)
        {
            return player.ScoreCard.Categories.Where(categoryRecord => categoryRecord.Played).ToList();
        }
        
        public List<CategoryRecord> GetUnPlayedCategories(IPlayer player)
        {
            return player.ScoreCard.Categories.Where(categoryRecord => !categoryRecord.Played).ToList();
        }

        public virtual void LockCategory(CategoryRecord categoryRecord, IPlayer player)
        {
            categoryRecord.Played = true;
            player.Score += categoryRecord.Score;
        }
        
        public bool IsGameEnded()
        {
            return Players.All(p => p.ScoreCard.AreAllCategoriesPlayed());
        }

        public virtual void ReRollDice(int[] reRollDice)
        {
            YatzyTurn.ReRoll(reRollDice);
        }
    }
}