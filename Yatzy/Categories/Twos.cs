using Yatzy.Categories;
using Yatzy.Categories.Utility;

namespace Yatzy
{
    public class Twos : ICategory
    {
        private const int FaceValue = 1;
        private readonly NumberPool _numberPool;
        public Twos()
        {
            _numberPool = new NumberPool(FaceValue);
        }
        public int Score(int[] dice)
        {
            return _numberPool.Score(dice);
        }
    }
}