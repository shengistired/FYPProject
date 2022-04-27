using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManaBar : MonoBehaviour
{
    public Slider manaBar;
    public PlayerStat playerStat;

    private float totalMana;
    private float currentMana;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    public static ManaBar instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // currentMana = playerStat.calculateTotalMana();
        // totalMana = playerStat.calculateTotalMana();
        currentMana = playerStat.MaxManaBar;
        totalMana = playerStat.MaxManaBar;
        //Debug.Log("currentMana"+currentMana);
        //Debug.Log("totalMana"+totalMana);

        manaBar.maxValue = totalMana;
        manaBar.value = currentMana;

    }





    public void onIntUp(float maxMana)
    {
        totalMana = maxMana;
        //player's total mana on slider
        manaBar.maxValue = totalMana;
        regenMana();
    }

    public bool useMana(int mana)
    {
        if (currentMana > mana)
        {
            currentMana -= mana;
            manaBar.value = currentMana;
            regenMana();
            return true;
        }
        else
        {
            Debug.Log("Not enough mana!");
            return false;
        }
    }

    private void regenMana()
    {
        //mana regen
        if (currentMana == totalMana)
        {
            StopCoroutine(regen);
        }

        else
        {
            regen = StartCoroutine(manaRegen());
        }

    }

    //manaRegen
    private IEnumerator manaRegen()
    {
        yield return new WaitForSeconds(2);

        while (currentMana < totalMana)
        {
            // replace bottom with this for the mana regen you set in playerstat.cs >>> currentMana += playerStat.manaRegen;
            currentMana += totalMana / 100;
            manaBar.value = currentMana;
            yield return regenTick;
        }
        regen = null;
    }
}
