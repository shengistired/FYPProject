using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IDataPersistence
{
    public Slider healthBar;
    public PlayerStat playerStat;
    public FoodBar foodBar;

    public float totalHp;
    private float currentHp;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);

    //HP regen amount
    private Coroutine regen;

    public static HealthBar instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(currentHp == -1)
        {
            currentHp = playerStat.MaxHpBar;
        }
        totalHp = playerStat.MaxHpBar;



        //player's total health
        healthBar.maxValue = totalHp;
        //player's current health
        healthBar.value = currentHp;



    }



    public void takeDamage(int damage)
    {
        if (currentHp - damage > 0)
        {
            currentHp -= damage;
            healthBar.value = currentHp;
            Debug.Log("Taken " + damage + " HP left is " + healthBar.value);
            checkFoodBarRegen();
        }
        else if (damage >= currentHp || damage > currentHp || currentHp == 0)
        {
            currentHp -= damage;
            healthBar.value = currentHp;
            Debug.Log("Taken " + damage + " HP left is " + healthBar.value);
            PlayerDied();
        }
    }

    public void onStrengthUp(float maxHp)
    {
        totalHp = maxHp;
        //player's total health on slider
        healthBar.maxValue = totalHp;
    }

    public void checkFoodBarRegen()
    {
        //if foodbar above 70% then regen
        if (FoodBar.food >= 140f && currentHp < totalHp)
        {
            regen = StartCoroutine(RegenHealth());
        }
        //regen stops when below 70% hunger
        else if (FoodBar.food < 140f)
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
            currentHp += 1;
            healthBar.value = currentHp;
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
    }

    public void SaveData(ref GameData data)
    {
        data.currentHP = currentHp;
    }
}