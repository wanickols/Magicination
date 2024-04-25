using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Battle
{
    public class StatusUI : MonoBehaviour
    {
        [SerializeField] private GameObject row;

        private Dictionary<int, bool> setActive = new Dictionary<int, bool>();

        private void Awake()
        {
            // Initialize the dictionary with keys 0 to 5 and set their values to false
            for (int i = 0; i < row.transform.childCount; i++)
                setActive.Add(i, false);
        }
        public void setActivated(int index) => setActive[index] = true;
        public void Activate()
        {
            for (int i = 0; i < row.transform.childCount; i++)
            {
                row.transform.GetChild(i).gameObject.SetActive(setActive[i]);
                setActive[i] = false;
            }
        }

    }
}
