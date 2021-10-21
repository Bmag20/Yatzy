using System.Collections.Generic;
using Yatzy.Player;

namespace Yatzy.InputOutput
{
    public interface IOutputHandler
    {
        public void Display(string text);
        void PlayerTurn(Player.Player player);

        void PlayerScore(Player.Player player);
        public void DisplayDice(int[] dice);
        void DisplayCategoryScores(List<CategoryRecord> categoryRecords);
        public void PrintNewLine();
        void DisplayCategories(List<CategoryRecord> getPlayedCategories);
    }
}