using System;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    public event EventHandler OnItemListChanged;
    private Item equipment;
    private Action<Item> useItemAction;
    private Inventory inventory;


    public Equipment(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        equipment = new Item();

    }

    public void AddItem(Item item)
    {

        if (item.isStackable())
        {
            bool itemAlreadyInInventory = true;


            if (equipment.itemType == item.itemType)
                {
                    equipment.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            
            if (!itemAlreadyInInventory)
            {
                equipment = item;

            }
   
        }
        else
        {

            equipment = item;
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public void RemoveItem(Item item)
    {
        if (item.isStackable())
        {
            Item itemInEquipment = null;

                if (equipment.itemType == item.itemType)
                {
                    equipment.amount -= item.amount;
                    itemInEquipment = equipment;
                }
            
            if (itemInEquipment != null && itemInEquipment.amount <= 0)
            {
                equipment = null;
            }

        }
        else
        {
            equipment = null;
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public void MoveItem(Item item)
    {

        equipment = null;

        OnItemListChanged?.Invoke(this, EventArgs.Empty);

    }
    public void UseEquipment(Item item)
    {
        useItemAction(item);
    }

    public Item GetEquipment()
    {
        return equipment;
    }
}
