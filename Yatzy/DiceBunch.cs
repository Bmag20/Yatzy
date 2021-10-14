using System;
using System.Collections.Generic;
using System.Linq;

namespace Yatzy
{
    public class DiceBunch
    {
        private const int NumberOfDicePerRoll = 5;
        private const int MaximumRolls = 3;
        public Die[] Dice { get; private set; }
        public int Rolls { get; private set; }

        public DiceBunch()
        {
            Dice = new Die[NumberOfDicePerRoll];
            InitialiseDice();
        }

        private void InitialiseDice()
        {
            for (var i = 0; i < NumberOfDicePerRoll; i++)
                Dice[i] = new Die();
            Rolls = 1;
        }

        public int[] GetDiceValues()
        {
            return Dice.Select(die => die.FaceValue).ToArray();
        }

        public void ReRoll(int[] diceToReRoll)
        {
            if (!CanBeRolled())
                throw new InvalidOperationException("This dice bunch is rolled maximum times");
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