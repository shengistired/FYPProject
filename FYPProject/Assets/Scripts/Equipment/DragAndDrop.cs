using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    public event Action<PointerEventData> OnBeginDragHandler;
    public event Action<PointerEventData> OnDragHandler;
    public event Action<PointerEventData, bool> OnEndDragHandler;
    private GameObject go;
    private CanvasGroup canvasGroup;
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private PlayerMovement player;
    private Item item;
    public bool FollowCursor { get; set; } = true;
    public Vector3 StartPosition;
    public static int index = -1;
    public bool CanDrag { get; set; } = true;
    private void Awake()
    {
        
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

    }
    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Potion:
               // equipment.RemoveItem(new Item { itemType = Item.ItemType.Potion, amount = 1 });
                break;

            case Item.ItemType.Food:
               // equipment.RemoveItem(new Item { itemType = Item.ItemType.Food, amount = 1 });
                break;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        item = uiInventory.item();

        //go = Instantiate(gameObject);
        //go.transform.position = GetComponent<RectTransform>().position;

        if (!CanDrag)
        {
            return;

        
        }
        
        OnBeginDragHandler?.Invoke(eventData);
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!CanDrag)
        {
            return;

        }
        OnDragHandler?.Invoke(eventData);
        if (FollowCursor)
        {
            rectTransform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10f));

        }


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!CanDrag)
        {
            return;
        }

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        DropArea dropArea = null;

        foreach (var result in results)
        {

            dropArea = result.gameObject.GetComponentInParent<DropArea>();

            if (dropArea != null)
            {
                break;
            }
        }

        if (dropArea != null)
        {
            if (dropArea.Accepts(this))
            {
                string name = dropArea.GetComponentInParent<EquipmentSlot>().name;

                dropArea.Drop(this);
                OnEndDragHandler?.Invoke(eventData, true);
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;

                uiInventory.Move(item);
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

                // equipment = PlayerMovement.equipment;
                // equipment.AddItem(item, index);

                //uiEquipmentList[index].SetEquipment(PlayerMovement.equipment);
                player.AddEquipment(item, index);
                return;
            }
        }
        rectTransform.position = StartPosition;
        OnEndDragHandler?.Invoke(eventData, false);
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        StartPosition = rectTransform.position;

    }

    public int indexReturn()
    {
        return index;
    }
}
