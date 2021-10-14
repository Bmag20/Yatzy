namespace Yatzy.Categories
{
    public class Threes : ICategory
    {
        public int Score(int[] dice)
        {
            return ScoreHelper.SumOfFaceInDice(1, dice);
        }
    }
}