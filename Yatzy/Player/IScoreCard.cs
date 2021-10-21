using System.Collections.Generic;

namespace Yatzy.Player
{
    public interface IScoreCard
    {
        public List<CategoryRecord> Categories { get;}
        public bool AreAllCategoriesPlayed();
    }
}