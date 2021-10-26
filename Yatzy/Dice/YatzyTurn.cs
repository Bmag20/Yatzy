using System;
using System.Linq;

namespace Yatzy.Dice
{
    public class YatzyTurn
    {
        private readonly int _numberOfDicePerRoll;
        private readonly int _maximumRolls;
        public Die[] Dice { get; private set; }
        public int Rolls { get; private set; }

        public YatzyTurn(int numberOfDicePerRoll, int maximumRolls)
        {
            _numberOfDicePerRoll = numberOfDicePerRoll;
            _maximumRolls = maximumRolls;
            InitialiseDice();
            Rolls = 0;
        }

        private void InitialiseDice()
        {
            Dice = new Die[_numberOfDicePerRoll];
            for (var i = 0; i < _numberOfDicePerRoll; i++)
            {
                Dice[i] = new Die();
            }
        }

        public void RollDice()
        {
            for (var i = 0; i < _numberOfDicePerRoll; i++)
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

        public bool CanBeRolled() => Rolls < _maximumRolls;
    }
}