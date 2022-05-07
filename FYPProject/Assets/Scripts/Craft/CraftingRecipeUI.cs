using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipeUI : MonoBehaviour
{
	[Header("References")]
	[SerializeField] RectTransform arrowParent;
	[SerializeField] GameObject craftedActive;
	[SerializeField] GameObject crafted;
	[SerializeField] Craft_Slots[] itemSlots;
	public Inventory inventory;
	public PlayerController player;

	private CraftingRecipe craftingRecipe;
    public CraftingRecipe CraftingRecipe
    {
        get { return craftingRecipe; }
        set { SetCraftingRecipe(value); }
    }

    public void setPlayer(PlayerController player)
    {
		this.player = player;
    }
    
    public void OnCraftButtonClick()
	{
		inventory = player.getInventory();
		if (craftingRecipe != null && inventory!= null)
		{
			craftingRecipe.Craft(inventory);
		}
	}

	private void OnValidate()
	{
		itemSlots = GetComponentsInChildren<Craft_Slots>(includeInactive: true);
	}
    private void Update()
    {
        try
        {

            if (craftingRecipe.CanCraft(player.getInventory())){
				craftedActive.SetActive(false);

			}
            else
            {
				craftedActive.SetActive(true);

			}

			if (craftingRecipe.crafted == true)
			{
				crafted.SetActive(true);
			}
		}
        catch { 
		}

	}
    private void SetCraftingRecipe(CraftingRecipe newCraftingRecipe)
	{
		craftingRecipe = newCraftingRecipe;


		if (craftingRecipe != null)
		{
			int slotIndex = 0;
			slotIndex = SetSlots(craftingRecipe.Materials, slotIndex);
			arrowParent.SetSiblingIndex(slotIndex);
			slotIndex = SetSlots(craftingRecipe.Results, slotIndex);

			for (int i = slotIndex; i < itemSlots.Length; i++)
			{
				itemSlots[i].transform.parent.gameObject.SetActive(false);
			}

			gameObject.SetActive(true);

		}
		else
		{
			gameObject.SetActive(false);
		}
	}
		private int SetSlots(IList<Item> itemList, int slotIndex)
		{
			for (int i = 0; i < itemList.Count; i++, slotIndex++)
			{
				Item item = itemList[i];
				Craft_Slots itemSlot = itemSlots[slotIndex];

				itemSlot.Item = item;
				itemSlot.transform.parent.gameObject.SetActive(true);
			}
			return slotIndex;
		}
	
}
