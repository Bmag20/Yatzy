using Yatzy.CategoryStrategy.Utility;

namespace Yatzy.CategoryStrategy
{
    public class SixesCategory : NumberPool
    {
        public SixesCategory()
        {
            SetFaceValue();
        }
        public sealed override void SetFaceValue()
        {
            FaceValue = 6;
        }
    }
}