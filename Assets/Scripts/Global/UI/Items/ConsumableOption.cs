using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MGCNTN
{
    public class ConsumableOption : Option
    {

        ///Public Parameters
        public Consumable consumable { get; private set; }

        ///Private Parameters
        [SerializeField] private TextMeshProUGUI qauntityText;
        [SerializeField] private Image icon;

        ///Public Functions
        public void changeOption(Consumable _consumable, int quantity)
        {
            consumable = _consumable;
            icon.sprite = consumable.Data.sprite;
            updateText(quantity.ToString());
        }


        public override void clear()
        {
            base.clear();
            icon.sprite = null;
            consumable = null;
            qauntityText.text = string.Empty;
        }

        ///Private Functions
        protected override void updateText(string quantity)
        {
            if (consumable == null)
            {
                clear();
                return;
            }

            textBox.text = consumable.Data.displayName;
            qauntityText.text = quantity;
        }


    }
}