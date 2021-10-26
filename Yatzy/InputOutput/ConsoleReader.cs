using System;

namespace Yatzy.InputOutput
{
    public class ConsoleReader : IInputHandler
    {
        public string GetStringInput()
        {
            return Console.ReadLine();
        }
        
        public int GetNumericInput()
        {
            int result;
            try
            {
                result = int.Parse(Console.ReadLine() ?? string.Empty);
            }
            catch (FormatException)
            {
                result = -1;
            }
            return result;
        }
    }
}