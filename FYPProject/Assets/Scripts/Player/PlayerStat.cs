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
    public int str;

    //archer starts with dex 15
    public int dex;

    //mage starts with 15 int
    public int intelligence;

    //thief starts with 15 
    public int luck;


    [Header("Player's HP bar and related stuff")]
    //increase str to increase HP (each stat increase HP by 2 )
    public float MaxHpBar;
    //increase int to increase mana  (each stat increase Mana by 2 )
    public float MaxManaBar;
    //increase dex to increase stamina  (each stat increase Stamina by 2 )
    public float MaxStamina;

    public int MaxHungerBar;

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



    [SerializeField] private Stats_UI stats_UI;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ManaBar manaBar;
    [SerializeField] private StaminaBar staminaBar;
    private bool statScreen = false;
    void FixedUpdate()
    {
        //str_add.onClick.AddListener(playerStat.addStrength);
        checkForLevelUp();
    }

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
        MaxHpBar = 100 + (str * 2);
        healthBar.onStrengthUp(MaxHpBar);
        healthBar.checkFoodBarRegen();
        return MaxHpBar;
    }

    public float calculatetotalStamina()
    {
        //MaxStamina = 100;
        //each stat of dex gives 2 stamina
        MaxStamina = 100 + (dex * 2);

        staminaBar.onDexUp(MaxStamina);
        return MaxStamina;
    }

    public float calculateTotalMana()
    {
        //MaxManaBar = 100;
        //each stat of intelligence gives 2 mana
        MaxManaBar = 100 + (intelligence * 2);
        manaBar.onIntUp(MaxManaBar);

        return MaxManaBar;
    }

    public int checkForLevelUp()
    {
        if (currentExp >= expNeededToNextLevel)
        {
            Playerlevel++;

            //each level up gives 5 stat points
            statPoints += 5;

            //each level up gives 1 skill point
            skillPoint++;
            currentExp -= expNeededToNextLevel;
            expNeededToNextLevel = Playerlevel * 100;
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
    public float damageDealt(float extraDmg)
    {
        int cirtHit;
        Debug.Log("my luck is " + luck);
        critChance = (luck * 2) / 10; //20% chance to crit if luck is 10
        Debug.Log("crit CHANCE is " + critChance);
        //for mage class
        if (PlayerClass == "mage")
        {
            cirtHit = Random.Range((int)critChance, 10);
            Debug.Log("crithit is " + cirtHit);
            if (cirtHit == (int)critChance)
            {
                damage = (intelligence * extraDmg) * 2;
                Debug.Log("crit damage is " + damage);
                return damage;
            }
            else
            {
                damage = intelligence * extraDmg;
                Debug.Log("Non crit damage is " + damage);
                return damage;
            }
            //Debug.Log("i am dealing " + damage + " damage");
            //return damage;

        }

        //for warrior 
        if (PlayerClass == "warrior")
        {
            cirtHit = Random.Range((int)critChance, 10);
            if (cirtHit == (int)critChance)
            {
                damage = (str * extraDmg) * 2;
                return damage;
            }
            else
            {
                damage = str * extraDmg;
                return damage;
            }


        }

        //for archer
        if (PlayerClass == "archer")
        {
            cirtHit = Random.Range((int)critChance, 10);
            if (cirtHit == (int)critChance)
            {
                damage = (dex * extraDmg) * 2;
                return damage;
            }
            else
            {
                damage = dex * extraDmg;
                return damage;
            }


        }

        //for thief
        if (PlayerClass == "thief")
        {
            cirtHit = Random.Range((int)critChance, 10);
            if (cirtHit == (int)critChance)
            {
                damage = (luck * extraDmg) * 2;
                return damage;
            }
            else
            {
                damage = luck * extraDmg;
                return damage;
            }



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
