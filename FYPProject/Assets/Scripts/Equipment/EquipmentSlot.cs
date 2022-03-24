using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    protected DropArea dropArea;
    
    protected virtual void Awake()
    {
        dropArea = GetComponent<DropArea>() ?? gameObject.AddComponent<DropArea>();
        dropArea.OnDropHandler += OnItemDropped;
    }

    private void OnItemDropped(DragAndDrop draggable)
    {
        Image pointerImage = draggable.GetComponent<RectTransform>().Find("Image").GetComponent<Image>();
        Image image = gameObject.GetComponent<RectTransform>().Find("Image").GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 1f;
        image.sprite = pointerImage.sprite;
        image.color = tempColor;


        draggable.transform.position = transform.position;
    }
}
