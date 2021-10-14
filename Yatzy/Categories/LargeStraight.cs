using System.Linq;

namespace Yatzy.Categories
{
    public class LargeStraight : ICategory
    {
        public int Score(int[] dice)
        {
            var sortedDistinctDice = dice.Distinct().OrderByDescending(d => d).ToArray();
            const int largeStraightValue = 40;
            const int differenceBetween5ConsecutiveNumbers = 4;
            return (sortedDistinctDice.Length == 5
                    && sortedDistinctDice[0] - sortedDistinctDice[4] == differenceBetween5ConsecutiveNumbers)
                ? largeStraightValue : 0;
        }
    }
}