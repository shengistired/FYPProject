using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public PlayerStat playerStat;

    private float totalHp;
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
        currentHp = playerStat.MaxHpBar = 100 + (playerStat.str * 2);
        totalHp = playerStat.MaxHpBar = 100 + (playerStat.str * 2);
        //currentHealth = maxHealth;
        //player's total health
        healthBar.maxValue = playerStat.MaxHpBar = 100 + (playerStat.str * 2); ;
        //player's current health
        healthBar.value = currentHp;

        //healthBar.maxValue = maxHealth;
        //healthBar.value = maxHealth;

    }

    public void takeDamage(int damage)
    {
        if (currentHp - damage >= 0)
        {
            currentHp -= damage;
            healthBar.value = currentHp;

            //uncomment for hp regen
            // if (regen != null)
            //     StopCoroutine(regen);

            // regen = StartCoroutine(RegenHealth());
        }
        if (damage > currentHp)
        {
            PlayerDied();
        }
    }

    private void PlayerDied()
    {
        {
            LevelManager.instance.GameOver();
            gameObject.SetActive(false);
        }
    }

    //regen health as a skill maybe
    private IEnumerator RegenHealth()
    {
        yield return new WaitForSeconds(2);
        while (currentHp < totalHp)
        {
            //currentHealth += numberiwant
            currentHp += 20;
            healthBar.value = currentHp;
            yield return regenTick;
        }
        regen = null;
    }
}