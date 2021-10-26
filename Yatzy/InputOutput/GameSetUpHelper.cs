using Yatzy.Control;

namespace Yatzy.InputOutput
{
    public class GameSetUpHelper
    {
        
        private readonly IInputHandler _inputReader;
        private readonly IOutputHandler _outputWriter;

        public GameSetUpHelper(IInputHandler inputHandler, IOutputHandler outputHandler)
        {
            _inputReader = inputHandler;
            _outputWriter = outputHandler;
        }
        
        public int GetPlayerCount(int maximumPlayers)
        {
            _outputWriter.Display(GameInstructions.NumberOfPlayersPrompt());
            var input = _inputReader.GetNumericInput();
            while (!InputValidator.IsLessThan(input, maximumPlayers))
            {
                _outputWriter.Display(GameInstructions.ReEnterNumberOfPlayers());
                input = _inputReader.GetNumericInput();
            }
            return input;
        }

        public string GetPlayerName()
        {
            _outputWriter.Display(GameInstructions.PlayerNamePrompt());
            return _inputReader.GetStringInput();
        }
    }
}