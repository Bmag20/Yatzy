using System.Collections.Generic;
using System.Linq;
using Yatzy.CategoryStrategy;

namespace Yatzy.Player
{
    public class ScoreCard : IScoreCard
    {
        public List<CategoryRecord> Categories { get; private set; }

        // public int TotalScore { get; set; }

        public ScoreCard()
        {
            GenerateCategoryRecords();
            // TotalScore = 0;
        }

        private void GenerateCategoryRecords()
        {
            Categories = new List<CategoryRecord>
            {
                new CategoryRecord("ONES", new OnesCategory()),
                new CategoryRecord("TWOS", new TwosCategory()),
                new CategoryRecord("THREES", new ThreesCategory()),
                new CategoryRecord("FOURS", new FoursCategory()),
                new CategoryRecord("FIVES", new FivesCategory()),
                new CategoryRecord("SIXES", new SixesCategory()),
                new CategoryRecord("ONE PAIR", new PairCategory()),
                new CategoryRecord("TWO PAIRS", new TwoPairsCategory()),
                new CategoryRecord("THREE OF A KIND", new ThreeOfAKindCategory()),
                new CategoryRecord("FOUR OF A KIND", new FourOfAKindCategory()),
                new CategoryRecord("FULL HOUSE", new FullHouseCategory()),
                new CategoryRecord("SMALL STRAIGHT", new SmallStraightCategory()),
                new CategoryRecord("LARGE STRAIGHT", new LargeStraightCategory()),
                new CategoryRecord("CHANCE", new ChanceCategory()),
                new CategoryRecord("YATZY", new YatzyCategory())
            };
        }
        
        public bool AreAllCategoriesPlayed()
        {
            return Categories.All(c => c.Played);
        }
    }
}