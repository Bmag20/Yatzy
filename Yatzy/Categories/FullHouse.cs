using System.Linq;

namespace Yatzy.Categories
{
    public class FullHouse : ICategory
    {
        public int Score(int[] dice)
        {
            var groupedDice = dice.GroupBy(d => d).Where(x => x.Count() is 2 or 3).ToArray();
            return groupedDice.Length == 2 && (groupedDice[0].Count() == 3 || groupedDice[1].Count() == 3) ? dice.Sum() : 0;
        }
    }
}