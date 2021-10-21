using System.Collections.Generic;
using System.Linq;
using Yatzy.CategoryStrategy;
using Yatzy.Player;

namespace YatzyTests.UtilityClasses
{
    public class OnesTwosThreesScoreCard : IScoreCard
    {
        public List<CategoryRecord> Categories { get; private set; }

        public bool AreAllCategoriesPlayed()
        {
            return Categories.All(c => c.Played);
        }

        public OnesTwosThreesScoreCard()
        {
            GenerateCategoryRecords();
        }

        private void GenerateCategoryRecords()
        {
            Categories = new List<CategoryRecord>
            {
                new CategoryRecord("ONES", new OnesCategory()),
                new CategoryRecord("TWOS", new TwosCategory()),
                new CategoryRecord("THREES", new ThreesCategory()),
            };
        }
    }
}