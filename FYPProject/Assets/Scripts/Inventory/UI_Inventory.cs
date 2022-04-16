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
    [SerializeField] private GameObject craftingMenu;
    public Item itemDrag;
    public Vector3 position;
    private PlayerMovement player;
    private DragAndDrop dragdrop;
    public static bool open = false;
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
    public void Refresh()
    {
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
        float itemSlotCellSize = 1.1f;

        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button>().onClick.AddListener(delegate { click(item); });

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);

            Vector3 pos = itemSlotRectTransform.position - player.getPosition();
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();

            EventTrigger trigger = itemSlotRectTransform.GetComponent<EventTrigger>();
            var enter = new EventTrigger.Entry();
            var enter1 = new EventTrigger.Entry();
            var enter2 = new EventTrigger.Entry();
            enter.eventID = EventTriggerType.PointerDown;
            enter1.eventID = EventTriggerType.PointerEnter;
            enter2.eventID = EventTriggerType.PointerExit;
            enter.callback.AddListener((e) => ItemDragged(item, pos));
            enter1.callback.AddListener((e) => ToolTip.ShowToolTip_Static(item.itemType.ToString()));
            enter2.callback.AddListener((e) => ToolTip.HideToolTip_Static());
            trigger.triggers.Add(enter);
            trigger.triggers.Add(enter1);
            trigger.triggers.Add(enter2);


            image.sprite = item.GetSprite();
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();

            if (item.amount > 1)
            {
                uiText.text = item.amount.ToString();
            }
            else
            {
                uiText.text = "";
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

    private void ItemDragged(Item item, Vector3 pos)
    {
        itemDrag = item;
        position = pos;

        
    }
    public Item item()
    {
        return itemDrag;
    }
    public Vector3 positionRect()
    {
        return position;
    }

    public void inventory_Position()
    {
        Vector3 centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.4f, 0.6f, 10f));
        transform.position = centerPos;
        if (Input.GetKeyDown(KeyCode.Escape) && open == true)
        {
            gameObject.SetActive(false);
            PlayerMovement.openCraft = true;
            player.craftOpen();
            open = false;
        }
        if (Input.GetKeyDown("i"))
        {

            if (open == false)
            {
                gameObject.SetActive(true);
                open = true ;
            }
            else
            {
                gameObject.SetActive(false);
                open = false;
                PlayerMovement.openCraft = true;
                player.craftOpen();

            }
        }
    }

    


}