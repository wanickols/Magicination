using UnityEngine;

namespace MGCNTN
{
    public class DestroyOverTime : MonoBehaviour
    {
        ///Private Variables
        [SerializeField] private int timeToDestroyInFrames = 60;

        ///Unity Functions
        private void FixedUpdate()
        {
            if (--timeToDestroyInFrames < 0)
                Destroy(gameObject);
        }
    }
}