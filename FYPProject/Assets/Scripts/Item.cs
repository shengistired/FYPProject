using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item 
{
    public enum ItemType
    {
        Weapon,
        Potion,
        Food,
        Coin,
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
            case ItemType.Potion: 
                    return ItemAssets.Instance.potionSprite;
            case ItemType.Food: 
                    return ItemAssets.Instance.foodSprite;
            case ItemType.Coin: 
                    return ItemAssets.Instance.coinSprite;
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
                return true;
            case ItemType.Weapon:
                return false;


        }
    }
}
