using UnityEngine;

namespace MGCNTN
{
    public abstract class Item : ScriptableObject
    {
        public abstract ItemData Data { get; set; }
    }
}
