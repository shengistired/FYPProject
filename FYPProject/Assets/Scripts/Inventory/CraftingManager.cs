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
    [SerializeField] private Sprite cross;
    [SerializeField] private PlayerMovement player;
    private int index;
    private Item itemDrag;

    public List<Item> itemList;
    public string[] recipes;
    public Item[] recipeResults;
    

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



    void CheckForCreatedRecipe()
    {
        
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
                else if(slot.name == "CraftSlotTemplate1")
                {
                    index = 1;
                }
                else
                {
                    index = 2;
                }

                item = craftItem.GetCraftItem(index);

                if (item != null)
                {
                    Craft_Slots craftslot = slot.GetComponent<Craft_Slots>();
                    
                    slot.GetComponent<Button>().onClick.AddListener(delegate { click(craftslot); });



                    /*
                    EventTrigger trigger = slot.GetComponent<EventTrigger>();

                    var enter = new EventTrigger.Entry();
                    enter.eventID = EventTriggerType.PointerDown;
                    enter.callback.AddListener((e) => ItemDragged(item));
                    trigger.triggers.Add(enter);
                    */

                    image.color = new Color32(255, 255, 255, 255);
                    image.sprite = item.GetSprite();

                    if (item.amount > 1)
                    {
                        uiText.text = item.amount.ToString();
                    }

                    else
                    {
                        uiText.text = "";
                    }
                }
                else
                {
                    uiText.text = "";
                    image.color = new Color32(255, 255, 255, 0);
                    image.sprite = null;
                }

            }
            catch
            {
                if(index == 2)
                {
                    image.sprite = cross;

                }
                else
                {
                    image.color = new Color32(255, 255, 255, 0);
                    image.sprite = null;
                }

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
    public void click(Craft_Slots craft)
    {
        int craftIndex;
        if(craft.name == "CraftSlotTemplate")
        {
            craftIndex = 0;
        }
        else
        {
            craftIndex = 1;
        }
        player.CraftToInventory(craftIndex, craft.item);
    }
    private void ItemDragged(Item item)
    {
        itemDrag = item;
    }
}
