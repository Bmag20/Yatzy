using System.Collections.Generic;
using Yatzy.Player;

namespace Yatzy.CategoryStrategy.Utility
{
    public interface ICategoryUtil
    {
        public List<CategoryRecord> GenerateCategoryRecords();
        // public Dictionary<ICategoryStrategy, int> GetEach
    }
}