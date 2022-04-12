using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Craft_Slots : MonoBehaviour
{
    protected DropArea dropArea;
    private Item item;
    protected virtual void Awake()
    {
        dropArea = GetComponent<DropArea>() ?? gameObject.AddComponent<DropArea>();
        dropArea.OnDropHandler += OnItemDropped;
    }

    private void OnItemDropped(DragAndDrop draggable)
    {
        
    }
}
