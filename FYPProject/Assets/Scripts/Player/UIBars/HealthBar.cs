using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public PlayerStat playerStat;

    private int totalHp;
    private int currentHp;

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
        currentHp = playerStat.calculateTotalHP();
        totalHp = playerStat.calculateTotalHP();

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

            //uncomment for hp regen
            // if (regen != null)
            //     StopCoroutine(regen);

            // regen = StartCoroutine(RegenHealth());

        }
        else if (damage >= currentHp || damage > currentHp || currentHp == 0)
        {
            currentHp -= damage;
            healthBar.value = currentHp;
            Debug.Log("Taken " + damage + " HP left is " + healthBar.value);
            PlayerDied();
        }
    }



    //regen health as a skill maybe
    private IEnumerator RegenHealth()
    {
        yield return new WaitForSeconds(2);
        while (currentHp < totalHp)
        {
            //currentHealth += numberiwant
            // add for hp regen set>>>> playerStat.healthRegen;
            currentHp += 20;
            healthBar.value = currentHp;
            yield return regenTick;
        }
        regen = null;
    }

    private void PlayerDied()
    {
        {
            LevelManager.instance.GameOver();
            gameObject.SetActive(false);
        }
    }
}