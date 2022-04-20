using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public PlayerStat playerStat;

    private int maxHealth = 200;
    private float currentHealth;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    public static HealthBar instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;

    }

    public void UseHealth(int amount)
    {
            if(currentHealth - amount >= 0)
            {
                currentHealth -= amount;
                healthBar.value = currentHealth;

                // if (regen != null)
                //     StopCoroutine(regen);

                // regen = StartCoroutine (RegenHealth());
            }
            else if (currentHealth <=0)
            {
                PlayerDied();
            }
    }
    
    private void PlayerDied (){
        {
            LevelManager.instance.GameOver();
            gameObject.SetActive (false);
        }
    }
    
    //regen health as a skill
    private IEnumerator RegenHealth()
    {
        yield return new WaitForSeconds(2);

        
        while (currentHealth < maxHealth)
        {   
            //currentHealth += numberiwant
            currentHealth += maxHealth / 100;
            healthBar.value = currentHealth;
            yield return regenTick;
        }
        regen = null;
    }
}