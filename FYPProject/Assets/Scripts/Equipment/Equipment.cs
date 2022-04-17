using System;
using System.Collections.Generic;


public class Equipment
{
    public event EventHandler OnItemListChanged;
    private Item[] equipment;
    private Action<Item> useItemAction;
    private Inventory inventory;
    public static bool addInventory;
    private Item previousEquipment;
    

    public Equipment(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        equipment = new Item[10];

        AddItem(new Item { itemType = Item.ItemType.Weapon, amount = 1 }, 0);
        AddItem(new Item { itemType = Item.ItemType.Axe, amount = 1 }, 1);
    }

    public bool AddItemCollide(Item item, int index)
    {
        bool added = false;
        for(int i = 0; i < equipment.Length; i++)
        {
            try
            {
                if (item.itemType == equipment[i].itemType)
                {
                    AddItem(item, i);
                    added = true;

                }
            }
            catch{

            }


        }
        return added;
    }
    public void AddItem(Item item, int index)
    {
        addInventory = false;
        if (item.isStackable())
        {
            if (equipment[index] != null)
            {
                if (equipment[index].itemType == item.itemType)
                {
                    equipment[index].amount += item.amount;

                }
                else
                {
                    previousEquipment = equipment[index];
                    addInventory = true;
                    equipment[index] = item;
                }

            }

            else
            {

                equipment[index] = item;

            }

        }
        else
        {
            if(equipment[index] != null)
            {
                previousEquipment = equipment[index];
                addInventory = true;
                equipment[index] = item;

            }
            else
            {
                equipment[index] = item;

            }
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public void RemoveItem(Item item, int index)
    {

        if (item.isStackable())
        {
            if (equipment[index] != null)
            {
                if (equipment[index].itemType == item.itemType)
                {

                    equipment[index].amount = item.amount - 1;
                    if (equipment[index].amount <= 0)
                    {
                        equipment[index] = null;
                    }

                }
            }



        }
        else
        {
            equipment[index] = null;
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public void MoveItem(Item item, int oldIndex, int newIndex)
    {

        if (item.isStackable())
        {
            if (equipment[newIndex] != null)
            {
                if (equipment[newIndex].itemType == item.itemType)
                {

                    equipment[newIndex].amount += item.amount;
                    equipment[oldIndex] = null;

                }
                else
                {
                    equipment[oldIndex] = equipment[newIndex];
                    equipment[newIndex] = item;
                }
            }
            else
            {
                equipment[newIndex] = equipment[oldIndex];
                equipment[oldIndex] = null;
            }


        }
        else
        {
            if (equipment[newIndex] != null)
            {
                equipment[oldIndex] = equipment[newIndex];
                equipment[newIndex] = item;
            }
            else
            {
                equipment[newIndex] = item;
                equipment[oldIndex] = null;


            }
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);

    }
    public void DeleteEquipment(int index)
    {
        equipment[index] = null;
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

    public Item previousItem()
    {
        return previousEquipment;
    }



    public List<int> filledList()
    {
        
        List<int> list = new List<int>();
        for (int i = 0; i < equipment.Length; i++)
        {
            if (equipment[i] == null)
            {
                list.Add(i);
            }

        }
        return list;
    }
}
