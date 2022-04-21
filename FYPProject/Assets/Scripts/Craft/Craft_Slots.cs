using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Craft_Slots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] protected Image image;
	[SerializeField] protected TextMeshProUGUI amountText;
	private RectTransform itemSlot;
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
		itemSlot = gameObject.GetComponentInParent<RectTransform>();

    }

    private void OnItemDropped(DragAndDrop draggable)
    {
        _item = draggable.item;
    }
    private void OnMouseEnter()
    {
		ToolTip.ShowToolTip_Static(_item.itemType.ToString());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

		if(itemSlot.parent.name == "CraftingSlotResult")
        {
			ToolTip.ShowToolTip_Static(_item.descriptionText());

		}
		else
        {
			ToolTip.ShowToolTip_Static(_item.descriptionText() + " Amount: " + _item.amount);

		}
	}

    public void OnPointerExit(PointerEventData eventData)
    {
		ToolTip.HideToolTip_Static();
    }
}
