namespace Yatzy.Player
{
    public class Player
    {
        public string PlayerName { get; set; }
        public int Score { get; set; }
        
        public IScoreCard ScoreCard { get; }
        
        public Player(string playerName, IScoreCard scoreCard)
        {
            PlayerName = playerName;
            ScoreCard = scoreCard;
            Score = 0;
        }
    }
}