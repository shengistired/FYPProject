using UnityEngine;
using TMPro;

public class PlayerStat : MonoBehaviour, IDataPersistence
{
    [Header("Player Class and Level")]
    public string PlayerClass = "mage";
    public int Playerlevel = 1;
    // exp that player has
    public int currentExp;
    //exp that player needs  

    public int expNeededToNextLevel;
    public int statPoints = 5;
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
    //increase str to increase HP (each stat increase HP by 2 )
    public float MaxHpBar;
    //increase int to increase mana  (each stat increase Mana by 2 )
    public float MaxManaBar;
    //increase dex to increase stamina  (each stat increase Stamina by 2 )
    public float MaxStamina;

    public int MaxHungerBar = 100;

    public float healthRegen = 10;
    public float manaRegen = 10;
    public float staminaRegen = 10;


    [Header("Player's misc stuff")]
    // Easy 5 death normal 3 death Hard 1 death
    public int DeathCount;
    public int portalsEntered;

    //increase luck to increase crit chance calculated in damage dealt 
    private float critChance;
    private float damage;
    //each point of main stat of that class = 1 def (example: Mage(Intelligence) 10 int = to 10 defence)
    private int defence;

    [SerializeField] private Stats_UI stats_UI;
    private bool statScreen = false;



    public void openStatScreen()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && statScreen == true)
        {
            stats_UI.gameObject.SetActive(false);
            statScreen = false;
        }
        if (Input.GetKeyDown("f"))
        {

            if (statScreen == false)
            {

                updateStats();
                stats_UI.gameObject.SetActive(true);
                statScreen = true;
            }
            else
            {
                stats_UI.gameObject.SetActive(false);
                statScreen = false;

            }
        }
    }

    private void updateStats()
    {
        string level = Playerlevel.ToString();
        string stats = statPoints.ToString();
        string strength = str.ToString();
        string dexString = dex.ToString();
        string intString = intelligence.ToString();
        string luckString = luck.ToString();
        stats_UI.getStats(level, stats, strength, dexString, intString, luckString);

    }

    public float calculateTotalHP()
    {
        //MaxHpBar = 100;
        //each stat of strength gives 2 hp
        MaxHpBar += (str * 2);
        return MaxHpBar;
    }

    public float calculatetotalStamina()
    {
        //MaxStamina = 100;
        //each stat of dex gives 2 stamina
        MaxStamina += (dex * 2);
        return MaxStamina;
    }

    public float calculateTotalMana()
    {
        //MaxManaBar = 100;
        //each stat of intelligence gives 2 mana
        MaxManaBar += (intelligence * 2);
        return MaxManaBar;
    }

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


    public void addStrength()
    {

        if (statPoints != 0)
        {
            str++;
            statPoints--;
            calculateTotalHP();
            updateStats();
        }
        else
        {
            Debug.Log("No stats point");
        }
    }

    public void addDex()
    {
        if (statPoints != 0)
        {
            dex++;
            statPoints--;
            calculatetotalStamina();
            updateStats();
        }
        else
        {
            Debug.Log("No stats point");
        }
    }

    public void addIntelligence()
    {
        if (statPoints != 0)
        {
            intelligence++;
            statPoints--;
            calculateTotalMana();
            updateStats();
        }
        else
        {
            Debug.Log("No stats point");
        }

    }

    public void addLuck()
    {
        if (statPoints != 0)
        {
            luck++;
            statPoints--;
            updateStats();
        }
        else
        {
            Debug.Log("No stats point");
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

    public void LoadData(GameData data)
    {
        Playerlevel = data.playerlevel;
        currentExp = data.currentExp;
        expNeededToNextLevel = data.expNeededToNextLevel;
        statPoints = data.statPoints;
        skillPoint = data.skillPoint;
        str = data.str;
        dex = data.dex;
        intelligence = data.intelligence;
        luck = data.luck;
        MaxHpBar = data.maxHpBar;
        MaxManaBar = data.maxManaBar;
        MaxStamina = data.maxStamina;


    }

    public void SaveData(ref GameData data)
    {
        data.playerlevel = Playerlevel;
        data.currentExp = currentExp;
        data.expNeededToNextLevel = expNeededToNextLevel;
        data.statPoints = statPoints;
        data.skillPoint = skillPoint;
        data.str = str;
        data.dex = dex;
        data.intelligence = intelligence;
        data.luck = luck;
        data.maxHpBar = MaxHpBar;
        data.maxManaBar = MaxManaBar;
        data.maxStamina = MaxStamina;
    }
}
