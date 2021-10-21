using Yatzy.CategoryStrategy.Utility;

namespace Yatzy.CategoryStrategy
{
    public class ThreeOfAKindCategory : ICategoryStrategy
    {
        private const int NumberOfAKind = 3;
        private readonly OfAKind _helper;
        public ThreeOfAKindCategory()
        {
            _helper = new OfAKind(NumberOfAKind);
        }
        public int Score(int[] dice)
        {
            return _helper.Score(dice);
        }
    }
}