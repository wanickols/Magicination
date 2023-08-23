using UnityEngine;

namespace Core
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

        public void Consume(PartyMember consumer)
        {
            if (data.augData != null)
            {
                Augmentation aug = data.augData.CreateAugmentation(consumer);
                aug.ApplyEffect();
            }
            Party.bag.Remove(this);
        }
    }
}