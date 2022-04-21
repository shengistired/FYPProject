using UnityEngine;
using System;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Weapon,
        Axe,
        Axe1,
        Axe2,
        Axe3,
        Axe4,
        Potion,
        Food,
        Meat,
        Coin,
        Dirt,
        coal,
        diamond,
        gold,
        Grass,
        iron,
        leaf,
        sand,
        snow,
        snowLeaf,
        Stone,
        treeLogs,
        treeWood,
        stone_wall,
        sand_wall,
        dirt_wall,
        campFire,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Weapon:
                return ItemAssets.Instance.weaponSprite;
            case ItemType.Axe:
                return ItemAssets.Instance.axeSprite;
            case ItemType.Axe1:
                return ItemAssets.Instance.axe1Sprite;
            case ItemType.Axe2:
                return ItemAssets.Instance.axe2Sprite;
            case ItemType.Axe3:
                return ItemAssets.Instance.axe3Sprite;
            case ItemType.Axe4:
                return ItemAssets.Instance.axe4Sprite;
            case ItemType.Potion:
                return ItemAssets.Instance.potionSprite;
            case ItemType.Food:
                return ItemAssets.Instance.foodSprite;
            case ItemType.Meat:
                return ItemAssets.Instance.meatSprite;
            case ItemType.Coin:
                return ItemAssets.Instance.coinSprite;
            case ItemType.Dirt:
                return ItemAssets.Instance.dirtSprite;
            case ItemType.coal:
                return ItemAssets.Instance.coalSprite;
            case ItemType.diamond:
                return ItemAssets.Instance.diamondSprite;
            case ItemType.gold:
                return ItemAssets.Instance.goldSprite;
            case ItemType.Grass:
                return ItemAssets.Instance.grassSprite;
            case ItemType.iron:
                return ItemAssets.Instance.ironSprite;
            case ItemType.leaf:
                return ItemAssets.Instance.leafSprite;
            case ItemType.sand:
                return ItemAssets.Instance.sandSprite;
            case ItemType.snow:
                return ItemAssets.Instance.snowSprite;
            case ItemType.snowLeaf:
                return ItemAssets.Instance.snowLeafSprite;
            case ItemType.Stone:
                return ItemAssets.Instance.stoneSprite;
            case ItemType.treeLogs:
                return ItemAssets.Instance.treeLogsSprite;
            case ItemType.treeWood:
                return ItemAssets.Instance.treeWoodSprite;
            case ItemType.stone_wall:
                return ItemAssets.Instance.stoneWallSprite;
            case ItemType.sand_wall:
                return ItemAssets.Instance.sandWallSprite;
            case ItemType.dirt_wall:
                return ItemAssets.Instance.dirtWallSprite;         
            case ItemType.campFire:
                return ItemAssets.Instance.campfireSprite;

        }
    }

    public bool isStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Coin:
            case ItemType.Food:
            case ItemType.Meat:
            case ItemType.Potion:
            case ItemType.Dirt:
            case ItemType.coal:
            case ItemType.diamond:
            case ItemType.gold:
            case ItemType.Grass:
            case ItemType.iron:
            case ItemType.leaf:
            case ItemType.sand:
            case ItemType.snow:
            case ItemType.snowLeaf:
            case ItemType.Stone:
            case ItemType.treeLogs:
            case ItemType.treeWood:
            case ItemType.stone_wall:
            case ItemType.sand_wall:
            case ItemType.dirt_wall:
            case ItemType.campFire:
                return true;
            case ItemType.Weapon:
            case ItemType.Axe:
            case ItemType.Axe1:
            case ItemType.Axe2:
            case ItemType.Axe3:
            case ItemType.Axe4:
                return false;


        }
    }
    public bool isAxe()
    {
        switch (itemType)
        {
            default:
            case ItemType.Coin:
            case ItemType.Food:
            case ItemType.Meat:
            case ItemType.Potion:
            case ItemType.Dirt:
            case ItemType.coal:
            case ItemType.diamond:
            case ItemType.gold:
            case ItemType.Grass:
            case ItemType.iron:
            case ItemType.leaf:
            case ItemType.sand:
            case ItemType.snow:
            case ItemType.snowLeaf:
            case ItemType.Stone:
            case ItemType.treeLogs:
            case ItemType.treeWood:
            case ItemType.stone_wall:
            case ItemType.sand_wall:
            case ItemType.dirt_wall:
            case ItemType.campFire:
            case ItemType.Weapon:
                return false;
            case ItemType.Axe:
            case ItemType.Axe1:
            case ItemType.Axe2:
            case ItemType.Axe3:
            case ItemType.Axe4:
                return true;


        }
    }

    public string descriptionText()
    {
        switch (itemType)
        {
            default:
            case ItemType.Food:
            case ItemType.Meat:
            case ItemType.Potion:
            case ItemType.Coin:
            case ItemType.Stone:
            case ItemType.diamond:
            case ItemType.Dirt:
            case ItemType.gold:
            case ItemType.treeWood:
            case ItemType.coal:
            case ItemType.Grass:
            case ItemType.iron:
            case ItemType.leaf:
            case ItemType.sand:
            case ItemType.snow:
            case ItemType.snowLeaf:
            case ItemType.treeLogs:
            case ItemType.stone_wall:
            case ItemType.sand_wall:
            case ItemType.dirt_wall:
            case ItemType.campFire:
                return itemType.ToString();
            case ItemType.Weapon:
                return itemType.ToString() + " Damage: 1";
            case ItemType.Axe:
                return itemType.ToString() + " Mining Power: 1";
            case ItemType.Axe1:
                return "Axe +1 Mining Power: 2";
            case ItemType.Axe2:
                return "Axe +2 Mining Power: 2";
            case ItemType.Axe3:
                return "Axe +3 Mining Power: 2";
            case ItemType.Axe4:
                return "Axe +4 Mining Power: 2";


        }
    }



}
