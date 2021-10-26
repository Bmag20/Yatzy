using System;
using System.Collections.Generic;
using Yatzy.Player;

namespace Yatzy.InputOutput
{
    public class ConsoleDisplay : IOutputHandler
    {
        public void Display(string text) => Console.WriteLine(text);
        public void DisplayWelcomeMessage()
        {
            Display(GameInstructions.WelcomeMessage());
        }

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

        public void DisplayGameEnded()
        {
            Display(GameInstructions.GameEnded());
        }

        public void DisplayReRollPrompt()
        {
            Display(GameInstructions.ReRollPrompt());
        }

        public void DisplayPlayedCategories(List<CategoryRecord> playedCategories)
        {
            Display("Played Categories : ");
            DisplayCategories(playedCategories);
        }

        public void DisplayUnPlayedCategories(List<CategoryRecord> unPlayedCategories)
        {
            Display("Categories to be played: ");
            DisplayCategories(unPlayedCategories);
        }

        public void DisplayWinners(List<IPlayer> winners)
        {
            if (winners is null || winners.Count <= 0) return;
            PrintNewLine();
            Console.WriteLine(winners.Count == 1 ? "Winner of the game is :" : "Winners of the game are :");
            foreach (var player in winners)
            {
                Console.WriteLine($"{player.PlayerName} with score {player.Score}");
            }
        }

        public void DisplayDice(int[] dice)
        {
            PrintNewLine();
            Console.WriteLine("Dice:");
            for (var i = 1; i <= dice.Length; i++)
                Console.Write($"#{i}    ");
            PrintNewLine();
            foreach (var die in dice)
                Console.Write($"[{die}]   ");
            PrintNewLine();
        }

        public void DisplayCategoryScores(List<CategoryRecord> categoryRecords)
        {
            PrintNewLine();
            int i = 0;
            categoryRecords.ForEach(_ => {
                Console.WriteLine($"({++i}) {categoryRecords[i-1].CategoryName}   : {categoryRecords[i-1].Score}"); });
            PrintNewLine();
        }

        public void PrintNewLine()
        {
            Console.WriteLine();
        }

        private void DisplayCategories(List<CategoryRecord> categoryRecords)
        {
            foreach (var categoryRecord in categoryRecords)
                Console.WriteLine(categoryRecord.CategoryName);
            PrintNewLine();
        }
    }
}