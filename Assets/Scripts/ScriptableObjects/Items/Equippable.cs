using UnityEngine;

namespace MGCNTN
{

    [CreateAssetMenu(fileName = "Equippable", menuName = ("Items/Equippable"))]
    public class Equippable : Item, IEquippable
    {

        //Serialized Fields
        [SerializeField] private EquippableType type;
        [SerializeField] private equipmentEffect effect;
        [SerializeField] private EquippableData data = new EquippableData();
        [SerializeField] protected EquippableStats stats = new EquippableStats();

        //Accessors
        public EquippableType Type => type;
        public string DisplayName => data.displayName;
        public override ItemData Data
        {
            get { return data as EquippableData; }
            set { data = value as EquippableData; }
        }

        public EquippableStats Stats => stats;


        //Interface
        protected bool isEquipped = false;
        public bool IsEquipped => isEquipped;

        public void Equip() { }
        public void Unequip() { }

        public void setType(EquippableType type) => this.type = type;
    }
}