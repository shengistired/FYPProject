using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IDataPersistence
{
    public Slider healthBar;
    public PlayerStat playerStat;
    public HungerBar hungerBar;
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
    private string biome;

    public static HealthBar instance;

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

    }


    private void FixedUpdate()
    {
        if (biome == "desert")
        {
            desertBurn(0.05f);
        }
    }

    public void desertBurn(float damage)
    {
        if (currentHp - damage > 0)
        {
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
        //stop regen when taking damage
        if (regen != null)
        {
            StopCoroutine(regen);
            regen = null;
        }


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
        if (HungerBar.currentHunger >= 140f && (currentHp < totalHp))
        {
            regen = StartCoroutine(RegenHealth());
        }
        //regen stops when below 70% hunger
        if (HungerBar.currentHunger < 140f)
        {

            StopCoroutine(regen);
            regen = null;

        }

    }



    //increase regen rate as skill
    public IEnumerator RegenHealth()
    {
        yield return new WaitForSeconds(2);
        while (currentHp < totalHp)
        {
            //currentHealth += numberiwant
            // add for hp regen set>>>> playerStat.healthRegen;
            currentHp += 0.5f;
            healthBar.value = currentHp;
            hpText.text = (int)currentHp + " / " + totalHp;
            yield return regenTick;
        }
        //regen = null;
    }

    private void PlayerDied()
    {
        {
            LevelManager.instance.GameOver();
            gameObject.SetActive(false);
        }
    }

    public void LoadData(GameData data)
    {
        currentHp = data.currentHP;
        biome = data.biome;
    }

    public void SaveData(ref GameData data)
    {
        data.currentHP = currentHp;
    }
}