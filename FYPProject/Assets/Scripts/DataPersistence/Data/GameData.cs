using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int portalEntered;
    public List<Item> equipmentItems;
    public List<Item> inventoryItems;
    public Vector3 tilesPosition;
    public Vector3 playerPosition;
    public CraftItem craftItem;
    public int playerlevel;
    public int currentExp;
    public int expNeededToNextLevel;
    public int statPoints;
    public int skillPoint;
    public int str;
    public int dex;
    public int intelligence;
    public int luck;
    public float maxHpBar;
    public float maxManaBar;
    public float maxStamina;
    public GameData()
    {
        portalEntered = 0;
        equipmentItems = new List<Item>();
        inventoryItems = new List<Item>();
        craftItem = new CraftItem();
        playerlevel = 1;
        currentExp = 0;
        expNeededToNextLevel = playerlevel * 100;
        statPoints = 5;
        skillPoint = 0;
        str = 10;
        dex = 10;
        intelligence = 15;
        luck = 10;
        maxHpBar = 100;
        maxStamina = 100;
        maxManaBar = 100;
        //equipment = null;
        
    }
}
