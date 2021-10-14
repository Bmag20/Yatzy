namespace Yatzy.Categories
{
    public class ThreeOfAKind : ICategory
    {
        public int Score(int[] dice)
        {
            return ScoreHelper.OfAKind(3, dice);
        }
    }
}