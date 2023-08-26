using System.Collections;
using UnityEngine;

namespace MGCNTN
{
    public class CO : MonoBehaviour
    {
        public static CO co { get; private set; }

        private void Awake()
        {
            //Singleton implementations (yes I know issues with these)
            if (co != null && co != this)
                Destroy(this);
            else
                co = this;
        }

        public static void startCO(IEnumerator enumerator) => co.StartCoroutine(enumerator);
        public static void stopCO(IEnumerator enumerator) => co.StopCoroutine(enumerator);
        public static void stopALLCO() => co.StopAllCoroutines();
    }
}
