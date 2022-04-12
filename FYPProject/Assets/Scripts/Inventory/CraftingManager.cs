using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;

public class CraftingManager : MonoBehaviour
{
    private CraftItem craftItem;
    [SerializeField] private RectTransform[] craftSlot;
    private int index;
    private Item itemDrag;
    public void SetCraftItem(CraftItem craftItem)
    {
        this.craftItem = craftItem;
        craftItem.OnItemListChanged += CraftManager_OnItemListChange;
        RefreshCrafttItem();
    }

    private void CraftManager_OnItemListChange(object sender, EventArgs e)
    {
        RefreshCrafttItem();
    }


    private void RefreshCrafttItem()
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
        foreach (Transform slot in craftSlot)
        {
            Image image = slot.Find("Image").GetComponent<Image>();
            TextMeshProUGUI uiText = slot.Find("amountText").GetComponent<TextMeshProUGUI>();

            try
            {
                if (slot.name == "CraftSlotTemplate")
                {
                    index = 0;
                }
                else
                {
                    index = 1;
                }

                item = craftItem.GetCraftItem(index);

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
                    uiText.SetText("");
                    image.color = new Color32(255, 255, 255, 0);
                    image.sprite = null;
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
    private void ItemDragged(Item item)
    {
        itemDrag = item;
    }
}
