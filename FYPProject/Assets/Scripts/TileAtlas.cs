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
    public TileClass leaf;
    public TileClass sand;
    public TileClass catcus;
    public TileClass snow;
    public TileClass snowLeaf;
    

    [Header("Ores")]
    public TileClass coal;
    public TileClass iron;
    public TileClass gold;
    public TileClass diamond;


}