namespace Yatzy.Categories
{
    public class Ones : ICategory
    {
        public int Score(int[] dice)
        {
            return ScoreHelper.SumOfFaceInDice(1, dice);
        }
    }
}