using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [Header("Player Class and Level")]
    public string PlayerClass = "mage";
    public int Playerlevel = 1;
    // exp that player has
    public int currentExp;
    //exp that player needs  
    public int expNeededToNextLevel;
    public int statPoints = 0;
    public int skillPoint = 0;


    [Header("Player's Stat")]
    //warrior starts with str 15
    public int str = 10;

    //archer starts with dex 15
    public int dex = 10;

    //mage starts with 15 int
    public int intelligence = 15;

    //thief starts with 15 
    public int luck = 10;


    [Header("Player's HP bar and related stuff")]
    //increase str to increase HP
    public int MaxHpBar;
    //increase int to increase mana
    public int MaxManaBar;
    //increase dex to increase stamina
    public int MaxStamina;
    public int MaxHungerBar = 100;
    public int healthRegen;


    [Header("Player's misc stuff")]
    // Easy 5 death normal 3 death Hard 1 death
    public int DeathCount;
    public int portalsEntered;

    //increase luck to increase crit chance calculated in damage dealt 
    private float critChance;
    private float damage;
    //each point of main stat of that class = 1 def (example: Mage(Intelligence) 10 int = to 10 defence)
    private int defence;

    public int checkForLevelUp()
    {
        if (currentExp > expNeededToNextLevel)
        {
            Playerlevel++;

            //each level up gives 5 stat points
            statPoints += 5;

            //each level up gives 1 skill point
            skillPoint++;

            expNeededToNextLevel = Playerlevel * 100;
            currentExp = 0;
            return Playerlevel;

        }
        else
        {
            return Playerlevel;
        }

    }


    public int addStrength()
    {

        if (statPoints != 0)
        {
            str++;
            statPoints--;
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
            statPoints--;
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
            statPoints--;
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
            statPoints--;
            return luck;
        }
        else
        {
            return -1;
        }

    }


    //calculate damage dealt base on stats. not including skill stats
    public float damageDealt()
    {
        float cirtHit;
        critChance = luck + (1 / 10) * 100; //20% chance to crit if luck is 10

        //for mage class
        if (PlayerClass == "mage")
        {
            cirtHit = Random.Range(0, critChance);
            if (cirtHit == 0)
            {
                damage = intelligence * 2;
            }
            else
            {
                damage = intelligence;
            }

            return damage;

        }

        //for warrior 
        if (PlayerClass == "warrior")
        {
            cirtHit = Random.Range(0, critChance);
            if (cirtHit == 0)
            {
                damage = str * 2;
            }
            else
            {
                damage = str;
            }

            return damage;

        }

        //for archer
        if (PlayerClass == "archer")
        {
            cirtHit = Random.Range(0, critChance);
            if (cirtHit == 0)
            {
                damage = dex * 2;
            }
            else
            {
                damage = dex;
            }

            return damage;

        }

        //for thief
        if (PlayerClass == "archer")
        {
            cirtHit = Random.Range(0, critChance);
            if (cirtHit == 0)
            {
                damage = luck * 2;
            }
            else
            {
                damage = luck;
            }

            return damage;

        }

        return damage;


    }



}
