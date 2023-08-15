using TMPro;
using UnityEngine;

namespace Core
{
    public class EquippableOption : MonoBehaviour
    {
        private TextMeshProUGUI textBox;
        public Equippable equippable { get; private set; }

        // Start is called before the first frame update
        void Awake() => textBox = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        public void changeOption(Equippable _equippable)
        {
            equippable = _equippable;
            upadateText();
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