namespace Yatzy.Player
{
    public interface IPlayer
    {
        public string PlayerName { get; }
        public int Score { get; set; }
        
        public IScoreCard ScoreCard { get; }
    }
}