using System;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
	public List<DropItemCondition> DropConditions = new List<DropItemCondition>();
	public event Action<DragAndDrop> OnDropHandler;
	public event Action<ReturnDragDrop> OnDropReturnHandler;

	public bool Accepts(DragAndDrop draggable)
	{
		return DropConditions.TrueForAll(cond => cond.Check(draggable));
	}
	public bool AcceptsReturn(ReturnDragDrop draggable)
	{
		return DropConditions.TrueForAll(cond => cond.CheckReturn(draggable));
	}

	public void Drop(DragAndDrop draggable)
	{
		OnDropHandler?.Invoke(draggable);
	}
	public void DropReturn(ReturnDragDrop draggable)
	{
		OnDropReturnHandler?.Invoke(draggable);
	}
}