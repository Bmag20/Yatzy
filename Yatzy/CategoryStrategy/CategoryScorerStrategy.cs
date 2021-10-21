namespace Yatzy.CategoryStrategy
{
    public class CategoryScorerStrategy
    {
        private ICategoryStrategy _category;

        public void SetCategory(ICategoryStrategy categoryToSet)
        {
            _category = categoryToSet;
        }

        public int GetScore(int[] dice)
        {
            return  _category.Score(dice);
        }

    }
}