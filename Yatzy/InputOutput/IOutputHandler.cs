using System.Collections.Generic;
using Yatzy.Player;

namespace Yatzy.InputOutput
{
    public interface IOutputHandler
    {
        public void Display(string text);
        void PlayerTurn(IPlayer player);

        void PlayerScore(IPlayer player);
        public void DisplayDice(int[] dice);
        void DisplayCategoryScores(List<CategoryRecord> categoryRecords);
        public void PrintNewLine();
        void DisplayCategories(List<CategoryRecord> getPlayedCategories);
        void PlayerAbandoned(IPlayer player);
        void DisplayFinalScores(List<IPlayer> players);
    }
}