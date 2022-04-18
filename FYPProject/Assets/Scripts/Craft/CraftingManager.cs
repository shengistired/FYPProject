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
    [SerializeField] private Craft_Slots[] craftSlotItems ;
    [SerializeField] private RectTransform resultSlot;
    [SerializeField] private Image resultImage;
    [SerializeField] private Sprite cross;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Button craftButton;
    private int index;
    private Item itemDrag;
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



    private void RefreshCrafttItem()
    {

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

                    image.color = new Color32(255, 255, 255, 0);
                    image.sprite = null;
                

            }


        }
        craftButton.onClick.AddListener(CheckForCreateRecipe);
       // CheckForCreateRecipe();




    }
    void CheckForCreateRecipe()
    {
        string currentRecipeString = "";
        resultImage = resultSlot.Find("Image").GetComponent<Image>();

        foreach (Item item in craftItem.GetCraftItemList())
        {
            if(item!= null)
            {
                currentRecipeString += item.itemType.ToString();
            }
            else
            {
                currentRecipeString += "empty";
                Debug.Log(currentRecipeString);

            }
        }

            for (int i = 0; i < recipes.Length; i++)
            {
                if (recipes[i] == currentRecipeString)
                {

                    resultImage.sprite = recipeResults[i].GetSprite();
                    resultImage.color = new Color32(200, 200, 200, 200);
                    player.CraftSuccessful(recipeResults[i]);
                    craftSlotItems[0].item = null;
                    craftSlotItems[1].item = null;

                }

            
        }





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
        if(craft.item != null)
        {
            player.CraftToInventory(craftIndex, craft.item);
            craft.item = null;
        }

    }


    private void ItemDragged(Item item)
    {
        itemDrag = item;
    }
}
