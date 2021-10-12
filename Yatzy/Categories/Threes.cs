using Yatzy.Categories.Utility;

namespace Yatzy.Categories
{
    public class Threes : ICategory
    {
        private const int FaceValue = 3;
        private readonly NumberPool _numberPool;
        public Threes()
        {
            _numberPool = new NumberPool(FaceValue);
        }
        public int Score(int[] dice)
        {
            return _numberPool.Score(dice);
        }
    }
}