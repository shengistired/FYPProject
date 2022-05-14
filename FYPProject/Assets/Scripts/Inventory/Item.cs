using UnityEngine;
using System;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Weapon,
        Axe,
        WoodAxe,
        StoneAxe,
        IronAxe,
        GoldAxe,
        DiamondAxe,
        PickAxe,
        WoodPickAxe,
        StonePickAxe,
        IronPickAxe,
        GoldPickAxe,
        DiamondPickAxe,
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
        cactus,
        cactusFruit,
        Stone,
        treeLogs,
        treeWood,
        stone_wall,
        sand_wall,
        dirt_wall,
        campFire,
        AxeMaterial,
        PickAxeMaterial,
        Hammer,
        WoodHammer,
        StoneHammer,
        IronHammer,
        GoldHammer,
        DiamondHammer,
        HammerMaterial,
        Shovel,
        WoodShovel,
        StoneShovel,
        IronShovel,
        GoldShovel,
        DiamondShovel,
        ShovelMaterial,
    }

    public ItemType itemType;
    public int amount;
    public int durablilty;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Weapon:
                return ItemAssets.Instance.weaponSprite;
            case ItemType.Axe:
                return ItemAssets.Instance.axeSprite;
            case ItemType.WoodAxe:
                return ItemAssets.Instance.woodAxeSprite;
            case ItemType.StoneAxe:
                return ItemAssets.Instance.stoneAxeSprite;
            case ItemType.IronAxe:
                return ItemAssets.Instance.ironAxeSprite;
            case ItemType.GoldAxe:
                return ItemAssets.Instance.goldAxeSprite;
            case ItemType.DiamondAxe:
                return ItemAssets.Instance.diamondAxeSprite;
            case ItemType.PickAxe:
                return ItemAssets.Instance.pickAxeSprite;
            case ItemType.WoodPickAxe:
                return ItemAssets.Instance.woodPickAxeSprite;
            case ItemType.StonePickAxe:
                return ItemAssets.Instance.stonePickAxeSprite;
            case ItemType.IronPickAxe:
                return ItemAssets.Instance.ironPickAxeSprite;
            case ItemType.GoldPickAxe:
                return ItemAssets.Instance.goldPickAxeSprite;
            case ItemType.DiamondPickAxe:
                return ItemAssets.Instance.diamondPickAxeSprite;
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
            case ItemType.cactus:
                return ItemAssets.Instance.cactusSprite;         
            case ItemType.cactusFruit:
                return ItemAssets.Instance.cactusFruitSprite;
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
            case ItemType.AxeMaterial:
                return ItemAssets.Instance.axeMaterialSprite;
            case ItemType.PickAxeMaterial:
                return ItemAssets.Instance.pickAxeMaterialSprite;
            case ItemType.Hammer:
                return ItemAssets.Instance.hammerSprite;
            case ItemType.WoodHammer:
                return ItemAssets.Instance.woodHammerSprite;
            case ItemType.StoneHammer:
                return ItemAssets.Instance.stoneHammerSprite;
            case ItemType.IronHammer:
                return ItemAssets.Instance.ironHammerSprite;
            case ItemType.GoldHammer:
                return ItemAssets.Instance.goldHammerSprite;
            case ItemType.DiamondHammer:
                return ItemAssets.Instance.diamondHammerSprite;
            case ItemType.HammerMaterial:
                return ItemAssets.Instance.hammerMaterialSprite;
            case ItemType.Shovel:
                return ItemAssets.Instance.shovelSprite;
            case ItemType.WoodShovel:
                return ItemAssets.Instance.woodShovelSprite;
            case ItemType.StoneShovel:
                return ItemAssets.Instance.stoneShovelSprite;
            case ItemType.IronShovel:
                return ItemAssets.Instance.ironShovelSprite;
            case ItemType.GoldShovel:
                return ItemAssets.Instance.goldShovelSprite;
            case ItemType.DiamondShovel:
                return ItemAssets.Instance.diamondShovelSprite;
            case ItemType.ShovelMaterial:
                return ItemAssets.Instance.shovelMaterialSprite;
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
            case ItemType.cactus:
            case ItemType.Stone:
            case ItemType.treeLogs:
            case ItemType.treeWood:
            case ItemType.stone_wall:
            case ItemType.sand_wall:
            case ItemType.dirt_wall:
            case ItemType.campFire:
            case ItemType.Axe:
            case ItemType.WoodAxe:
            case ItemType.StoneAxe:
            case ItemType.IronAxe:
            case ItemType.GoldAxe:
            case ItemType.DiamondAxe:
            case ItemType.PickAxe:
            case ItemType.WoodPickAxe:
            case ItemType.StonePickAxe:
            case ItemType.IronPickAxe:
            case ItemType.GoldPickAxe:
            case ItemType.DiamondPickAxe:
            case ItemType.AxeMaterial:
            case ItemType.PickAxeMaterial:
            case ItemType.HammerMaterial:
            case ItemType.ShovelMaterial:
            case ItemType.Shovel:
            case ItemType.WoodShovel:
            case ItemType.StoneShovel:
            case ItemType.IronShovel:
            case ItemType.GoldShovel:
            case ItemType.DiamondShovel:
            case ItemType.Hammer:
            case ItemType.WoodHammer:
            case ItemType.StoneHammer:
            case ItemType.IronHammer:
            case ItemType.GoldHammer:
            case ItemType.DiamondHammer:
                return true;
            case ItemType.Weapon:

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
            case ItemType.cactus:
            case ItemType.Stone:
            case ItemType.treeLogs:
            case ItemType.treeWood:
            case ItemType.stone_wall:
            case ItemType.sand_wall:
            case ItemType.dirt_wall:
            case ItemType.campFire:
            case ItemType.Weapon:
            case ItemType.PickAxe:
            case ItemType.WoodPickAxe:
            case ItemType.StonePickAxe:
            case ItemType.IronPickAxe:
            case ItemType.GoldPickAxe:
            case ItemType.DiamondPickAxe:
            case ItemType.AxeMaterial:
            case ItemType.PickAxeMaterial:
            case ItemType.HammerMaterial:
            case ItemType.ShovelMaterial:
            case ItemType.Shovel:
            case ItemType.WoodShovel:
            case ItemType.StoneShovel:
            case ItemType.IronShovel:
            case ItemType.GoldShovel:
            case ItemType.DiamondShovel:
            case ItemType.Hammer:
            case ItemType.WoodHammer:
            case ItemType.StoneHammer:
            case ItemType.IronHammer:
            case ItemType.GoldHammer:
            case ItemType.DiamondHammer:
                return false;
            case ItemType.Axe:
            case ItemType.WoodAxe:
            case ItemType.StoneAxe:
            case ItemType.IronAxe:
            case ItemType.GoldAxe:
            case ItemType.DiamondAxe:
                return true;


        }
    }
    public bool isPickAxe()
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
            case ItemType.cactus:
            case ItemType.Stone:
            case ItemType.treeLogs:
            case ItemType.treeWood:
            case ItemType.stone_wall:
            case ItemType.sand_wall:
            case ItemType.dirt_wall:
            case ItemType.campFire:
            case ItemType.Weapon:
            case ItemType.Axe:
            case ItemType.WoodAxe:
            case ItemType.StoneAxe:
            case ItemType.IronAxe:
            case ItemType.GoldAxe:
            case ItemType.DiamondAxe:
            case ItemType.AxeMaterial:
            case ItemType.PickAxeMaterial:
            case ItemType.HammerMaterial:
            case ItemType.ShovelMaterial:
            case ItemType.Shovel:
            case ItemType.WoodShovel:
            case ItemType.StoneShovel:
            case ItemType.IronShovel:
            case ItemType.GoldShovel:
            case ItemType.DiamondShovel:
            case ItemType.Hammer:
            case ItemType.WoodHammer:
            case ItemType.StoneHammer:
            case ItemType.IronHammer:
            case ItemType.GoldHammer:
            case ItemType.DiamondHammer:
                return false;
            case ItemType.PickAxe:
            case ItemType.WoodPickAxe:
            case ItemType.StonePickAxe:
            case ItemType.IronPickAxe:
            case ItemType.GoldPickAxe:
            case ItemType.DiamondPickAxe:
                return true;


        }
    }
    public bool isShovel()
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
            case ItemType.cactus:
            case ItemType.Stone:
            case ItemType.treeLogs:
            case ItemType.treeWood:
            case ItemType.stone_wall:
            case ItemType.sand_wall:
            case ItemType.dirt_wall:
            case ItemType.campFire:
            case ItemType.Weapon:
            case ItemType.Axe:
            case ItemType.WoodAxe:
            case ItemType.StoneAxe:
            case ItemType.IronAxe:
            case ItemType.GoldAxe:
            case ItemType.DiamondAxe:
            case ItemType.AxeMaterial:
            case ItemType.PickAxeMaterial:
            case ItemType.HammerMaterial:
            case ItemType.ShovelMaterial:
            case ItemType.PickAxe:
            case ItemType.WoodPickAxe:
            case ItemType.StonePickAxe:
            case ItemType.IronPickAxe:
            case ItemType.GoldPickAxe:
            case ItemType.DiamondPickAxe:
            case ItemType.Hammer:
            case ItemType.WoodHammer:
            case ItemType.StoneHammer:
            case ItemType.IronHammer:
            case ItemType.GoldHammer:
            case ItemType.DiamondHammer:
                return false;
            case ItemType.Shovel:
            case ItemType.WoodShovel:
            case ItemType.StoneShovel:
            case ItemType.IronShovel:
            case ItemType.GoldShovel:
            case ItemType.DiamondShovel:
                return true;


        }
    }
    public bool isHammer()
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
            case ItemType.cactus:
            case ItemType.Stone:
            case ItemType.treeLogs:
            case ItemType.treeWood:
            case ItemType.stone_wall:
            case ItemType.sand_wall:
            case ItemType.dirt_wall:
            case ItemType.campFire:
            case ItemType.Weapon:
            case ItemType.Axe:
            case ItemType.WoodAxe:
            case ItemType.StoneAxe:
            case ItemType.IronAxe:
            case ItemType.GoldAxe:
            case ItemType.DiamondAxe:
            case ItemType.AxeMaterial:
            case ItemType.PickAxeMaterial:
            case ItemType.HammerMaterial:
            case ItemType.ShovelMaterial:
            case ItemType.PickAxe:
            case ItemType.WoodPickAxe:
            case ItemType.StonePickAxe:
            case ItemType.IronPickAxe:
            case ItemType.GoldPickAxe:
            case ItemType.DiamondPickAxe:
            case ItemType.Shovel:
            case ItemType.WoodShovel:
            case ItemType.StoneShovel:
            case ItemType.IronShovel:
            case ItemType.GoldShovel:
            case ItemType.DiamondShovel:
                return false;
            case ItemType.Hammer:
            case ItemType.WoodHammer:
            case ItemType.StoneHammer:
            case ItemType.IronHammer:
            case ItemType.GoldHammer:
            case ItemType.DiamondHammer:
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
            case ItemType.cactus:
            case ItemType.treeLogs:
            case ItemType.stone_wall:
            case ItemType.sand_wall:
            case ItemType.dirt_wall:
            case ItemType.campFire:
            case ItemType.AxeMaterial:
            case ItemType.HammerMaterial:
            case ItemType.ShovelMaterial:
            case ItemType.PickAxeMaterial:
                return itemType.ToString();
            case ItemType.Weapon:
                return itemType.ToString() + " Damage: 1";
            case ItemType.Axe:
                return itemType.ToString() + " Mining Power: 1 Durability: " + durablilty;
            case ItemType.WoodAxe:
                return itemType.ToString() + " Mining Power: 2 Durability: " + durablilty;
            case ItemType.StoneAxe:
                return itemType.ToString() + " Mining Power: 3 Durability: " + durablilty;
            case ItemType.IronAxe:
                return itemType.ToString() + " Mining Power: 4 Durability: " + durablilty;
            case ItemType.GoldAxe:
                return itemType.ToString() + " Mining Power: 5 Durability: " + durablilty;
            case ItemType.DiamondAxe:
                return itemType.ToString() + " Mining Power: 6 Durability: " + durablilty;
            case ItemType.PickAxe:
                return itemType.ToString() + " Mining Power: 1 Durability: " + durablilty;
            case ItemType.WoodPickAxe:
                return itemType.ToString() + " Mining Power: 2 Durability: " + durablilty;
            case ItemType.StonePickAxe:
                return itemType.ToString() + " Mining Power: 3 Durability: " + durablilty;
            case ItemType.IronPickAxe:
                return itemType.ToString() + " Mining Power: 4 Durability: " + durablilty;
            case ItemType.GoldPickAxe:
                return itemType.ToString() + " Mining Power: 5 Durability: " + durablilty;
            case ItemType.DiamondPickAxe:
                return itemType.ToString() + " Mining Power: 6 Durability: " + durablilty;            
            case ItemType.Hammer:
                return itemType.ToString() + " Mining Power: 1 Durability: " + durablilty;
            case ItemType.WoodHammer:
                return itemType.ToString() + " Mining Power: 2 Durability: " + durablilty;
            case ItemType.StoneHammer:
                return itemType.ToString() + " Mining Power: 3 Durability: " + durablilty;
            case ItemType.IronHammer:
                return itemType.ToString() + " Mining Power: 4 Durability: " + durablilty;
            case ItemType.GoldHammer:
                return itemType.ToString() + " Mining Power: 5 Durability: " + durablilty;
            case ItemType.DiamondHammer:
                return itemType.ToString() + " Mining Power: 6 Durability: " + durablilty;            
            case ItemType.Shovel:
                return itemType.ToString() + " Mining Power: 1 Durability: " + durablilty;
            case ItemType.WoodShovel:
                return itemType.ToString() + " Mining Power: 2 Durability: " + durablilty;
            case ItemType.StoneShovel:
                return itemType.ToString() + " Mining Power: 3 Durability: " + durablilty;
            case ItemType.IronShovel:
                return itemType.ToString() + " Mining Power: 4 Durability: " + durablilty;
            case ItemType.GoldShovel:
                return itemType.ToString() + " Mining Power: 5 Durability: " + durablilty;
            case ItemType.DiamondShovel:
                return itemType.ToString() + " Mining Power: 6 Durability: " + durablilty;


        }
    }

    public int miningPower()
    {
        switch (itemType)
        {
            default:
            case ItemType.Axe:
                return 1;
            case ItemType.WoodAxe:
                return 2;
            case ItemType.StoneAxe:
                return 3;
            case ItemType.IronAxe:
                return 4;
            case ItemType.GoldAxe:
                return 5;
            case ItemType.DiamondAxe:
                return 6;
            case ItemType.PickAxe:
                return 1;
            case ItemType.WoodPickAxe:
                return 2;
            case ItemType.StonePickAxe:
                return 3;
            case ItemType.IronPickAxe:
                return 4;
            case ItemType.GoldPickAxe:
                return 5;
            case ItemType.DiamondPickAxe:
                return 6;
            case ItemType.Hammer:
                return 1;
            case ItemType.WoodHammer:
                return 2;
            case ItemType.StoneHammer:
                return 3;
            case ItemType.IronHammer:
                return 4;
            case ItemType.GoldHammer:
                return 5;
            case ItemType.DiamondHammer:
                return 6;
            case ItemType.Shovel:
                return 1;
            case ItemType.WoodShovel:
                return 2;
            case ItemType.StoneShovel:
                return 3;
            case ItemType.IronShovel:
                return 4;
            case ItemType.GoldShovel:
                return 5;
            case ItemType.DiamondShovel:
                return 6;
        }


    }
    public int getDurability()
    {
        switch (itemType)
        {

            default:
            case ItemType.Axe:
                return 30;
            case ItemType.WoodAxe:
                return 50;
            case ItemType.StoneAxe:
                return 70;
            case ItemType.IronAxe:
                return 90;
            case ItemType.GoldAxe:
                return 110;
            case ItemType.DiamondAxe:
                return 130;
            case ItemType.PickAxe:
                return 30;
            case ItemType.WoodPickAxe:
                return 50;
            case ItemType.StonePickAxe:
                return 70;
            case ItemType.IronPickAxe:
                return 90;
            case ItemType.GoldPickAxe:
                return 1100;
            case ItemType.DiamondPickAxe:
                return 130;
            case ItemType.Hammer:
                return 30;
            case ItemType.WoodHammer:
                return 50;
            case ItemType.StoneHammer:
                return 70;
            case ItemType.IronHammer:
                return 90;
            case ItemType.GoldHammer:
                return 110;
            case ItemType.DiamondHammer:
                return 130;
            case ItemType.Shovel:
                return 30;
            case ItemType.WoodShovel:
                return 50;
            case ItemType.StoneShovel:
                return 70;
            case ItemType.IronShovel:
                return 90;
            case ItemType.GoldShovel:
                return 110;
            case ItemType.DiamondShovel:
                return 130;

        }


    }
}
