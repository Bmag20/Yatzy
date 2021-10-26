using Yatzy.CategoryStrategy.Utility;

namespace Yatzy.CategoryStrategy
{
    public class FoursCategory : NumberPool
    {
        public FoursCategory()
        {
            SetFaceValue();
        }
        public sealed override void SetFaceValue()
        {
            FaceValue = 4;
        }
    }
}