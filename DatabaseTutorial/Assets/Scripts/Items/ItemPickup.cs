using UnityEngine;

public class ItemPickup : Interactable
{
    public Item Item;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picked up " + Item.Name);
        bool wasPickedUp = Inventory.Instance.Add(Item);
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }
}
