using System.Collections.Generic;
using System.Linq;

namespace Yatzy.Categories.Utility
{
    public class OfAKind
    {
        private readonly int _numberOfKind;

        public OfAKind(int numberOfKind)
        {
            _numberOfKind = numberOfKind;
        }
        public int Score(IEnumerable<int> dice)
        {
            var groupedDice = dice.GroupBy(d => d)
                .Where(x => x.Count() >= _numberOfKind).ToArray();
            return groupedDice.Any() ? groupedDice.First().Key * _numberOfKind : 0;
        }
    }
}