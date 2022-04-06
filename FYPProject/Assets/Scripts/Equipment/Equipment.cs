using System;

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
}
