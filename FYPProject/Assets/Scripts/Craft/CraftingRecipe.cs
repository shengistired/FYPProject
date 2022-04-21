using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    public List<Item> Materials;
    public List<Item> Results;

	public bool CanCraft(Inventory inventory)
	{
		return HasMaterials(inventory);
	}

	private bool HasMaterials(Inventory inventory)
	{
		bool hasMaterials = false;
		foreach (Item item in Materials)
		{
			foreach(Item itemInInventory in inventory.GetItemList())
            {
				if(item.itemType == itemInInventory.itemType)
                {
					if (itemInInventory.amount >= item.amount)
					{
						hasMaterials = true;
					}
                    else
                    {
						Debug.LogWarning("You don't have the required materials.");
						hasMaterials = false;

					}
				}

            }
		}
		return hasMaterials;
	}
	private void RemoveMaterials(Inventory inventory)
	{
		foreach (Item item in Materials)
		{
			for (int i = 0; i < item.amount; i++)
			{
				inventory.RemoveItem(item);
			}
		}
	}
	private void AddResults(Inventory inventory)
	{
		foreach (Item item in Results)
		{
			for (int i = 0; i < item.amount; i++)
			{
				inventory.AddItem(item);
			}
		}
	}
	public void Craft(Inventory inventory)
	{
		if (CanCraft(inventory))
		{
			RemoveMaterials(inventory);
			AddResults(inventory);
		}
	}
}
