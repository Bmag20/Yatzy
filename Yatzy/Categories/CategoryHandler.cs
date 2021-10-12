namespace Yatzy.Categories
{
    public class CategoryHandler
    {
        private ICategory _category;

        public void SetCategory(ICategory categoryToSet)
        {
            _category = categoryToSet;
        }

        public int GetScore(int[] dice)
        {
            return  _category.Score(dice);
        }
    }
}