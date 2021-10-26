using System.Collections.Generic;
using System.Linq;
using Yatzy.CategoryStrategy;

namespace Yatzy.Player
{
    public class ScoreCard : IScoreCard
    {
        public List<CategoryRecord> Categories { get; private set; }

        public ScoreCard()
        {
            GenerateCategoryRecords();
        }

        private void GenerateCategoryRecords()
        {
            Categories = new List<CategoryRecord>
            {
                new("ONES", new OnesCategory()),
                new("TWOS", new TwosCategory()),
                new("THREES", new ThreesCategory()),
                new("FOURS", new FoursCategory()),
                new("FIVES", new FivesCategory()),
                new("SIXES", new SixesCategory()),
                new("ONE PAIR", new PairCategory()),
                new("TWO PAIRS", new TwoPairsCategory()),
                new("THREE OF A KIND", new ThreeOfAKindCategory()),
                new("FOUR OF A KIND", new FourOfAKindCategory()),
                new("FULL HOUSE", new FullHouseCategory()),
                new("SMALL STRAIGHT", new SmallStraightCategory()),
                new("LARGE STRAIGHT", new LargeStraightCategory()),
                new("CHANCE", new ChanceCategory()),
                new("YATZY", new YatzyCategory())
            };
        }
        
        public bool AreAllCategoriesPlayed()
        {
            return Categories.All(c => c.Played);
        }
    }
}