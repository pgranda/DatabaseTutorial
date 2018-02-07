using System.Linq.Expressions;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public Equipment[] DefaultEquipment;

    private Equipment[] currentEquipment;

    private SkinnedMeshRenderer[] currentMeshes;

    public SkinnedMeshRenderer TargetMesh;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);

    public OnEquipmentChanged DlgOnEquipmentChanged;

    private Inventory inventory;

    void Start()
    {
        inventory = Inventory.Instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipDefaultItems();
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.EquipmentSlot;

        Equipment oldItem = Unequip(slotIndex);

        if (DlgOnEquipmentChanged != null)
        {
            DlgOnEquipmentChanged.Invoke(newItem, oldItem);
        }

        SetEquipmentBlendShapes(newItem, 100);

        currentEquipment[slotIndex] = newItem;

        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = TargetMesh.transform;

        newMesh.bones = TargetMesh.bones;
        newMesh.rootBone = TargetMesh.rootBone;

        currentMeshes[slotIndex] = newMesh;
    }

    public Equipment Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }
            Equipment oldItem = currentEquipment[slotIndex];

            SetEquipmentBlendShapes(oldItem, 0);

            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (DlgOnEquipmentChanged != null)
            {
                DlgOnEquipmentChanged.Invoke(null, oldItem);
            }
            return oldItem;
        }
        return null;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
        EquipDefaultItems();
    }

    void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach (EquipmentMeshRegion blendShape in item.CoveredMeshRegions)
        {
            TargetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    void EquipDefaultItems()
    {
        foreach (Equipment item in DefaultEquipment)
        {
            Equip(item);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
