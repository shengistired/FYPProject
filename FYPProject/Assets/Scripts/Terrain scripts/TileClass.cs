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
    public bool isTree;
    public bool isGround;
    public bool isDirt;
    public bool isBackground;

    //each tile will have their own tileclass 
    public static TileClass CreateInstance(TileClass tile, bool isGeneratedNaturally)
    {
        var thisTile = ScriptableObject.CreateInstance<TileClass>();
        thisTile.initializeTile(tile, isGeneratedNaturally);
        return thisTile;
    }

    public void initializeTile(TileClass tile, bool isGeneratedNaturally)
    {
        tileName = tile.tileName;
        backgroundVersion = tile.backgroundVersion;
        tileSprites = tile.tileSprites;
        isSolidTile = tile.isSolidTile;
        tileDrop = tile.tileDrop;
        generatedNaturally = isGeneratedNaturally;
        tileHealth = tile.tileHealth;
        isTree = tile.isTree;
        isGround = tile.isGround;
        isDirt = tile.isDirt;
        isBackground = tile.isBackground;
    }

    public static implicit operator TileClass(string v)
    {
        throw new NotImplementedException();
    }
}
