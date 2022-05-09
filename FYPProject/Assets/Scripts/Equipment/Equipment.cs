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
    public string description;

    public Equipment(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        equipment = new Item[10];

        AddItem(new Item { itemType = Item.ItemType.Weapon, amount = 1 }, 0);
        AddItem(new Item { itemType = Item.ItemType.Axe, amount = 1 , durablilty = 10}, 1);
        AddItem(new Item { itemType = Item.ItemType.PickAxe, amount = 2 , durablilty = 10}, 2);
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
    public void reduceDurability(Item item)
    {

        if (item.isAxe())
        {
            item.durablilty -= 1;
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



    public int filledArray()
    {
        int indexArray = -1;
        
        for (int i = 0; i < equipment.Length; i++)
        {
            if (equipment[i] == null)
            {
                
                return i;
            }

        }
        return indexArray;
    }

    public bool isFilled()
    {
        bool isFilled = false;
        if(filledArray() == -1)
        {
            isFilled = true;
        }
        return isFilled;
    }

    public string itemDescription(int index)
    {

            description = equipment[index].descriptionText();
            return description;
        
    }
    public List<Item> arrayToList()
    {
        List<Item> list = new List<Item>();
        foreach(Item item in equipment)
        {
            if(item != null)
            {
                list.Add(item);

            }
        }
        return list;
    }
}
