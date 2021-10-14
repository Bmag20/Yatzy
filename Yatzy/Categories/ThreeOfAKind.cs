using Yatzy.Categories.Utility;

namespace Yatzy.Categories
{
    public class ThreeOfAKind : ICategory
    {
        private const int NumberOfAKind = 3;
        private readonly OfAKind _helper;
        public ThreeOfAKind()
        {
            _helper = new OfAKind(NumberOfAKind);
        }
        public int Score(int[] dice)
        {
            return _helper.Score(dice);
        }
    }
}