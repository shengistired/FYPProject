using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReturnDragDrop : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    public event Action<PointerEventData> OnBeginDragHandler;
    public event Action<PointerEventData> OnDragHandler;
    public event Action<PointerEventData, bool> OnEndDragHandler;
    private CanvasGroup canvasGroup;
    private UI_Inventory uiInventory;
    public bool FollowCursor { get; set; } = true;
    public Vector3 StartPosition;

    public bool CanDrag { get; set; } = true;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

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

            Debug.Log(dropArea);
            if (dropArea != null)
            {
                break;
            }
        }

        if (dropArea != null)
        {
            if (dropArea.AcceptsReturn(this))
            {
                dropArea.DropReturn(this);
                OnEndDragHandler?.Invoke(eventData, true);
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;

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
