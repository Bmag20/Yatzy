using System.Collections.Generic;
using System.Linq;

namespace Yatzy.Categories
{
    public static class ScoreHelper
    {
        public static int SumOfFaceInDice(int faceValue, IEnumerable<int> dice)
        {
            return dice.Where(d => d == faceValue).Sum();
        }
        
        public static int OfAKind(int repetition, IEnumerable<int> dice)
        {
            var groupedDice = dice.GroupBy(d => d).Where(x => x.Count() >= repetition).ToArray();
            return groupedDice.Any() ? groupedDice.First().Key * repetition : 0;
        }
    }
    
    
}