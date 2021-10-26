using System.Collections.Generic;
using Yatzy.InputOutput;

namespace Yatzy.Player
{
    public class HumanInteractor : IInteractor
    {
        private readonly IInputHandler _inputReader;
        private readonly IOutputHandler _outputWriter;

        public HumanInteractor(IInputHandler inputHandler, IOutputHandler outputHandler)
        {
            _inputReader = inputHandler;
            _outputWriter = outputHandler;
        }
        
        public bool PlayerWantsToReRoll()
        {
            return InputValidator.IsYes(_inputReader.GetStringInput());
        }

        public int[] DiceIndicesToReRoll(int numberOfDicePerRoll)
        {
            var reRollDice = new List<int>();
            while (true)
            {
                _outputWriter.Display(GameInstructions.NumberToReRoll());
                var playerInput = _inputReader.GetNumericInput();
                if (InputValidator.IsLessThan(playerInput, numberOfDicePerRoll))
                    reRollDice.Add(playerInput);
                else
                    return reRollDice.ToArray();
            }
        }

        public int CategoryIndexToPlaceRoll(int numberOfCategories)
        {
            while(true)
            {
                _outputWriter.Display(GameInstructions.AskCategory());
                var playerInput = _inputReader.GetNumericInput();
                if (InputValidator.IsLessThan(playerInput, numberOfCategories))
                    return playerInput - 1;
                _outputWriter.Display(GameInstructions.InValidCategory());
            }
        }

        public bool PlayerWantsToQuit()
        {
            _outputWriter.Display(GameInstructions.WantsToQuit());
            var playerInput = _inputReader.GetStringInput();
            return InputValidator.IsQuit(playerInput);
        }

    }
}