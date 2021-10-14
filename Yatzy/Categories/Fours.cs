namespace Yatzy.Categories
{
    public class Fours : ICategory
    {
        public int Score(int[] dice)
        {
            return ScoreHelper.SumOfFaceInDice(4, dice);
        }
    }
}