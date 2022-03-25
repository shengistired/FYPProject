using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_Inventory : MonoBehaviour
{

    private Inventory inventory;
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemSlotTemplate;
    public Item itemDrag;

    private PlayerMovement player;
    private DragAndDrop dragdrop;
    private int count = 0;
    public void SetPlayer(PlayerMovement player)
    {

        this.player = player;

    }
    public void DragDrop(DragAndDrop dragdrop)
    {

        this.dragdrop = dragdrop;

    }


    public void SetInventory(Inventory inventory)
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

        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 1.5f;

        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button>().onClick.AddListener(delegate { click(item); });

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);

            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();

            EventTrigger trigger = itemSlotRectTransform.GetComponent<EventTrigger>();
            var enter = new EventTrigger.Entry();
            enter.eventID = EventTriggerType.PointerDown;
            enter.callback.AddListener((e) => ItemDragged(item));
            trigger.triggers.Add(enter);



            image.sprite = item.GetSprite();
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();

            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }
            x++;
            if (x > 4)
            {
                x = 0;
                y--;
            }
        }


    }

    public void click(Item item)
    {
        inventory.UseItem(item);

        Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
        //inventory.RemoveItem(item);
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

        inventory.MoveItem(item);

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

    public void inventory_Position()
    {
        Vector3 centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.7f, 0.65f, 10f));
        transform.position = centerPos;
        if (Input.GetKeyDown("i"))
        {

            if (count == 0)
            {
                gameObject.SetActive(true);
                count = 1;
            }
            else
            {
                gameObject.SetActive(false);
                count = 0;

            }
        }
    }


}