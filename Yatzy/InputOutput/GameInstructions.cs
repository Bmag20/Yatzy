namespace Yatzy.InputOutput
{
    public static class GameInstructions
    {
        public static string WelcomeMessage() => "Welcome to Yatzy!!";
        public static string NumberOfPlayersPrompt() => "Please enter the number of players that wish to play";
        public static string ReEnterNumberOfPlayers()
        {
            return "Oh no! That's not permissible! The maximum number of players can be 10! \nPlease enter again ";
        }
        public static string PlayerNamePrompt() => "Please enter the player's name";
        public static string WantsToQuit() => "Please enter any key to roll the dice. Press (Q) to exit : ";
        public static string ReRollPrompt()
        {
            return "Would you like to re roll the dice?\nPress (Y) to re roll, any other key to keep the current roll: ";
        }
        public static string NumberToReRoll() => "Please enter the dice number to re roll. Any other key to end re rolling: ";
        public static string AskCategory() => "Please enter the category number you wish to play your roll: ";
        public static string InValidCategory() => "The current selection is invalid!!";
        public static string GameEnded() => "Game is ended! Thank you for playing :) ";
        
    }
}