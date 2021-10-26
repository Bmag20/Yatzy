using System.Collections.Generic;
using System.Linq;
using Yatzy.CategoryStrategy;
using Yatzy.Player;

namespace YatzyTests.UtilityClasses
{
    public static class SmallSCoreCard
    {
        public static ScoreCard GetOnesTwosThreesScoreCard()
        {
            return new ScoreCard(GenerateCategoryRecords());
        }
        private static List<CategoryRecord> GenerateCategoryRecords()
        {
            return new List<CategoryRecord>
            {
                new("ONES", new OnesCategory()),
                new("TWOS", new TwosCategory()),
                new("THREES", new ThreesCategory()),
            };
        }
    }
}