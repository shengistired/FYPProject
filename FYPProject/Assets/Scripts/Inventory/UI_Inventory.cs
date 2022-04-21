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
    [SerializeField] private Transform[] slotTemplate;

    [SerializeField] private GameObject craftingMenu;

    int index;
    public Item itemDrag;
    public Vector3 position;
    private PlayerController player;
    private DragAndDrop dragdrop;
    public static bool open = false;
    public void SetPlayer(PlayerController player)
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


        Item item;
        foreach (Transform slot in slotTemplate)
        {
            Transform itemSlot = slot.Find("Item").GetComponent<Transform>();
            Image image = itemSlot.Find("Image").GetComponent<Image>();
            TextMeshProUGUI uiText = itemSlot.Find("amountText").GetComponent<TextMeshProUGUI>();

            try
            {
                int stringLength = slot.name.Length;
                if(stringLength == 17)
                {
                    index = int.Parse(slot.name.Substring(slot.name.Length - 1));

                }
                else if (stringLength == 18)
                {
                    index = int.Parse(slot.name.Substring(slot.name.Length - 2));

                }
                item = inventory.GetItem(index);


                EventTrigger trigger = slot.GetComponent<EventTrigger>();

                var enter = new EventTrigger.Entry();
                var enter1 = new EventTrigger.Entry();
                var enter2 = new EventTrigger.Entry();
                enter.eventID = EventTriggerType.PointerDown;
                enter1.eventID = EventTriggerType.PointerEnter;
                enter2.eventID = EventTriggerType.PointerExit;
                enter.callback.AddListener((e) => ItemDragged(item));
                if (stringLength == 17)
                {
                    enter1.callback.AddListener((e) => ToolTip.ShowToolTip_Static(inventory.GetItem(int.Parse(slot.name.Substring(slot.name.Length - 1))).descriptionText()));


                }
                else if (stringLength == 18)
                {
                    enter1.callback.AddListener((e) => ToolTip.ShowToolTip_Static(inventory.GetItem(int.Parse(slot.name.Substring(slot.name.Length - 2))).descriptionText()));

                }
                enter2.callback.AddListener((e) => ToolTip.HideToolTip_Static());
                trigger.triggers.Add(enter);
                trigger.triggers.Add(enter1);
                trigger.triggers.Add(enter2);


                if (item != null)
                {
                    //button.onClick.AddListener(delegate { click(item, index); });

                    trigger.enabled = true;


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
                    trigger.enabled = false;

                }

            }
            catch
            {
                uiText.text = "";
                image.color = new Color32(255, 255, 255, 0);
                image.sprite = null;
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
    public void Move(int index)
    {

        inventory.MoveItem(index);

    }

    private void ItemDragged(Item item)
    {
        itemDrag = item;

        
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
            PlayerController.openCraft = true;
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
                PlayerController.openCraft = true;
                player.craftOpen();

            }
        }
    }

    


}