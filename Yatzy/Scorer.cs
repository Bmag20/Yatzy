using System.Collections.Generic;
using System.Linq;

namespace Yatzy
{
    public static class Scorer
    {
        public static int Chance(IEnumerable<int> dice)
        {
            return dice.Sum();
        }

        public static int Yatzy(int[] dice)
        {
            return dice.All(d => d == dice[0]) ? 50 : 0;
        }
        
       public static int SumOfFaceInDice(int faceValue, IEnumerable<int> dice)
       {
           return dice.Where(d => d == faceValue).Sum();
       }
       
       public static int Pair(int[] dice)
       {
           var groupedDice = dice.OrderByDescending(d => d).GroupBy(d => d)
               .Where(x => x.Count() >= 2).ToArray();
           return groupedDice.Any() ? groupedDice.First().Key * 2 : 0;
       }
       
       public static int TwoPairs(int[] dice)
       {
           var groupedDice = dice.GroupBy(d => d).Where(x => x.Count() >= 2).ToArray();
           return groupedDice.Length == 2 ? (groupedDice[0].Key + groupedDice[1].Key) * 2 : 0;
       }

       public static int ThreeOfAKind(int[] dice)
       {
           var groupedDice = dice.GroupBy(d => d).Where(x => x.Count() >= 3).ToArray();
           return groupedDice.Any() ? groupedDice.First().Key * 3 : 0;
       }

       public static int FourOfAKind(int[] dice)
       {
           var groupedDice = dice.GroupBy(d => d).Where(x => x.Count() >= 4).ToArray();
           return groupedDice.Any() ? groupedDice.First().Key * 4 : 0;
       }
       
       public static int OfAKind(int[] dice, int repetition)
       {
           var groupedDice = dice.GroupBy(d => d).Where(x => x.Count() >= repetition).ToArray();
           return groupedDice.Any() ? groupedDice.First().Key * repetition : 0;
       }

       public static int SmallStraight(int[] dice)
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

       public static int LargeStraight(int[] dice)
       {
           var sortedDistinctDice = dice.Distinct().OrderByDescending(d => d).ToArray();
           const int largeStraightValue = 40;
           const int differenceBetween5ConsecutiveNumbers = 4;
           return (sortedDistinctDice.Length == 5
               && sortedDistinctDice[0] - sortedDistinctDice[4] == differenceBetween5ConsecutiveNumbers)
               ? largeStraightValue : 0;

       }

       public static int FullHouse(int[] dice)
       {
           var groupedDice = dice.GroupBy(d => d).Where(x => x.Count() is 2 or 3).ToArray();
           return groupedDice.Length == 2 && (groupedDice[0].Count() == 3 || groupedDice[1].Count() == 3) ? dice.Sum() : 0;
       }
    }
}