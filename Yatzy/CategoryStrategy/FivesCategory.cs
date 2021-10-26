using Yatzy.CategoryStrategy.Utility;

namespace Yatzy.CategoryStrategy
{
    public class FivesCategory : NumberPool
    {
        public FivesCategory()
        {
            SetFaceValue();
        }
        public sealed override void SetFaceValue()
        {
            FaceValue = 5;
        }
    }
}