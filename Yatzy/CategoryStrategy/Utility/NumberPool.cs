using System.Collections.Generic;
using System.Linq;

namespace Yatzy.CategoryStrategy.Utility
{
    public abstract class NumberPool : ICategoryStrategy
    {
        protected int FaceValue;

        public abstract void SetFaceValue();
        
        public int Score(int[] dice)
        {
            return dice.Where(d => d == FaceValue).Sum();
        }
        
    }
}