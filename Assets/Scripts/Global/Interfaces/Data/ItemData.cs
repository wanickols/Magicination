using System;
using UnityEngine;

namespace MGCNTN
{
    [Serializable]
    public class ItemData : ObjectData
    {
        [Header("Items")]
        public int price;
        public static int nextID = 0;
        [HideInInspector]
        public int quantity = 1;

        public ItemData()
        {
            nextID++;
            id = nextID;
        }
    }
}