using Yatzy.CategoryStrategy.Utility;

namespace Yatzy.CategoryStrategy
{
    public class OnesCategory : ICategoryStrategy
    {
        public const string Name = "ONES";
        private const int FaceValue = 1;
        private readonly NumberPool _numberPool;
        public OnesCategory()
        {
            _numberPool = new NumberPool(FaceValue);
        }
        public int Score(int[] dice)
        {
            return _numberPool.Score(dice);
        }
    }
}