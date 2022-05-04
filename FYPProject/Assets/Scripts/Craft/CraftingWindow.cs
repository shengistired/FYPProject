using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingWindow : MonoBehaviour
{
	[Header("References")]
	[SerializeField] CraftingRecipeUI recipeUIPrefab;
	[SerializeField] RectTransform recipeUIParent;
	[SerializeField] List<CraftingRecipeUI> craftingRecipeUIs;

	[Header("Public Variables")]
	public Inventory inventory;
	public List<CraftingRecipe> CraftingRecipes;
	public List<CraftingRecipe> CraftingRecipesSnow;
	public List<CraftingRecipe> CraftingRecipesDesert;
	private void OnValidate()
	{
		Init();
	}

	private void Start()
	{
		Init();

	}

	private void Init()
	{
		recipeUIParent.GetComponentsInChildren<CraftingRecipeUI>(includeInactive: true, result: craftingRecipeUIs);
		UpdateCraftingRecipes();
	}

	public void UpdateCraftingRecipes()
	{
			for (int i = 0; i < 6; i++)
			{
				if (craftingRecipeUIs.Count == i)
				{
					craftingRecipeUIs.Add(Instantiate(recipeUIPrefab, recipeUIParent, false));
				}
				else if (craftingRecipeUIs[i] == null)
				{
					craftingRecipeUIs[i] = Instantiate(recipeUIPrefab, recipeUIParent, false);
				}

				craftingRecipeUIs[i].inventory = inventory;
			if (TerrainGeneration.biome == "forest")
            {
				craftingRecipeUIs[i].CraftingRecipe = CraftingRecipes[i];

			}
			else if (TerrainGeneration.biome == "desert")
			{
				craftingRecipeUIs[i].CraftingRecipe = CraftingRecipesDesert[i];

			}
			else if (TerrainGeneration.biome == "snow")
			{
				
				craftingRecipeUIs[i].CraftingRecipe = CraftingRecipesSnow[i];

			}
		}
		

		
		for (int i = CraftingRecipes.Count; i < craftingRecipeUIs.Count; i++)
		{
			craftingRecipeUIs[i].CraftingRecipe = null;

		}
		
	}
}
