using Yatzy.Categories;

namespace Yatzy
{
    public class Twos : ICategory
    {
        public int Score(int[] dice)
        {
            return ScoreHelper.SumOfFaceInDice(2, dice);
        }
    }
}