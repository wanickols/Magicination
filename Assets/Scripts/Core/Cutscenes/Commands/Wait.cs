using System.Collections;
using UnityEngine;


namespace Core
{
    [System.Serializable]
    public class Wait : ICutCommand
    {

        [SerializeField] private float seconds = 0;

        public bool isFinished { get; private set; }

        public IEnumerator CO_Execute()
        {
            yield return new WaitForSeconds(seconds);
        }

        public override string ToString() => "Wait For Seconds";
    }
}
