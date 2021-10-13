namespace Yatzy
{
    public class DiceBunch
    {
        private const int NumberOfDicePerRoll = 5;
        public Die[] Dice { get; private set; }

        public DiceBunch()
        {
            Dice = new Die[NumberOfDicePerRoll];
            InitialiseDice();
        }

        private void InitialiseDice()
        {
            for (var i = 0; i < NumberOfDicePerRoll; i++)
                Dice[i] = new Die();
        }
        

    }
}