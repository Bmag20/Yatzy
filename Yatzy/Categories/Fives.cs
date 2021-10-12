namespace Yatzy.Categories
{
    public class Fives : ICategory
    {
        public int Score(int[] dice)
        {
            return ScoreHelper.SumOfFaceInDice(5, dice);
        }
    }
}