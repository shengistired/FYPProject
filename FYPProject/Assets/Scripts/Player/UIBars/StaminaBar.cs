using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaBar;
    public PlayerStat playerStat;

    private int totalStamina;
    public int currentStamina;


    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    public static StaminaBar instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //currentStamina = maxStamina;
        currentStamina = playerStat.calculatetotalStamina();
        totalStamina = playerStat.calculatetotalStamina();

        staminaBar.maxValue = totalStamina;
        staminaBar.value = totalStamina;
    }

    public bool UseStamina(int stamina)
    {
        if (currentStamina >= stamina)
        {
            currentStamina -= stamina;
            staminaBar.value = currentStamina;

            if (regen != null)
                StopCoroutine(regen);

            regen = StartCoroutine(RegenStamina());
            return true;
        }
        else
        {
            Debug.Log("Not enough stamina!");
            PlayerController.running = false;
            return false;
        }
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);

        while (currentStamina < totalStamina)
        {
            currentStamina += totalStamina / 100;
            staminaBar.value = currentStamina;
            yield return regenTick;
        }
        regen = null;
    }



}
