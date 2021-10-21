using Yatzy.CategoryStrategy.Utility;

namespace Yatzy.CategoryStrategy
{
    public class TwosCategory : ICategoryStrategy
    {
        private const int FaceValue = 2;
        private readonly NumberPool _numberPool;
        public TwosCategory()
        {
            _numberPool = new NumberPool(FaceValue);
        }
        public int Score(int[] dice)
        {
            return _numberPool.Score(dice);
        }
    }
}