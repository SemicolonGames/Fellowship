using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Inventory/Action Item"))]
public class ActionItem : InventoryItem
{
    [SerializeField] private bool consumable = false;

    public virtual bool Use(GameObject user)
    {
        Debug.Log("Usin Action: " + this);
        return this;
    }

    public bool isConsumable()
    {
        return consumable;
    }
}