using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ReturnSlot : MonoBehaviour
{
    protected DropArea dropArea;

    protected virtual void Awake()
    {
        dropArea = GetComponent<DropArea>() ?? gameObject.AddComponent<DropArea>();
        dropArea.OnDropReturnHandler += OnItemDropped;
    }

    private void OnItemDropped(ReturnDragDrop draggable)
    {
        //Transform itemSlot = transform.Find("Item").GetComponent<Transform>();
        //draggable.transform.position = transform.position;
    }
}
