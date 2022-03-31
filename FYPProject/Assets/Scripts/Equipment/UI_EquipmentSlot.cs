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

        equipSlotTemplate = GetComponent<RectTransform>();
        // Debug.Log("From UI_EquipmentSlot" + item.itemType);
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
        Debug.Log(index);
        item = equipment.GetEquipment(index);
        Debug.Log(item);


        equipSlotTemplate.GetComponent<Button>().onClick.AddListener(delegate { click(item, index); });

            Image image = equipSlotTemplate.Find("Image").GetComponent<Image>();
            Debug.Log("From UI_EquipmentSlot" + equipSlotTemplate);

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
    public void click(Item item, int index)
    {
        //equipment.UseEquipment(item);



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
