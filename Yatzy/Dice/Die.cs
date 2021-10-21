using System;

namespace Yatzy.Dice
{
    public class Die
    {
        private const int MinimumFaceValue = 1;
        private const int MaximumFaceValue = 6;
        
        public int FaceValue { get; private set; }
       // public bool Held { get; set; }
        
        private readonly Random _random;
        public Die()
        {
            _random = new Random();
        }
        
        public void Roll()
        {
          //  if(!Held)
                FaceValue = _random.Next(MinimumFaceValue, MaximumFaceValue + 1);
        }
    }
}