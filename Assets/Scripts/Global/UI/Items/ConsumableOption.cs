using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MGCNTN
{
    public class ConsumableOption : MonoBehaviour
    {

        ///Public Parameters
        public Consumable consumable { get; private set; }

        ///Private Parameters
        [SerializeField] private TextMeshProUGUI textBox;
        [SerializeField] private TextMeshProUGUI qauntityText;
        [SerializeField] private Image icon;


        /// Unity Function
        private void OnDisable() => clear();

        ///Public Functions
        public void changeOption(Consumable _consumable, int quantity)
        {
            consumable = _consumable;
            icon.sprite = consumable.Data.sprite;
            upadateText(quantity);
        }


        ///Private Functions
        private void upadateText(int quantity)
        {
            if (consumable == null)
            {
                clear();
                return;
            }

            textBox.text = consumable.Data.displayName;
            qauntityText.text = quantity.ToString();
        }

        private void clear()
        {
            icon.sprite = null;
            consumable = null;
            textBox.text = string.Empty;
        }
    }
}