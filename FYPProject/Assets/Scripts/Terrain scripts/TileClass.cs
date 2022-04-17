using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "newtileclass", menuName = "Tile Class")]
public class TileClass : ScriptableObject
{
    public string tileName;
    public TileClass backgroundVersion;
    public Sprite[] tileSprites;
    public bool generatedNaturally = false;
    public bool isSolidTile = true;
    public bool tileDrop = true;
    public int tileHealth;

    //each tile will have their own tileclass 
    public TileClass (TileClass tile , bool isGeneratedNaturally)
    {
        tileName= tile.tileName;
        backgroundVersion = tile.backgroundVersion;
        tileSprites = tile.tileSprites;
        isSolidTile = tile.isSolidTile;
        tileDrop = tile.tileDrop;
        generatedNaturally = isGeneratedNaturally;
        tileHealth = tile.tileHealth;
    }

    public static implicit operator TileClass(string v)
    {
        throw new NotImplementedException();
    }
}
