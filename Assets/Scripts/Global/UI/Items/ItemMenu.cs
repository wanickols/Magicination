using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MGCNTN
{
    public class ItemMenu : MonoBehaviour
    {
        /// Private Parameters
        [SerializeField] private GameObject content; //content window

        //Selection
        private ItemMenuAction onSelectAction = ItemMenuAction.use;
        private List<ConsumableOption> options = new List<ConsumableOption>();


        ///Unity Functions
        private void OnEnable() => initItems();
        private void OnDisable() => clearItems();

        /// Public Functions
        public void initItems()
        {
            int i = 0;
            foreach (Transform t in content.transform)
            {
                ConsumableOption option = t.GetComponent<ConsumableOption>();

                if (option == null)
                    continue;

                if (i < Party.bag.consumables.Count)
                {
                    t.gameObject.SetActive(true);
                    option.changeOption(Party.bag.consumables.Keys.ElementAt(i), Party.bag.consumables.Values.ElementAt(i));
                    options.Add(option);
                }
                else
                    t.gameObject.SetActive(false);

                i++;
            }
        }

        public bool setFlag(int selected)
        {
            if ((ItemMenuAction)selected == ItemMenuAction.sort)
            {
                sort();
                return false;
            }

            return true;
        }
        public Consumable selectItem(int index)
        {
            switch (onSelectAction)
            {
                case ItemMenuAction.use: return use(index);
                case ItemMenuAction.key: return key(index);
                default:
                    Debug.Log("Incorect Item Action Value");
                    return null;
            }
        }

        ///Private Functions
        private void sort()
        {
            Debug.Log("Sorted");
        }
        private Consumable use(int index)
        {
            return options[index].consumable;
        }
        private Consumable key(int index)
        {
            return options[index].consumable;
        }
        private void clearItems()
        {
            foreach (Transform t in content.transform)
                t.gameObject.SetActive(false);
        }
    }
}