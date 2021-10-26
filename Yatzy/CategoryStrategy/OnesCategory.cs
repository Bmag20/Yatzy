using Yatzy.CategoryStrategy.Utility;

namespace Yatzy.CategoryStrategy
{
    public class OnesCategory : NumberPool
    {
        public OnesCategory()
        {
            SetFaceValue();
        }
        public sealed override void SetFaceValue()
        {
            FaceValue = 1;
        }
    }

}