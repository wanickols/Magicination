using System;
using UnityEngine;

[Serializable]
public class ItemData
{
    [SerializeField] public int id;
    [SerializeField] public int price;
    [SerializeField] public string displayName;
    [SerializeField] protected string description;
    [SerializeField] protected int quantity;


    [SerializeField] protected int requiredLevel;

    public static int nextID = 0;

    public ItemData()
    {
        nextID++;
        id = nextID;
    }

    //UI related stuff needs to be added
}
