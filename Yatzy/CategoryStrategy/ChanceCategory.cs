using System.Linq;

namespace Yatzy.CategoryStrategy
{
    public class ChanceCategory : ICategoryStrategy
    {
        public int Score(int[] dice)
        {
            return dice.Sum();
        }
    }
}