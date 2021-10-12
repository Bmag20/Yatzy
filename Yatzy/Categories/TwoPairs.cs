using System.Linq;
using Yatzy.Categories;

namespace Yatzy
{
    public class TwoPairs : ICategory
    {
        public int Score(int[] dice)
        {
            var groupedDice = dice.GroupBy(d => d).Where(x => x.Count() >= 2).ToArray();
            return groupedDice.Length == 2 ? (groupedDice[0].Key + groupedDice[1].Key) * 2 : 0;
        }
    }
}