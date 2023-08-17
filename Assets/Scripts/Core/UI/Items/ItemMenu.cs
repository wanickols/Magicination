using Core;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenu : MonoBehaviour
{

    [SerializeField] private GameObject items;
    [SerializeField] private GameObject ConsumableOptionPrefab, DropDownGridSelectorPrefab;
    [SerializeField] private GameObject itemWindow;

    public GridSelector initItems()
    {
        foreach (KeyValuePair<Consumable, int> nom in Party.bag.consumables)
        {
            ConsumableOption option = Instantiate(ConsumableOptionPrefab, items.transform).GetComponent<ConsumableOption>();
            option.changeOption(nom.Key, nom.Value);
        }

        GridSelector selector = Instantiate(DropDownGridSelectorPrefab, items.transform).GetComponent<GridSelector>();

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

    public void openItemWindow()
    {
        itemWindow.SetActive(true);
    }

    public void closeItemWindow()
    {
        itemWindow.SetActive(false);
    }
}
