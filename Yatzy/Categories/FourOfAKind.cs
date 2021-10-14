using Yatzy.Categories.Utility;

namespace Yatzy.Categories
{
    public class FourOfAKind : ICategory
    {
        private const int NumberOfAKind = 4;
        private readonly OfAKind _helper;
        public FourOfAKind()
        {
            _helper = new OfAKind(NumberOfAKind);
        }
        public int Score(int[] dice)
        {
            return _helper.Score(dice);
        }
    }
}