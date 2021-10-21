using System.Linq;

namespace Yatzy.CategoryStrategy
{
    public class YatzyCategory : ICategoryStrategy
    {
        public int Score(int[] dice)
        {
            return dice.All(d => d == dice[0]) ? 50 : 0;
        }
    }
}