using System.Linq;
using Yatzy.Categories;

namespace Yatzy
{
    class SmallStraight : ICategory
    {
        public int Score(int[] dice)
        {
            var sortedDistinctDice = dice.Distinct().OrderByDescending(d => d).ToArray();
            const int smallStraightValue = 30;
            const int differenceBetween4ConsecutiveNumbers = 3;
            if (sortedDistinctDice.Length >= 4
                && sortedDistinctDice[0] - sortedDistinctDice[3] == differenceBetween4ConsecutiveNumbers)
                return smallStraightValue;
            if (sortedDistinctDice.Length == 5
                && sortedDistinctDice[1] - sortedDistinctDice[4] == differenceBetween4ConsecutiveNumbers)
                return smallStraightValue;
            return 0;
        }
    }
}