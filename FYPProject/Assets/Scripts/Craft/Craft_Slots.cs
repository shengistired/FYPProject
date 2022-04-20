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
			if (_item == null && Amount != 0) Amount = 0;

			if (_item == null)
			{
				image.sprite = null;
				image.color = disabledColor;
			}
			else
			{
                try
                {
					image.sprite = _item.GetSprite();
					image.color = normalColor;
				}
                catch
                {

                }

			}

		}
	}

	private int _amount;
	public int Amount
	{
		get { return _amount; }
		set
		{
			_amount = value;
			if (_amount < 0) _amount = 0;
			if (_amount == 0 && Item != null) Item = null;

			if (amountText != null)
			{
				amountText.enabled = _item != null && _amount > 1;
				if (amountText.enabled)
				{
					Debug.Log(_amount);
					amountText.text = _amount.ToString();
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
		Amount = _amount;
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
