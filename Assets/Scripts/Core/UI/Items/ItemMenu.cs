using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class ItemMenu : MonoBehaviour
    {

        /// Public Parameters
        public Selector actionSelector;

        /// Private Parameters
        [SerializeField] private GameObject items;
        [SerializeField] private GameObject ConsumableOptionPrefab;
        [SerializeField] private GameObject ScrollingGridSelectorPrefab;

        //Selection
        private itemMenuAction onSelectAction = itemMenuAction.use;
        private List<ConsumableOption> options = new List<ConsumableOption>();

        public Selector initItems()
        {
            foreach (KeyValuePair<Consumable, int> nom in Party.bag.consumables)
            {
                ConsumableOption option = Instantiate(ConsumableOptionPrefab, items.transform).GetComponent<ConsumableOption>();
                option.changeOption(nom.Key, nom.Value);
                options.Add(option);
            }

            Selector selector = Instantiate(ScrollingGridSelectorPrefab, items.transform).GetComponent<Selector>();
            selector.type = SelectorType.Grid;
            selector.gameObject.SetActive(false);
            return selector;
        }

        public void clearItems()
        {
            foreach (Transform t in items.transform)
            {
                ConsumableOption option = t.GetComponent<ConsumableOption>();
                option?.clear();
                Destroy(t.gameObject);
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