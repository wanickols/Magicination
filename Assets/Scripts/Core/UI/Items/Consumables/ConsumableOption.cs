using TMPro;
using UnityEngine;

namespace Core
{
    public class ConsumableOption : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textBox;
        [SerializeField] private TextMeshProUGUI qauntityText;
        public Consumable consumable { get; private set; }

        public void changeOption(Consumable _consumable, int quantity)
        {
            consumable = _consumable;
            if (_consumable != null)
                upadateText(quantity);
            else
                textBox.text = string.Empty;
        }
        public void clear()
        {
            consumable = null;
            textBox.text = string.Empty;
        }
        /// Private
        private void upadateText(int quantity)
        {
            textBox.text = consumable.Data.displayName;
            qauntityText.text = quantity.ToString();
        }

    }
}