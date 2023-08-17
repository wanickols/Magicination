using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class ItemBag
    {
        public Dictionary<Consumable, int> consumables { get; private set; } = new Dictionary<Consumable, int>();
        /// Public Functions
        public void Add(Consumable consumable)
        {

            if (consumables.ContainsKey(consumable))
                consumables[consumable] += consumable.Data.quantity;
            else
                consumables.Add(consumable, 1);

        }

        public void Remove(Consumable consumable)
        {
            if (!consumables.ContainsKey(consumable))
            {
                Debug.Log($"Consumable Item Not Found to Consume: {consumable}");
                return;
            }

            consumables[consumable] -= consumable.Data.quantity;

            if (consumables[consumable] <= 0)
                consumables.Remove(consumable);

        }
    }
}