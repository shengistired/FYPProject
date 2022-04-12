using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileAtlas", menuName = "Tile Atlas")]
public class TileAtlas : ScriptableObject
{
    [Header("Environment objects")]
    public TileClass grass;
    public TileClass dirt;
    public TileClass stone;
    public TileClass treeLog;
    public TileClass treeWood;
    public TileClass leaf;
    public TileClass sand;
    public TileClass catcus;
    public TileClass snow;
    public TileClass snowLeaf;
    //background tiles
    // public TileClass stone_wall;
    // public TileClass dirt_wall;

    [Header("Ores")]
    public TileClass coal;
    public TileClass iron;
    public TileClass gold;
    public TileClass diamond;

    [Header("Important objects")]
    public TileClass portal;


}
