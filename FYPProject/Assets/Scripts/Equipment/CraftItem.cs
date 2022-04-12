using System;
using UnityEngine;

public class CraftItem
{
    public event EventHandler OnItemListChanged;
    private Item[] craftItem;
    private Inventory inventory;
    public static bool addInventory;
    private Item previouscraftItem;


    public CraftItem()
    {

        craftItem = new Item[2];
    }


    public void AddItem(Item item, int index)
    {
        addInventory = false;
        if (item.isStackable())
        {
            if (craftItem[index] != null)
            {
                if (craftItem[index].itemType == item.itemType)
                {
                    craftItem[index].amount += item.amount;

                }
                else
                {
                    previouscraftItem = craftItem[index];
                    addInventory = true;
                    craftItem[index] = item;
                }

            }

            else
            {
                craftItem[index] = item;

            }

        }
        else
        {
            if(craftItem[index] != null)
            {
                previouscraftItem = craftItem[index];
                addInventory = true;
                craftItem[index] = item;

            }
            else
            {
                craftItem[index] = item;

            }
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public void RemoveItem(int index)
    {
        Debug.Log(index);
        craftItem[index] = null;        

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public void MoveItem(Item item, int oldIndex, int newIndex)
    {

        if (item.isStackable())
        {
            if (craftItem[newIndex] != null)
            {
                if (craftItem[newIndex].itemType == item.itemType)
                {

                    craftItem[newIndex].amount += item.amount;
                    craftItem[oldIndex] = null;

                }
                else
                {
                    craftItem[oldIndex] = craftItem[newIndex];
                    craftItem[newIndex] = item;
                }
            }
            else
            {
                craftItem[newIndex] = craftItem[oldIndex];
                craftItem[oldIndex] = null;
            }


        }
        else
        {
            if (craftItem[newIndex] != null)
            {
                craftItem[oldIndex] = craftItem[newIndex];
                craftItem[newIndex] = item;
            }
            else
            {
                craftItem[newIndex] = item;
                craftItem[oldIndex] = null;


            }
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);

    }

    public Item GetCraftItem(int index)
    {
        return craftItem[index];
    }

    public Item[] GetCraftItemList()
    {
        return craftItem;
    }

    public Item previousItem()
    {
        return previouscraftItem;
    }
}
