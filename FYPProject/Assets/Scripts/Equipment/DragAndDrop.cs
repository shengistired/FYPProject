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
    private Equipment equipment;
    private Item item;
    public bool FollowCursor { get; set; } = true;
    public Vector3 StartPosition;

    public bool CanDrag { get; set; } = true;
    private void Awake()
    {
        equipment = new Equipment(UseItem);


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
                dropArea.Drop(this);
                OnEndDragHandler?.Invoke(eventData, true);
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;

                equipment.AddItem(item);
                uiInventory.Move(item);

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
}
