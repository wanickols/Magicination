using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class ItemMenu : MonoBehaviour
    {


        /// Private Parameters
        [SerializeField] private GameObject content;

        //Selection
        private itemMenuAction onSelectAction = itemMenuAction.use;
        private List<ConsumableOption> options = new List<ConsumableOption>();


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

        public void clearItems()
        {
            foreach (Transform t in content.transform)
            {
                ConsumableOption option = t.GetComponent<ConsumableOption>();
                option?.clear();
                t.gameObject.SetActive(false);
            }
        }

        public bool setFlag(int selected)
        {
            onSelectAction = (itemMenuAction)selected;

            if (onSelectAction == itemMenuAction.sort)
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
                case itemMenuAction.use: return use(index);
                case itemMenuAction.key: return key(index);
                default:
                    Debug.Log("Incorect Item Action Value");
                    break;
            }

            return null;
        }

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
    }
}