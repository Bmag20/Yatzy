using Yatzy.CategoryStrategy.Utility;

namespace Yatzy.CategoryStrategy
{
    public class FoursCategory : ICategoryStrategy
    {
        private const int FaceValue = 4;
        private readonly NumberPool _numberPool;
        public FoursCategory()
        {
            _numberPool = new NumberPool(FaceValue);
        }
        public int Score(int[] dice)
        {
            return _numberPool.Score(dice);
        }
    }
}