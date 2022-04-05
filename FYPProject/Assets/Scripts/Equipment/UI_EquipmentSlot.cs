using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_EquipmentSlot : MonoBehaviour
{
    int index;
    private Equipment equipment;
  //  [SerializeField] private Transform equipSlotTemplate;
    [SerializeField] private Transform[] equipSlotTemplate;
  //  [SerializeField] private Transform itemTemplate;
    public Item itemDrag;
    int i = 0;

    private void Awake()
    {
        
    }
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
        /*
        if (name == "equipSlotTemplate1")
        {
            index = 0;
        }
        else if (name == "equipSlotTemplate2")
        {
            index = 1;
        }
        else if (name == "equipSlotTemplate3")
        {
            index = 2;
        }
        else if (name == "equipSlotTemplate4")
        {
            index = 3;
        }
        else if (name == "equipSlotTemplate5")
        {
            index = 4;
        }
        else if (name == "equipSlotTemplate6")
        {
            index = 5;
        }
        else if (name == "equipSlotTemplate7")
        {
            index = 6;
        }
        else if (name == "equipSlotTemplate8")
        {
            index = 7;
        }
        else if (name == "equipSlotTemplate9")
        {
            index = 8;
        }
        else if (name == "equipSlotTemplate10")
        {
            index = 9;
        }
        Debug.Log(index);
        */
        Item item;
        foreach(Transform slot in equipSlotTemplate)
        {
            Transform itemSlot = slot.Find("Item").GetComponent<Transform>();
            Image image = itemSlot.Find("Image").GetComponent<Image>();

            try
            {
                index = int.Parse(slot.name.Substring(slot.name.Length - 1));
                index -= 1;
                item = equipment.GetEquipment(index);


                if (item != null)
                {
                    //button.onClick.AddListener(delegate { click(item, index); });
                    

                    EventTrigger trigger = slot.GetComponent<EventTrigger>();

                    var enter = new EventTrigger.Entry();
                    enter.eventID = EventTriggerType.PointerDown;
                    enter.callback.AddListener((e) => ItemDragged(item));
                    trigger.triggers.Add(enter);


                    image.color = new Color32(255, 255, 255, 255);
                    image.sprite = item.GetSprite();

                    TextMeshProUGUI uiText = itemSlot.Find("amountText").GetComponent<TextMeshProUGUI>();
                    if (item.amount > 1)
                    {
                        uiText.SetText(item.amount.ToString());
                    }

                    else
                    {
                        uiText.SetText("");
                    }
                }

            }
            catch
            {
                image.color = new Color32(255, 255, 255, 0);
                image.sprite = null;
            }
         

        }
        /*
        try
        {
            item = equipment.GetEquipment(index);
            Image image = itemTemplate.Find("Image").GetComponent<Image>();

            Debug.Log("Index " + index + " " + item.itemType);
            if (item != null)
            {
                //button.onClick.AddListener(delegate { click(item, index); });
                

                EventTrigger trigger = equipSlotTemplate.GetComponent<EventTrigger>();

                var enter = new EventTrigger.Entry();
                enter.eventID = EventTriggerType.PointerDown;
                //  enter.callback.AddListener((e) => ItemDragged(item));
                trigger.triggers.Add(enter);


                image.color = new Color32(255, 255, 255, 255);
                image.sprite = item.GetSprite();

                //Debug.Log(equipSlotTemplate);
                TextMeshProUGUI uiText = itemTemplate.Find("amountText").GetComponent<TextMeshProUGUI>();
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


        */





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
        i++;
    }


    public void Move(Item item)
    {

       // equipment.MoveItem(item);

    }

    private void ItemDragged(Item item)
    {
        itemDrag = item;
    }
    public Item item()
    {
        return itemDrag;
    }
}
