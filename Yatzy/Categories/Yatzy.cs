using System.Linq;

namespace Yatzy.Categories
{
    public class Yatzy : ICategory
    {
        public int Score(int[] dice)
        {
            return dice.All(d => d == dice[0]) ? 50 : 0;
        }
    }
}