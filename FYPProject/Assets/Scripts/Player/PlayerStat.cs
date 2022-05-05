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

    public int life;

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
 
    public float healthRegen = 0.2f;
    public float manaRegen = 0.2f;
    public float staminaRegen = 0.2f;


    [Header("Player's misc stuff")]
    // Easy 5 death normal 3 death Hard 1 death
    public int DeathCount;
    public int portalsEntered;

    //increase luck to increase crit chance calculated in damage dealt 
    private float critChance;
    private float damage;

    [Header("Survival skills")]
    private int healthRegenSkillValue;
    private int staminaRegenSkillValue;
    private int manaRegenSkillValue;
    private int hungerResistSkillValue;
    private int biomeResistSkillValue;

    [SerializeField] private Stats_UI stats_UI;
    [SerializeField] private Skills_UI skills_UI;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ManaBar manaBar;
    [SerializeField] private StaminaBar staminaBar;
    private bool statScreen = false;
    private bool skillScreen = false;

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

    public void openSkillScreen()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && skillScreen == true)
        {
            skills_UI.gameObject.SetActive(false);
            skillScreen = false;
        }
        if (Input.GetKeyDown("g"))
        {

            if (skillScreen == false)
            {

                updateStats();
                skills_UI.gameObject.SetActive(true);
                skillScreen = true;
            }
            else
            {
                skills_UI.gameObject.SetActive(false);
                skillScreen = false;

            }
        }
    }


    private void updateStats()
    {
        //player stats
        string level = Playerlevel.ToString();
        string stats = statPoints.ToString();
        string strength = str.ToString();
        string dexString = dex.ToString();
        string intString = intelligence.ToString();
        string luckString = luck.ToString();
        stats_UI.getStats(level, stats, strength, dexString, intString, luckString);


        //player survival skills
        string skillPointString = skillPoint.ToString();
        string healthRegenString = healthRegenSkillValue.ToString();
        string staminaRegenString = staminaRegenSkillValue.ToString();
        string manaRegenString = manaRegenSkillValue.ToString();
        string hungerResistString = hungerResistSkillValue.ToString();
        string biomeResistString = biomeResistSkillValue.ToString();

        skills_UI.getSkillPoint(skillPointString, healthRegenString, staminaRegenString, manaRegenString, hungerResistString, biomeResistString);

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
            DataPersistenceManager.instance.SaveGame();
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
            DataPersistenceManager.instance.SaveGame();
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
            DataPersistenceManager.instance.SaveGame();
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
            DataPersistenceManager.instance.SaveGame();
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
            DataPersistenceManager.instance.SaveGame();
        }
        else
        {
            Debug.Log("No stats point");
        }


    }

    //survival skills for all classes
    public void addHealthRegenSkill()
    {

        if (skillPoint != 0 && healthRegenSkillValue <= 3)
        {
            //level 1 skill
            if (healthRegenSkillValue == 0)
            {
                skillPoint--;
                healthRegen = 0.4f;
                healthRegenSkillValue = 1;
                healthBar.onHpRegenSkillUp(healthRegen);
                updateStats();


            }

            //level 2 skill
            else if (healthRegenSkillValue == 1)
            {
                skillPoint--;
                healthRegen = 0.6f;
                healthRegenSkillValue = 2;
                healthBar.onHpRegenSkillUp(healthRegen);
                updateStats();

            }

            //level 3 skill
            else if (healthRegenSkillValue == 2)
            {
                skillPoint--;
                healthRegen = 1f;
                healthRegenSkillValue = 3;
                healthBar.onHpRegenSkillUp(healthRegen);
                updateStats();

            }
        }
        else
        {
            Debug.Log("No stats point or max skill level reached");
        }
        DataPersistenceManager.instance.SaveGame();

    }

    public void addStaminaRegenSkill()
    {
        

    }

    public void addManaRegenSkill()
    {

    }

    public void addHungerResistSkill()
    {

    }

    public void addBiomeResistSkill()
    {

    }
    //End of survival skills//



    //Combat skills//




    //End of combat Skills//

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
        healthRegen = data.healthRegen;
        staminaRegen = data.staminaRegen;
        manaRegen = data.manaRegen;
        healthRegenSkillValue = data.healthRegenSkillValue;
        staminaRegenSkillValue = data.staminaRegenSkillValue;
        manaRegenSkillValue = data.manaRegenSkillValue;
        hungerResistSkillValue = data.hungerResistSkillValue;
        biomeResistSkillValue = data.biomeResistSkillValue;
        life = data.life;


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
        data.healthRegen = healthRegen;
        data.manaRegen = manaRegen;
        data.staminaRegen = staminaRegen;
        data.healthRegenSkillValue = healthRegenSkillValue;
        data.staminaRegenSkillValue = staminaRegenSkillValue;
        data.manaRegenSkillValue = manaRegenSkillValue;
        data.hungerResistSkillValue = hungerResistSkillValue;
        data.biomeResistSkillValue = biomeResistSkillValue;
        data.life = life;
        //save healthRegenSkillValue staminaRegenSkillValue manaRegenSkillValue hungerResistSkillValue biomeResistSkillValue
    }
}
