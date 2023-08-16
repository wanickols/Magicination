using TMPro;
using UnityEngine;

namespace Core
{
    public class EquippableOption : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textBox;
        public Equippable equippable { get; private set; }

        public void changeOption(Equippable _equippable)
        {
            equippable = _equippable;
            if (_equippable != null)
                upadateText();
            else
                textBox.text = string.Empty;
        }
        public void clear()
        {
            equippable = null;
            textBox.text = string.Empty;
        }
        /// Private

        private void upadateText() => textBox.text = equippable.Data.displayName;
    }
}