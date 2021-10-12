using Yatzy.Categories.Utility;

namespace Yatzy.Categories
{
    public class Sixes : ICategory
    {
        private const int FaceValue = 6;
        private readonly NumberPool _numberPool;
        public Sixes()
        {
            _numberPool = new NumberPool(FaceValue);
        }
        public int Score(int[] dice)
        {
            return _numberPool.Score(dice);
        }
    }
}