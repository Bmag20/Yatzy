using Yatzy.InputOutput;

namespace Yatzy.Player
{
    public class YatzyPlayer : IPlayer
    {
        public string PlayerName { get; }
        public int Score { get; set; }
        
        public IScoreCard ScoreCard { get; }
        public IInteractor ResponseHandler { get; }

        public YatzyPlayer(string playerName, IScoreCard scoreCard, IInteractor responseGenerator)
        {
            PlayerName = playerName;
            ScoreCard = scoreCard;
            ResponseHandler = responseGenerator;
            Score = 0;
        }
    }
}