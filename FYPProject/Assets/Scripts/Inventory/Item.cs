using UnityEngine;
using System;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Weapon,
        Axe,
        Potion,
        Food,
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
        stoneWall,
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
            case ItemType.Potion:
                return ItemAssets.Instance.potionSprite;
            case ItemType.Food:
                return ItemAssets.Instance.foodSprite;
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
            case ItemType.stoneWall:
                return ItemAssets.Instance.stoneWallSprite;

        }
    }

    public bool isStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Coin:
            case ItemType.Food:
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
                return true;
            case ItemType.stoneWall:
            case ItemType.Weapon:
            case ItemType.Axe:
                return false;


        }
    }
}
