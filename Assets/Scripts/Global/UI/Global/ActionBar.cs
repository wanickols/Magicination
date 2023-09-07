using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN
{
    public class ActionBar : MonoBehaviour
    {
        ///Private Paremeters
        [SerializeField] private Selector selector;

        private List<RectTransform> selectables => selector.getSelectables();

        ///Unity Functions

        private void OnEnable() => activateOptions();

        private void activateOptions()
        {
            foreach (RectTransform t in selectables)
                t.gameObject.SetActive(true);
        }
        ///Public Functions
        public void deactivateOption(int i)
        {
            selectables[i].gameObject.SetActive(false);
            selector.SelectedIndex = 0;
        }
    }
}
