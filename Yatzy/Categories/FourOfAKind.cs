namespace Yatzy.Categories
{
    public class FourOfAKind : ICategory
    {
        public int Score(int[] dice)
        {
            return ScoreHelper.OfAKind(4, dice);
        }
    }
}