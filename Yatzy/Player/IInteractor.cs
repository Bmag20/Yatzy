namespace Yatzy.Player
{
    public interface IInteractor
    {
        bool PlayerWantsToReRoll();
        int[] DiceIndicesToReRoll(int numberOfDicePerRoll);
        int CategoryIndexToPlaceRoll(int numberOfCategories);
        bool PlayerWantsToQuit();
    }
}