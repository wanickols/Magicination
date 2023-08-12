using System;

namespace Core
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
