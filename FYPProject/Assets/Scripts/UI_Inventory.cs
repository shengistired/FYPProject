using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{

    private Inventory inventory;
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemSlotTemplate;
    private PlayerMovement player;
    private static Item itemN;

    public void SetPlayer(PlayerMovement player)
    {

        this.player = player;

    }
    public void SetInventory (Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChange;
        RefreshInventoryItem();
    }

    private void Inventory_OnItemListChange(object sender, EventArgs e)
    {
        RefreshInventoryItem();
    }

    private void RefreshInventoryItem()
    {

        foreach(Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject); 
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 2f;

        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            
            itemSlotRectTransform.GetComponent<Button>().onClick.AddListener(delegate { click(item); });

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
            if(item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }
            x++;
            if(x> 4)
            {
                x = 0;
                y--;
            }
        }


    }

    public void click(Item item)
    {
            //inventory.UseItem(item);
            
            Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
            inventory.RemoveItem(item);
            Vector3 direction;
            if(player.getDirection() == 1)
            {
                direction = Vector3.right;
            }
            else
            {
                direction = Vector3.left;
            }
            ItemWorld.DropItem(direction, player.getPosition(),  duplicateItem);              
    }
}
