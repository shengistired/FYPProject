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


    private void RefreshEquipmentItem()
    {

        Item item;
        foreach (Transform slot in equipSlotTemplate)
        {
            Transform itemSlot = slot.Find("Item").GetComponent<Transform>();
            Image image = itemSlot.Find("Image").GetComponent<Image>();
            TextMeshProUGUI uiText = itemSlot.Find("amountText").GetComponent<TextMeshProUGUI>();
           
            try
            {
                index = int.Parse(slot.name.Substring(slot.name.Length - 1));
                item = equipment.GetEquipment(index);


                EventTrigger trigger = slot.GetComponent<EventTrigger>();

                var enter = new EventTrigger.Entry();
                var enter1 = new EventTrigger.Entry();
                var enter2 = new EventTrigger.Entry();
                enter.eventID = EventTriggerType.PointerDown;
                enter1.eventID = EventTriggerType.PointerEnter;
                enter2.eventID = EventTriggerType.PointerExit;
                enter.callback.AddListener((e) => ItemDragged(item));
                enter1.callback.AddListener((e) => ToolTip.ShowToolTip_Static(equipment.GetEquipment(int.Parse(slot.name.Substring(slot.name.Length - 1))).descriptionText()));
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
