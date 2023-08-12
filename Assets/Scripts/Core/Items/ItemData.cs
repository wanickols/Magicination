using System;
using UnityEngine;

[Serializable]
public class ItemData
{
    [SerializeField] public int id;
    [SerializeField] public int price;
    [SerializeField] protected string displayName;
    [SerializeField] protected string description;
    [SerializeField] protected int quantity;

    public string DisplayName => displayName;

    [SerializeField] protected int requiredLevel;

    public static int nextID = 0;

    public ItemData()
    {
        nextID++;
        id = nextID;
    }

    //UI related stuff needs to be added
}
