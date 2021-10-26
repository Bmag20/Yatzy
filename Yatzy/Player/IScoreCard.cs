using System.Collections.Generic;

namespace Yatzy.Player
{
    public interface IScoreCard // associated with each player
    {
        public List<CategoryRecord> Categories { get;}
        public bool AreAllCategoriesPlayed();
    }
}