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
        public int quantity;

        public ItemData()
        {
            nextID++;
            id = nextID;
        }
    }
}