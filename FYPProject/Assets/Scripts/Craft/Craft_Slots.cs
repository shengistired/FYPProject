using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Craft_Slots : MonoBehaviour
{
	[SerializeField] protected Image image;
	[SerializeField] protected TextMeshProUGUI amountText;
	protected Color normalColor = Color.white;
	protected Color disabledColor = new Color(1, 1, 1, 0);

	protected DropArea dropArea;
	protected Item _item;
	public Item Item
	{
		get { return _item; }
		set
		{
			_item = value;
			if (_item == null)
			{
				image.sprite = null;
				image.color = disabledColor;
			}
			else
			{
                try
                {
					//_item.amount = value.amount;
					image.sprite = _item.GetSprite();
					image.color = normalColor;
					Debug.Log(_item.amount);
					amountText.text = _item.amount.ToString();

				}
                catch
                {

                }

			}

		}
	}


	protected virtual void OnValidate()
	{
		if (image == null)
			image = GetComponent<Image>();

		if (amountText == null)
			amountText = GetComponentInChildren<TextMeshProUGUI>();

		Item = _item;

	}
	protected virtual void Awake()
    {
        dropArea = GetComponent<DropArea>() ?? gameObject.AddComponent<DropArea>();
        dropArea.OnDropHandler += OnItemDropped;
    }

    private void OnItemDropped(DragAndDrop draggable)
    {
        _item = draggable.item;
    }
}
