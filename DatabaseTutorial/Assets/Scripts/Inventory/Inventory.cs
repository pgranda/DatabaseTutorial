using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
        }
        Instance = this;
    }
    #endregion

    public delegate void OnItemChanged();

    public OnItemChanged OnItemChangedCallback;

    public int Space = 20;

    public List<Item> Items = new List<Item>();

    public bool Add(Item item)
    {
        if (!item.IsDefaultItem)
        {
            if (Items.Count >= Space)
            {
                Debug.Log("Not enough room");
                return false;
            }
            Items.Add(item);
            if (OnItemChangedCallback != null)
            {
                OnItemChangedCallback.Invoke();
            }
        }
        return true;
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
    }

}
