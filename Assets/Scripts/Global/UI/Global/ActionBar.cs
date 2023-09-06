using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN
{
    public class ActionBar : MonoBehaviour
    {
        ///Private Paremeters
        [SerializeField] private Selector selector;

        private List<RectTransform> selectables = new List<RectTransform>();

        ///Unity Functions
        private void Awake()
        {
            selectables = selector.getSelectables();
        }

        private void OnEnable()
        {
            foreach (RectTransform t in selectables)
                t.gameObject.SetActive(true);
        }


        ///Public Functions
        public void deactivateOption(int i) => selectables[i].gameObject.SetActive(false);
    }
}
