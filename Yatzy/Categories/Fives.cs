using Yatzy.Categories.Utility;

namespace Yatzy.Categories
{
    public class Fives : ICategory
    {
        private const int FaceValue = 5;
        private readonly NumberPool _numberPool;
        public Fives()
        {
            _numberPool = new NumberPool(FaceValue);
        }
        public int Score(int[] dice)
        {
            return _numberPool.Score(dice);
        }
    }
}