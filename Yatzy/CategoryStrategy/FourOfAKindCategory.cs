using Yatzy.CategoryStrategy.Utility;

namespace Yatzy.CategoryStrategy
{
    public class FourOfAKindCategory : ICategoryStrategy
    {
        private const int NumberOfAKind = 4;
        private readonly OfAKind _helper;
        public FourOfAKindCategory()
        {
            _helper = new OfAKind(NumberOfAKind);
        }
        public int Score(int[] dice)
        {
            return _helper.Score(dice);
        }
    }
}