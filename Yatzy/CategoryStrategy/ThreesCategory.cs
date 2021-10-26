using Yatzy.CategoryStrategy.Utility;

namespace Yatzy.CategoryStrategy
{
    public class ThreesCategory : NumberPool
    {
        public ThreesCategory()
        {
            SetFaceValue();
        }
        public sealed override void SetFaceValue()
        {
            FaceValue = 3;
        }
    }
}