using Yatzy.Categories.Utility;

namespace Yatzy.Categories
{
    public class Ones : ICategory
    {
        private const int FaceValue = 1;
        private readonly NumberPool _numberPool;
        public Ones()
        {
            _numberPool = new NumberPool(FaceValue);
        }
        public int Score(int[] dice)
        {
            return _numberPool.Score(dice);
        }
    }
}