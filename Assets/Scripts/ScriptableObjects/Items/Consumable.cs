using UnityEngine;

namespace MGCNTN
{
    [CreateAssetMenu(fileName = "Consumable", menuName = ("Items/Consumable"))]
    public class Consumable : Item, IConsumable
    {

        [SerializeField] private ItemData data = new ItemData();
        public override ItemData Data
        {
            get => data;
            set => data = value;
        }

        public void Consume() => Party.bag.Remove(this);
    }
}