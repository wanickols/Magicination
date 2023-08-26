using System;

namespace MGCNTN
{
    [Serializable]
    public class EquippableData : ItemData
    {
        public EquippableData() : base()
        {
            this.quantity = 1;
        }
    }
}
