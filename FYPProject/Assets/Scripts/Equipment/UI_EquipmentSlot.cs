using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_EquipmentSlot : MonoBehaviour
{
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

            equipSlotTemplate = GetComponent<RectTransform>();
             Debug.Log(equipSlotTemplate);

             Item item = equipment.GetEquipment();
            Debug.Log(item.itemType);
            equipSlotTemplate.GetComponent<Button>().onClick.AddListener(delegate { click(item); });

            Image image = equipSlotTemplate.Find("Image").GetComponent<Image>();

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
    public void click(Item item)
    {
        equipment.UseEquipment(item);

        Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
        equipment.RemoveItem(item);
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
