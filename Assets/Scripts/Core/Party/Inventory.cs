using System.Collections.Generic;

namespace Core
{
    public class Inventory
    {

        private Dictionary<Item, int> items = new Dictionary<Item, int>();

        public void Add(Item item, int quantity)
        {
            items.Add(item, quantity);
        }

        public void Remove(Item item)
        {
            items.Remove(item);
        }


    }
}