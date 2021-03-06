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
	//public bool crafted = false;
	//public int num;
	//public bool isRecraftable;
	
	public bool CanCraft(Inventory inventory)
	{
		return HasMaterials(inventory);
	}

    private bool HasMaterials(Inventory inventory)
	{
		List<bool> hasMaterials = new List<bool>();
		foreach (Item item in Materials)
		{
			foreach(Item itemInInventory in inventory.GetItemList())
            {
				if(item.itemType == itemInInventory.itemType)
                {
					if (itemInInventory.amount >= item.amount)
					{
						hasMaterials.Add(true);
					}
                    else
                    {
						hasMaterials.Add(false);

					}
				}

            }
		}
        try {
			if(Materials.Count == 1)
            {
				if (hasMaterials.ToArray()[0] == true)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
            else
            {
				if (hasMaterials.ToArray()[0] == true && hasMaterials.ToArray()[1] == true)
				{
					return true;
				}
				else
				{
					return false;
				}
			} 


		}
        catch
        {
			return false;
        }

	}

	private void RemoveMaterials(Inventory inventory)
	{
		foreach (Item item in Materials)
		{
			inventory.RemoveItem(item);
			
		}
	}
	private void AddResults(Inventory inventory)
	{
		Item item = new Item();
		item.itemType = Results[0].itemType;
		item.durablilty = Results[0].durablilty;
		item.amount = 1;
		inventory.AddItem(item);

		/*
		foreach (Item item in Results)
		{
			for (int i = 0; i < item.amount; i++)
			{
				inventory.AddItem(item);
			}
		}
		*/
	}
	public void Craft(Inventory inventory)
	{
		if (CanCraft(inventory))
		{
			RemoveMaterials(inventory);
			AddResults(inventory);

			/*
            if (!isRecraftable)
            {
				crafted = true;

			}
			*/
		}

	}

	public bool isCrafted()
    {
		return true;
    }
	/*
    public void LoadData(GameData data)
    {
		crafted = data.crafted[num];
    }

    public void SaveData(ref GameData data)
    {
		data.crafted[num] = crafted;
    }
	*/
}
