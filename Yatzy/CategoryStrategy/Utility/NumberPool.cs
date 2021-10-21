using System.Collections.Generic;
using System.Linq;

namespace Yatzy.CategoryStrategy.Utility
{
    public class NumberPool
    {
        private readonly int _faceValue;

        public NumberPool(int faceValue)
        {
            _faceValue = faceValue;
        }
        
        public int Score(IEnumerable<int> dice)
        {
            return dice.Where(d => d == _faceValue).Sum();
        }
    }
}