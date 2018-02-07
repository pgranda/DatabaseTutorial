using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform ItemsParent;
    public GameObject InventoryUiGameObject;

    private Inventory inventory;
    private InventorySlot[] slots;

	void Start ()
	{
	    inventory = Inventory.Instance;
	    inventory.OnItemChangedCallback += UpdateUI;

	    slots = ItemsParent.GetComponentsInChildren<InventorySlot>();
	}
	
	void Update ()
    {
	    if (Input.GetButtonDown("Inventory"))
	    {
	        InventoryUiGameObject.SetActive(!InventoryUiGameObject.activeSelf);
	    }
	}

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.Items.Count)
            {
                slots[i].AddItem(inventory.Items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
