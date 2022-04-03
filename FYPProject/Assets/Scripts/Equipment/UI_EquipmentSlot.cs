using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_EquipmentSlot : MonoBehaviour
{
    int index;
    private Equipment equipment;
    [SerializeField] private Transform equipSlotTemplate;
    public Item itemDrag;
    int i = 0;
    public void SetEquipment(Equipment equipment)
    {
        this.equipment = equipment;
        equipment.OnItemListChanged += Equipment_OnItemListChange;
        RefreshEquipmentItem();
    }

    private void Equipment_OnItemListChange(object sender, EventArgs e)
    {
        RefreshEquipmentItem();
    }
    public void Refresh()
    {
        RefreshEquipmentItem();
    }

    private void RefreshEquipmentItem()
    {
        Item item;
        string name = equipSlotTemplate.name;
        Button button = equipSlotTemplate.GetComponent<Button>();
        if (name == "equipSlotTemplate2")
        {
            index = 0;
        }
        else if (name == "equipSlotTemplate3")
        {
            index = 1;
        }
        else if (name == "equipSlotTemplate4")
        {
            index = 2;
        }
        else if (name == "equipSlotTemplate5")
        {
            index = 3;
        }
        else if (name == "equipSlotTemplate6")
        {
            index = 4;
        }
        else if (name == "equipSlotTemplate7")
        {
            index = 5;
        }
        else if (name == "equipSlotTemplate8")
        {
            index = 6;
        }
        else if (name == "equipSlotTemplate9")
        {
            index = 7;
        }
        else if (name == "equipSlotTemplate10")
        {
            index = 8;
        }

        try
        {
            item = equipment.GetEquipment(index);
            Image image = equipSlotTemplate.Find("Image").GetComponent<Image>();

            if (item != null)
            {
                //button.onClick.AddListener(delegate { click(item, index); });
                

                EventTrigger trigger = equipSlotTemplate.GetComponent<EventTrigger>();

                var enter = new EventTrigger.Entry();
                enter.eventID = EventTriggerType.PointerDown;
                //  enter.callback.AddListener((e) => ItemDragged(item));
                trigger.triggers.Add(enter);



                image.sprite = item.GetSprite();
                //Debug.Log(equipSlotTemplate);
                TextMeshProUGUI uiText = equipSlotTemplate.Find("amountText").GetComponent<TextMeshProUGUI>();
                if (item.amount > 1)
                {
                    uiText.SetText(item.amount.ToString());
                }
                else
                {
                    uiText.SetText("");
                }
            }
            else
            {
                image.color = new Color32(255, 255, 255, 0);
                image.sprite = null;
            }

           

        }
        catch
        {

        }








    }
    public void click(Item item, int index)
    {
        //equipment.UseEquipment(item);

        Debug.Log("Number of Clicks " + i);
        Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
        equipment.RemoveItem(item, index);
        /*        Vector3 direction;
                if (player.getDirection() == 1)
                {
                    direction = Vector3.right;
                }
                else
                {
                    direction = Vector3.left;
                }
                ItemWorld.DropItem(direction, player.getPosition(), duplicateItem);
        */
        i++;
    }


    public void Move(Item item)
    {

        equipment.MoveItem(item);

    }

    private void ItemDragged(Item item)
    {
        Debug.Log(itemDrag);
        itemDrag = item;
    }
    public Item item()
    {
        return itemDrag;
    }
}
