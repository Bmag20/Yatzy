using System.Linq;

namespace Yatzy.Categories
{
    public class Chance : ICategory
    {
        public int Score(int[] dice)
        {
            return dice.Sum();
        }
    }
}