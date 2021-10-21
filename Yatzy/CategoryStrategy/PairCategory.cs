using System.Linq;

namespace Yatzy.CategoryStrategy
{
    public class PairCategory : ICategoryStrategy
    {
        public int Score(int[] dice)
        {
            var groupedDice = dice.OrderByDescending(d => d).GroupBy(d => d)
                .Where(x => x.Count() >= 2).ToArray();
            return groupedDice.Any() ? groupedDice.First().Key * 2 : 0;
        }
    }
}