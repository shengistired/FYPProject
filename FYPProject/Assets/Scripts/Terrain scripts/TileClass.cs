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

    public static implicit operator TileClass(string v)
    {
        throw new NotImplementedException();
    }
}
