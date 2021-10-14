namespace Yatzy.Categories
{
    public class Sixes : ICategory
    {
        public int Score(int[] dice)
        {
            return ScoreHelper.SumOfFaceInDice(6, dice);
        }
    }
}