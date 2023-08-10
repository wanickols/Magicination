using UnityEngine;

namespace Core
{

    public interface ITriggerByTouch
    {
        Vector2Int Cell { get; }

        void Trigger();
    }
}
