using System;

namespace Yatzy
{
    public class Die
    {
        private const int MinimumFaceValue = 1;
        private const int MaximumFaceValue = 6;
        
        public int FaceValue { get; private set; }
        
        private readonly Random _random;
        public Die()
        {
            _random = new Random();
            FaceValue = _random.Next(MinimumFaceValue, MaximumFaceValue + 1);
        }
        
        public void Roll()
        {
            FaceValue = _random.Next(MinimumFaceValue, MaximumFaceValue + 1);
        }
    }
}