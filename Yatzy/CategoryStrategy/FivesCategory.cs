using Yatzy.CategoryStrategy.Utility;

namespace Yatzy.CategoryStrategy
{
    public class FivesCategory : ICategoryStrategy
    {
        private const int FaceValue = 5;
        private readonly NumberPool _numberPool;
        public FivesCategory()
        {
            _numberPool = new NumberPool(FaceValue);
        }
        public int Score(int[] dice)
        {
            return _numberPool.Score(dice);
        }
    }
}