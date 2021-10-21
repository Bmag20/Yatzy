using Yatzy.CategoryStrategy.Utility;

namespace Yatzy.CategoryStrategy
{
    public class ThreesCategory : ICategoryStrategy
    {
        private const int FaceValue = 3;
        private readonly NumberPool _numberPool;
        public ThreesCategory()
        {
            _numberPool = new NumberPool(FaceValue);
        }
        public int Score(int[] dice)
        {
            return _numberPool.Score(dice);
        }
    }
}