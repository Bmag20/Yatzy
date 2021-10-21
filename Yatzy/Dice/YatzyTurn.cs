using System;
using System.Linq;

namespace Yatzy.Dice
{
    public class YatzyTurn
    {
        private const int NumberOfDicePerRoll = 5;
        private const int MaximumRolls = 3;
        public Die[] Dice { get; private set; }
        public int Rolls { get; private set; }

        public YatzyTurn()
        {
            Dice = new Die[NumberOfDicePerRoll];
            for (int i = 0; i < NumberOfDicePerRoll; i++)
            {
                Dice[i] = new Die();
            }
            Rolls = 0;
        }

        public void RollDice()
        {
            for (var i = 0; i < NumberOfDicePerRoll; i++)
                Dice[i].Roll();
            Rolls = 1;
        }

        public int[] GetDiceValues()
        {
            return Dice.Select(die => die.FaceValue).ToArray();
        }

        public void ReRoll(int[] diceToReRoll)
        {
            if (!CanBeRolled())
                throw new InvalidOperationException("This dice bunch is rolled maximum times"); // custom
            foreach (var index in diceToReRoll)
            {
                Dice[index - 1].Roll();
            }
            Rolls++;
        }

        public bool CanBeRolled()
        {
            return Rolls < MaximumRolls;
        }
    }
}