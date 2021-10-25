namespace Yatzy.Dice
{
    public interface IYatzyTurn
    {
        public Die[] Dice { get; }
        public int Rolls { get; }

        public void RollDice();
        public int[] GetDiceValues();
        public void ReRoll(int[] diceIndices);
        public bool CanBeRolled();

    }
}