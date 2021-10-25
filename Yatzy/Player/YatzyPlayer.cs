namespace Yatzy.Player
{
    public class YatzyPlayer : IPlayer
    {
        public string PlayerName { get; }
        public int Score { get; set; }
        
        public IScoreCard ScoreCard { get; }
        
        public YatzyPlayer(string playerName, IScoreCard scoreCard)
        {
            PlayerName = playerName;
            ScoreCard = scoreCard;
            Score = 0;
        }
    }
}