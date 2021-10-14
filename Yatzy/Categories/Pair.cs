using System.Linq;

namespace Yatzy.Categories
{
    public class Pair : ICategory
    {
        public int Score(int[] dice)
        {
            var groupedDice = dice.OrderByDescending(d => d).GroupBy(d => d)
                .Where(x => x.Count() >= 2).ToArray();
            return groupedDice.Any() ? groupedDice.First().Key * 2 : 0;
        }
    }
}