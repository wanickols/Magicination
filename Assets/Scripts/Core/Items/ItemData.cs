using System;
using UnityEngine;

[Serializable]
public class ItemData
{
    public int id;
    public int price;
    public string displayName;
    public string description;
    public int quantity;
    public Sprite sprite;

    public int requiredLevel;

    public static int nextID = 0;

    public ItemData()
    {
        nextID++;
        id = nextID;
    }
}
