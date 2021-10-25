using System;
using System.Collections.Generic;
using Yatzy.Player;

namespace Yatzy.InputOutput
{
    public class YatzyDisplay : IOutputHandler
    {
        public void Display(string text) => Console.WriteLine(text);
        public void PlayerTurn(IPlayer player)
        {
            PrintNewLine();
            Console.WriteLine($"{player.PlayerName}'s turn");
            Console.WriteLine($"Current Score - {player.Score}");
            PrintNewLine();
        }

        public void PlayerScore(IPlayer player)
        {
            PrintNewLine();
            Console.WriteLine($"{player.PlayerName}'s total score is {player.Score}");
            PrintNewLine();
        }
        
        public void PlayerAbandoned(IPlayer player)
        {
            PrintNewLine();
            Console.WriteLine($"{player.PlayerName} abandoned!");
            PrintNewLine();
        }

        public void DisplayFinalScores(List<IPlayer> players)
        {
            PrintNewLine();
            Console.WriteLine("Player final scores");
            foreach (var player in players)
                Console.WriteLine($"{player.PlayerName} - {player.Score}");
            PrintNewLine();
        }

        public void DisplayDice(int[] dice)
        {
            PrintNewLine();
            Console.WriteLine("Dice:");
            Console.WriteLine("#1    #2    #3    #4    #5");
            foreach (var die in dice)
                Console.Write($"[{die}]   ");
            PrintNewLine();
        }

        public void DisplayCategoryScores(List<CategoryRecord> categoryRecords)
        {
            PrintNewLine();
            int i = 0;
            categoryRecords.ForEach(category => {
                Console.WriteLine($"#{++i} {categoryRecords[i-1].CategoryName}   : {categoryRecords[i-1].Score}"); });
            PrintNewLine();
        }

        public void PrintNewLine()
        {
            Console.WriteLine();
        }

        public void DisplayCategories(List<CategoryRecord> categoryRecords)
        {
            foreach (var categoryRecord in categoryRecords)
                Console.WriteLine(categoryRecord.CategoryName);
            PrintNewLine();
        }
    }
}