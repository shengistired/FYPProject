using System;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    public event EventHandler OnItemListChanged;
    private List<Item> equipmentList;
    private Action<Item> useItemAction;


    public Equipment(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        equipmentList = new List<Item>();

    }

    public void AddItem(Item item)
    {
        if (item.isStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item equipmentItem in equipmentList)
            {
                if (equipmentItem.itemType == item.itemType)
                {
                    equipmentItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                equipmentList.Add(item);
            }
        }
        else
        {
            equipmentList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public void RemoveItem(Item item)
    {
        if (item.isStackable())
        {
            Item itemInEquipment = null;
            foreach (Item inventoryItem in equipmentList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInEquipment = inventoryItem;
                }
            }
            if (itemInEquipment != null && itemInEquipment.amount <= 0)
            {
                equipmentList.Remove(itemInEquipment);
            }

        }
        else
        {
            equipmentList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseEquipment(Item item)
    {
        useItemAction(item);
    }

    public List<Item> GetEquipmentList()
    {
        return equipmentList;
    }
}
