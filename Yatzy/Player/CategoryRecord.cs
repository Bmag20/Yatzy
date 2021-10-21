using Yatzy.CategoryStrategy;

namespace Yatzy.Player
{
    public class CategoryRecord
    {
        public string CategoryName { get;}
        public ICategoryStrategy Category { get; set; }
        public int Score { get; set; }
        public bool Played { get; set; }
        public CategoryRecord(string categoryName, ICategoryStrategy category)
        {
            CategoryName = categoryName;
            Category = category;
            Score = 0;
            Played = false;
        }

    }
}