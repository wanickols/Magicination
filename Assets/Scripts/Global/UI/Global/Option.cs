using TMPro;
using UnityEngine;

namespace MGCNTN
{
    public abstract class Option : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI textBox;

        public virtual void clear() => textBox.text = string.Empty;

        ///Private
        private void OnDisable() => clear();

        protected virtual void updateText(string text = null) => textBox.text = text;

    }
}