using System.Collections.Generic;
using System.Linq;
using Yatzy.CategoryStrategy;

namespace Yatzy.Player
{
    public class ScoreCard
    {
        public List<CategoryRecord> Categories { get; private set; }

        public ScoreCard(List<CategoryRecord> categoryRecords)
        {
            Categories = categoryRecords;
        }

        public bool AreAllCategoriesPlayed()
        {
            return Categories.All(c => c.Played);
        }
    }
}