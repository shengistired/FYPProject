using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/RecipeScriptableObject")]
public class RecipeScriptableObject : ScriptableObject
{
    public Item output;
    public Item item1;
    public Item item2;
}
