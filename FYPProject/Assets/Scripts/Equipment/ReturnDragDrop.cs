using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReturnDragDrop : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{    
    public event Action<PointerEventData> OnBeginDragHandler;
    public event Action<PointerEventData> OnDragHandler;
    public event Action<PointerEventData, bool> OnEndDragHandler;
    private GameObject go;
    private CanvasGroup canvasGroup;
    [SerializeField] private Transform equipTemplate;
    [SerializeField] private PlayerMovement player;
    private bool isMining = false;

    public static bool move;
    private Transform itemSlot;
    private Item item;
    public bool FollowCursor { get; set; } = true;
    public Vector3 StartPosition;
    public static int index = -1;
    private int indexBegin = -1;
    public bool CanDrag { get; set; } = true;
    private void Awake()
    {
        move = false;
        itemSlot = equipTemplate.Find("Item").GetComponent<Transform>();
        canvasGroup = GetComponent<CanvasGroup>();

    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        if (PlayerMovement.mine == true)
        {
            isMining = true;
            PlayerMovement.mine = false;

        }

        try
        {
            indexBegin = int.Parse(equipTemplate.name.Substring(equipTemplate.name.Length - 1));
            indexBegin -= 1;
            item = player.getEquipment(indexBegin);
            
            if(item!= null)
            {
                go = Instantiate(itemSlot.gameObject);
                go.transform.SetParent(equipTemplate);

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
        if (FollowCursor && item!= null)
        {
            go.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10f));

        }


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isMining == true)
        {
            PlayerMovement.mine = true;
            isMining = false;
        }


        if (item!= null)
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
                    string name = dropArea.GetComponentInParent<ReturnSlot>().name;

                    if (dropArea.AcceptsReturn(this) && name != equipTemplate.name)
                    {
                        dropArea.DropReturn(this);
                        OnEndDragHandler?.Invoke(eventData, true);
                        canvasGroup.alpha = 1f;
                        canvasGroup.blocksRaycasts = true;
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

                        if (name == "UI_Inventory")
                        {

                            player.AddItemInventory(item, indexBegin);
                        }
                        else
                        {


                            player.MoveEquipment(item, indexBegin, index);

                        }

                        return;
                    }
                }
                catch
                {

                }

            }
            OnEndDragHandler?.Invoke(eventData, false);
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
       

    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        //StartPosition = rectTransform.position;

    }




}
