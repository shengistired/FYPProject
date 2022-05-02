using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour, IDataPersistence
{
    public Slider staminaBar;
    public PlayerStat playerStat;

    private float totalStamina;
    public float currentStamina;
    private string biome;


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
        // currentStamina = playerStat.calculatetotalStamina();
        // totalStamina = playerStat.calculatetotalStamina();


        //currentStamina = playerStat.MaxStamina;

        totalStamina = playerStat.MaxStamina;

        staminaBar.maxValue = totalStamina;
        staminaBar.value = totalStamina;
        RegenStamina();

        if (biome == "")
        {
            biome = NewGame.biomeSelection;

        }
    }

    public void onDexUp(float maxStamina)
    {
        totalStamina = maxStamina;
        //player's total stamina on slider
        staminaBar.maxValue = totalStamina;
    }

    public bool UseStamina(float stamina)
    {
        if (currentStamina >= stamina)
        {

            currentStamina -= stamina;
            staminaBar.value = currentStamina;

            if (regen != null)
            {
                StopCoroutine(regen);
            }


            regen = StartCoroutine(RegenStamina());
            return true;
        }
        else if (stamina >= currentStamina)
        {
            Debug.Log("Not enough stamina!");
            PlayerController.running = false;
            return false;
        }
        return false;
    }

    private IEnumerator RegenStamina()
    {
        if (biome == "snow")
        {
            yield return new WaitForSeconds(2);
            while (currentStamina < totalStamina)
            {
                //regen slower in snow region
                currentStamina += totalStamina / 150;
                staminaBar.value = currentStamina;
                yield return regenTick;
            }
            regen = null;


        }
        else
        {
            yield return new WaitForSeconds(2);
            while (currentStamina < totalStamina)
            {
                currentStamina += totalStamina / 70;
                staminaBar.value = currentStamina;
                yield return regenTick;
            }
            regen = null;

        }

    }

    public void LoadData(GameData data)
    {
        biome = data.biome;
        currentStamina = data.currentStamina;
    }

    public void SaveData(ref GameData data)
    {
        data.currentStamina = currentStamina;

    }


}
