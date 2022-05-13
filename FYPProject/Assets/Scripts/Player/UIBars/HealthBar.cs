using System.Xml.Schema;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IDataPersistence
{
    public Slider healthBar;
    public PlayerStat playerStat;
    public StaminaBar staminaBar;
    public ManaBar manaBar;
    public HungerBar hungerBar;
    public Death_UI death_UI;
    public Text hpText;

    public float totalHp;
    private float currentHp;
    private float defence;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);

    //only take damage once every second
    private float damageTakenTime = 0f;
    private float damageTakenCoolDown = 1f;

    //HP regen amount
    private Coroutine regen;
    private float regenSpeed;
    private bool regening = false;

    public static HealthBar instance;
    private string biome;
    private int playerLife;

    private float maxMana;
    private float maxStamina;
    private float MaxHungerBar;
    private int biomeResistSkillValue;



    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {


        totalHp = playerStat.MaxHpBar;
        // hpText.text = currentHp + " / " + totalHp;

        hpText.text = (int)currentHp + " / " + totalHp;

        //player's total health
        healthBar.maxValue = totalHp;
        //player's current health
        healthBar.value = currentHp;

        checkFoodBarRegen();

        if (biome == "")
        {
            biome = NewGame.biomeSelection;

        }

        if (playerLife == -1)
        {
            playerLife = NewGame.life;
        }

    }


    private void FixedUpdate()
    {
        if (biome == "desert")
        {
            desertBurn(0.04f);
        }
    }

    public void onBiomeResistSkillUp(int biomeSkillValue)
    {
        biomeResistSkillValue = biomeSkillValue;
    }

    public void desertBurn(float damage)
    {
        biomeResistSkillValue = playerStat.biomeResistSkillValue;
        if (biomeResistSkillValue == 1)
        {
            damage = 0.03f;
        }

        if (biomeResistSkillValue == 2)
        {
            damage = 0.02f;
        }

        if (biomeResistSkillValue == 3)
        {
            damage = 0.01f;
        }

        if (currentHp - damage > 0)
        {
            //Debug.Log("Desert burning damage is " + damage);
            currentHp -= damage;

            healthBar.value = currentHp;
            hpText.text = (int)currentHp + " / " + totalHp;

            //Debug.Log("Desert burn damage " + damage + " HP left is " + healthBar.value);

        }
        else if (damage >= currentHp || damage > currentHp || currentHp == 0)
        {
            currentHp -= damage;


            healthBar.value = currentHp;
            hpText.text = (int)currentHp + " / " + totalHp;

            //Debug.Log("Desert burn damage " + damage + " HP left is " + healthBar.value);
            PlayerDied();
        }

    }

    public void takeDamage(float damage)
    {
        if (regen != null)
        {
            StopCoroutine(regen);
            regen = null;
            regening = false;

        }
        regening = false;

        //only take damage once every second
        if (Time.time > damageTakenTime + damageTakenCoolDown)
        {
            //defence = playerStat.getDefence();
            defence = getDefence();
            damageTakenTime = Time.time;

            //Debug.Log("Defence is " + defence);
            // Debug.Log("Damage taken before defence is " + damage);
            damage -= defence;
            //Debug.Log("Damage taken after defence is " + damage);

            //if enemy dealing less than 1 damage set damage to 1.
            if (damage <= 0)
            {
                damage = 1;
            }
            if (currentHp - damage > 0)
            {
                currentHp -= damage;
                healthBar.value = currentHp;
                hpText.text = (int)currentHp + " / " + totalHp;
                Debug.Log("Taken " + damage + "damage, HP left is " + healthBar.value);

                //run function to check if player can regen after

                checkFoodBarRegen();
            }
            else if (damage >= currentHp || damage > currentHp || currentHp == 0)
            {
                currentHp -= damage;
                healthBar.value = currentHp;
                hpText.text = (int)currentHp + " / " + totalHp;
                Debug.Log("Taken " + damage + "damage, HP left is " + healthBar.value);
                PlayerDied();
            }

        }

    }

    //defence is player's class main stat divided by 2.
    public float getDefence()
    {
        //Debug.Log(playerStat.PlayerClass);
        if (playerStat.PlayerClass == "warrior")
        {
            defence = playerStat.str / 2f;
            return defence;
        }
        if (playerStat.PlayerClass == "mage")
        {
            //Debug.Log("Player int is " + playerStat.intelligence);
            defence = playerStat.intelligence / 2f;
            return defence;
        }
        if (playerStat.PlayerClass == "archer")
        {
            defence = playerStat.dex / 2f;
            return defence;
        }
        if (playerStat.PlayerClass == "thief")
        {
            defence = playerStat.luck / 2f;
            return defence;
        }

        else
        {
            defence = playerStat.str / 2f;

        }

        return defence;
    }

    public void onStrengthUp(float maxHp)
    {
        totalHp = maxHp;
        //player's total health on slider
        healthBar.maxValue = totalHp;
        hpText.text = (int)currentHp + " / " + totalHp;
    }

    public void checkFoodBarRegen()
    {
        // Debug.Log("my hunger is " + HungerBar.currentHunger);
        //if foodbar above 70% then regen
        // if (HungerBar.currentHunger >= 140f && (currentHp < totalHp))
        // {
        //     regen = StartCoroutine(RegenHealth());
        // }
        // //regen stops when below 70% hunger
        // if (HungerBar.currentHunger < 140f)
        // {

        //     StopCoroutine(regen);
        //     regen = null;

        // }
        if (regening == false)
        {
            regen = StartCoroutine(RegenHealth());
            regening = true;
        }

    }


    public void onHpRegenSkillUp(float healthRegen)
    {
        regenSpeed = healthRegen;
    }
    //increase regen rate as skill
    public IEnumerator RegenHealth()
    {
        regenSpeed = playerStat.healthRegen;
        //Debug.Log("regnespeed " + regenSpeed);

        if (regening == false)
        {
            yield return new WaitForSeconds(2);
            while (currentHp < totalHp && HungerBar.currentHunger > 140f)
            {

                //currentHealth += numberiwant
                // add for hp regen set>>>> playerStat.healthRegen;

                //default healing with no skill 0.2f
                //Debug.Log("regening >>> " + regenSpeed);
                currentHp += regenSpeed;
                if (currentHp > totalHp)
                {
                    currentHp = totalHp;
                }
                healthBar.value = currentHp;
                hpText.text = (int)currentHp + " / " + totalHp;
                // regening = true;
                yield return regenTick;



            }
            if (HungerBar.currentHunger <= 140f)
            {
                StopCoroutine(regen);
                regening = false;
                regen = null;
            }

        }
        if (regening == true)
        {
            if (HungerBar.currentHunger <= 140f)
            {
                StopCoroutine(regen);
                regening = false;
                regen = null;
            }
        }


    }

    private void PlayerDied()
    {


        if (playerStat.life > 0)
        {
            playerLife--;
            death_UI.getPlayerLife(playerLife);
            DataPersistenceManager.instance.SaveGame();
            Debug.Log("life left is " + playerLife);
        }

        else if (playerStat.life == 0)
        {
            //playerLife--;
            death_UI.getPlayerLife(playerLife);
            DataPersistenceManager.instance.SaveGame();
            Debug.Log("life left is " + playerLife);
        }

        //set hp mana stamina hunger to full
        currentHp = totalHp;
        manaBar.recoverManaFull();
        staminaBar.recoverStaminaFull();
        hungerBar.recoverHungerBar();

        //Debug.Log("mana " + maxMana + " stamina " + maxStamina + " hunger " + MaxHungerBar);
        death_UI.getPlayerLife(playerLife);
        death_UI.ToggleDeathPanel(playerLife);
        DataPersistenceManager.instance.SaveGame();
        gameObject.SetActive(false);
        

    }

    public void LoadData(GameData data)
    {
        currentHp = data.currentHP;
        biome = data.biome;
        playerLife = data.life;
        biomeResistSkillValue = data.biomeResistSkillValue;
    }

    public void SaveData(ref GameData data)
    {

        data.life = playerLife;
        data.currentHP = currentHp;



    }
}