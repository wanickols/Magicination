using Core;
using UnityEngine;

[CreateAssetMenu(fileName = "Equippable", menuName = ("Items/Equippable"))]
public class Equippable : Item, IEquippable
{

    //Serialized Fields
    [SerializeField] private EquipmentType type;
    [SerializeField] private equipmentEffect effect;
    [SerializeField] private EquippableData data;
    [SerializeField] protected EquippableStats stats;

    //Accessors
    public EquipmentType Type => type;
    public override ItemData Data
    {
        get { return data as EquippableData; }
        set { data = value as EquippableData; }
    }


    //Interface
    protected bool isEquipped = false;
    public bool IsEquipped => isEquipped;

    public void Equip() { }
    public void Unequip() { }
}
