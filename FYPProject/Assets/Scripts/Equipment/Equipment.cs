using System;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    public event EventHandler OnItemListChanged;
    private Item[] equipment;
    private Action<Item> useItemAction;
    private Inventory inventory;


    public Equipment(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        equipment = new Item[9];

    }

  
    public void AddItem(Item item, int index)
    {


        if (item.isStackable())
        {
            if (equipment[index] != null)
            {
                if (equipment[index].itemType == item.itemType)
                {
                    equipment[index].amount += item.amount;

                }
            }

            else
            {

                equipment[index] = item;

            }

        }
        else
        {

            equipment[index] = item;
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public void RemoveItem(Item item, int index)
    {

        if (item.isStackable())
        {
            if (equipment[index].itemType == item.itemType)
                {
                Debug.Log("Before Remove :" + equipment[index].amount);
                    equipment[index].amount = item.amount - 1;
                Debug.Log("After Remove :" + equipment[index].amount);

            }

            if (equipment[index].amount <= 0)
            {
                equipment[index] = null;
            }

        }
        else
        {
            equipment[index] = null;
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

    public Item GetEquipment(int index)
    {
        return equipment[index];
    }

    public Item[] GetEquipmentList()
    {
        return equipment;
    }
}
