using System.Collections.Generic;
using Yatzy.Player;

namespace Yatzy.InputOutput
{
    public interface IOutputHandler
    {
        void Display(string text);
        void DisplayWelcomeMessage();
        void PlayerTurn(IPlayer player);
        void PlayerScore(IPlayer player);
        void DisplayDice(int[] dice);
        void DisplayCategoryScores(List<CategoryRecord> categoryRecords);
        void PrintNewLine();
        void PlayerAbandoned(IPlayer player);
        void DisplayFinalScores(List<IPlayer> players);
        void DisplayGameEnded();
        void DisplayReRollPrompt();
        void DisplayPlayedCategories(List<CategoryRecord> playedCategories);
        void DisplayUnPlayedCategories(List<CategoryRecord> unPlayedCategories);
        void DisplayWinners(List<IPlayer> winners);
    }
}