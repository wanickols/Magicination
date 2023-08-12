public interface IEquippable
{
    bool IsEquipped { get; }

    void Equip();
    void Unequip();
}
