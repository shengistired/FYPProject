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
    private CanvasGroup canvasGroup;
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private PlayerController player;
    [SerializeField] private Transform slotTemplate;

    private bool isMining = false;
    private int indexInventory;
    public Item item;
    private Transform itemSlot;

    private GameObject go;
    public bool FollowCursor { get; set; } = true;
    public Vector3 StartPosition;
    public static int index = -1;
    public bool CanDrag { get; set; } = true;
    private void Awake()
    {
        
        itemSlot = slotTemplate.Find("Item").GetComponent<Transform>();
        canvasGroup = GetComponent<CanvasGroup>();

        item = null;

    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (PlayerController.mine == true)
        {
            isMining = true;
            PlayerController.mine = false;

        }
        try
        {
            int stringLength = slotTemplate.name.Length;
            if (stringLength == 17)
            {
                indexInventory = int.Parse(slotTemplate.name.Substring(stringLength - 1));

            }
            else if (stringLength == 18)
            {
                indexInventory = int.Parse(slotTemplate.name.Substring(stringLength - 2));

            }
            item = player.getInventoryItem(indexInventory);

            if (item != null)
            {
                go = Instantiate(itemSlot.gameObject);
                go.transform.SetParent(slotTemplate);

                // go.transform.position
                if (!CanDrag)
                {
                    return;


                }

                OnBeginDragHandler?.Invoke(eventData);
                canvasGroup.alpha = .6f;
                canvasGroup.blocksRaycasts = false;
            }

        }
        catch
        {

        }


    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!CanDrag)
        {
            return;

        }
        OnDragHandler?.Invoke(eventData);
        try
        {
            if (FollowCursor && item != null)
            {
                go.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10f));

            }
        }
        catch
        {

        }



    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(isMining == true)
        {
            PlayerController.mine = true;
            isMining = false;
        }

        if (item != null)
        {
            Destroy(go);

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

                    try
                    {
                        string name = dropArea.GetComponentInParent<EquipmentSlot>().name;

                    if (dropArea.Accepts(this))
                    {
                        dropArea.Drop(this);
                        OnEndDragHandler?.Invoke(eventData, true);
                        canvasGroup.alpha = 1f;
                        canvasGroup.blocksRaycasts = true;

                        uiInventory.Move(indexInventory);
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

                        player.AddEquipment(item, index);
                        return;
                    }
                    }
                    catch
                    {

                    

                }
            }
            uiInventory.Refresh();
            OnEndDragHandler?.Invoke(eventData, false);
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }

    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {

    }

    public int indexReturn()
    {
        return index;
    }
}
