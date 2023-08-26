using UnityEngine;

namespace MGCNTN.Core
{
    public interface ITriggerByTouch
    {
        Vector2Int Cell { get; }

        void Trigger();
    }
}
