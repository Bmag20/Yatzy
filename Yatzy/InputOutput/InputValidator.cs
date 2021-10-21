using System;

namespace Yatzy.InputOutput
{
    public static class InputValidator
    {
        public static bool IsQuit(string playerInput)
        {
            return playerInput.ToLower() == "q";
        }        
        
        public static bool IsYes(string playerInput)
        {
            return playerInput.ToLower() == "y";
        }
        
        public static bool IsValidValue(int playerInput, int upperBound)
        {
            return playerInput > 0 && playerInput < upperBound;
        }
    }
}