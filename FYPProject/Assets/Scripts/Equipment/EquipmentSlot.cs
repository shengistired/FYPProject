using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EquipmentSlot : MonoBehaviour
{
    protected DropArea dropArea;
    [SerializeField] private TMP_Text text;
    
    protected virtual void Awake()
    {
        dropArea = GetComponent<DropArea>() ?? gameObject.AddComponent<DropArea>();
        dropArea.OnDropHandler += OnItemDropped;
    }

    private void OnItemDropped(DragAndDrop draggable)
    {
        Image pointerImage = draggable.GetComponent<RectTransform>().Find("Image").GetComponent<Image>();
        Image image = gameObject.GetComponent<RectTransform>().Find("Image").GetComponent<Image>();
        string amount = draggable.GetComponent<RectTransform>().Find("amountText").GetComponent<TMP_Text>().text.ToString();

        try
        {
            if (image.sprite.name == pointerImage.sprite.name)
            {
                if (int.TryParse((text.text), out int num))
                {
                    if (int.TryParse((amount), out int num2))
                    {
                        int total = num + num2;
                        text.text = total.ToString();
                    }
                }

            }
            else
            {
                text.text = amount;

            }
        }
        catch(Exception ex)
        {
            text.text = amount;
        }


        


        var tempColor = image.color;
        tempColor.a = 1f;
        image.sprite = pointerImage.sprite;
        image.color = tempColor;


        draggable.transform.position = transform.position;
    }
}
