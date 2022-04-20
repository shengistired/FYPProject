using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{

    public string PlayerClass = "mage";
    public int Playerlevel = 1;
    public int expNeededToNext;
    //warrior starts with str 15
    public int str = 10;

    //archer starts with dex 15
    public int dex = 10;

    //mage starts with 15 int
    public int intelligence = 15;

    //thief starts with 15 
    public int luck = 10;


    //increase str to increase HP
    public int MaxHpBar = 100;

    //increase int to increase mana
    public int MaxManaBar = 100;

    //increase dex to increase stamina
    public int MaxStamina = 100;

    //increase luck to increase crit chance calculated in damage dealt 
    public float critChance;





    public int healthRegen = 10;

    public int MaxHungerBar = 100;


    // Easy 5 death normal 3 death Hard 1 death
    public int DeathCount;

    public float damage;
    public int defence;


    public int statPoints = 0;
    public int skillPoint = 0;
    public int portalsEntered;


    public int levelUp()
    {
        Playerlevel++;
        statPoints++;
        skillPoint++;
        return Playerlevel;
    }


    public int addStrength()
    {

        if (statPoints != 0)
        {
            str++;
            return str;
        }
        else
        {
            return -1;
        }
    }

    public int addDex()
    {
        if (statPoints != 0)
        {
            dex++;
            return dex;
        }
        else
        {
            return -1;
        }
    }

    public int addIntelligence()
    {
        if (statPoints != 0)
        {
            intelligence++;
            return intelligence;
        }
        else
        {
            return -1;
        }

    }

    public int addLuck()
    {
        if (statPoints != 0)
        {
            luck++;
            return luck;
        }
        else
        {
            return -1;
        }

    }



    public float damageDealt()
    {

        critChance = luck + 1 / 10; //0.2
        //for mage class
        damage = intelligence + (intelligence * critChance);

        return damage;

    }



}
