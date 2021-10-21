using System.Collections.Generic;
using System.Linq;
using Yatzy.CategoryStrategy;
using Yatzy.Dice;
using Yatzy.Player;

namespace Yatzy.Control
{
    public class GameEngine
    {
        public Player.Player Player { get; }
        public YatzyTurn YatzyTurn { get; set; }
        private readonly CategoryScorerStrategy _categoryHandler;
        
        public GameEngine(Player.Player player)
        {
            Player = player;
            YatzyTurn = new YatzyTurn();
            _categoryHandler = new CategoryScorerStrategy();
        }

        public void CalculateUnPlayedCategoryScores()
        {
            foreach (var categoryRecord in Player.ScoreCard.Categories.Where(categoryRecord => !categoryRecord.Played))
            {
                _categoryHandler.SetCategory(categoryRecord.Category);
                categoryRecord.Score = _categoryHandler.GetScore(YatzyTurn.GetDiceValues());
            }
        }

        public List<CategoryRecord> GetPlayedCategories()
        {
            return Player.ScoreCard.Categories.Where(categoryRecord => categoryRecord.Played).ToList();
        }
        
        public List<CategoryRecord> GetUnPlayedCategories()
        {
            return Player.ScoreCard.Categories.Where(categoryRecord => !categoryRecord.Played).ToList();
        }
        // public void LockCategory(string categoryName)
        // {
        //     var selectedCategory = Player.ScoreCard.Categories.First(c => c.CategoryName == categoryName);
        //     selectedCategory.Played = true;
        //     Player.Score += selectedCategory.Score;
        // }

        public virtual void LockCategory(CategoryRecord categoryRecord)
        {
            categoryRecord.Played = true;
            Player.Score += categoryRecord.Score;
        }
        
        public bool IsGameEnded()
        {
            return Player.ScoreCard.AreAllCategoriesPlayed();
        }

        public virtual void ReRollDice(int[] reRollDice)
        {
            YatzyTurn.ReRoll(reRollDice);
        }
    }
}