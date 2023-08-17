using Core;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenu : MonoBehaviour
{

    [SerializeField] private GameObject items;
    [SerializeField] private GameObject ConsumableOptionPrefab, DropDownGridSelectorPrefab;

    public Selector initItems()
    {
        foreach (KeyValuePair<Consumable, int> nom in Party.bag.consumables)
        {
            ConsumableOption option = Instantiate(ConsumableOptionPrefab, items.transform).GetComponent<ConsumableOption>();
            option.changeOption(nom.Key, nom.Value);
        }

        Selector selector = Instantiate(DropDownGridSelectorPrefab, items.transform).GetComponent<Selector>();
        selector.type = SelectorType.Grid;

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
}
