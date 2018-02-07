using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot EquipmentSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] CoveredMeshRegions;

    public int ArmorModifier;
    public int DamageModifier;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }

}

public enum EquipmentSlot
{
    Head,
    Chest,
    Legs,
    Weapon,
    Shield,
    Feet
};

public enum EquipmentMeshRegion
{
    Legs,
    Arms,
    Torso
};