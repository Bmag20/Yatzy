using Yatzy.InputOutput;

namespace Yatzy.Player
{
    public interface IPlayer
    {
        public string PlayerName { get; }
        public int Score { get; set; }
        public IScoreCard ScoreCard { get; }
        
        public IInteractor ResponseHandler { get; }
    }
}