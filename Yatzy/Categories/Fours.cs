using Yatzy.Categories.Utility;

namespace Yatzy.Categories
{
    public class Fours : ICategory
    {
        private const int FaceValue = 4;
        private readonly NumberPool _numberPool;
        public Fours()
        {
            _numberPool = new NumberPool(FaceValue);
        }
        public int Score(int[] dice)
        {
            return _numberPool.Score(dice);
        }
    }
}