using Yatzy.CategoryStrategy.Utility;

namespace Yatzy.CategoryStrategy
{
    public class SixesCategory : ICategoryStrategy
    {
        private const int FaceValue = 6;
        private readonly NumberPool _numberPool;
        public SixesCategory()
        {
            _numberPool = new NumberPool(FaceValue);
        }
        public int Score(int[] dice)
        {
            return _numberPool.Score(dice);
        }
    }
}