using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour, IDataPersistence
{
    public Slider staminaBar;
    public PlayerStat playerStat;
    public Text staminaText;


    public float totalStamina;
    public float currentStamina;
    private string biome;
    private int biomeResistSkillValue;
    private float regenSpeed;

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
        staminaText.text = (int)currentStamina + " / " + totalStamina;


        staminaBar.maxValue = totalStamina;
        staminaBar.value = currentStamina;
        startRegen();

        if (biome == "")
        {
            biome = NewGame.biomeSelection;

        }
    }
    public void startRegen()
    {

        regen = StartCoroutine(RegenStamina());



    }

    public void onDexUp(float maxStamina)
    {
        totalStamina = maxStamina;
        //player's total stamina on slider
        staminaBar.maxValue = totalStamina;
        staminaText.text = (int)currentStamina + " / " + totalStamina;

    }

    public void onStamRegenSkillUp(float stamRegen)
    {
        regenSpeed = stamRegen;
    }

    public void onBiomeResistSkillUp(int biomeSkillValue)
    {
        biomeResistSkillValue = biomeSkillValue;
    }

    public bool UseStamina(float stamina)
    {
        if (currentStamina >= stamina)
        {

            currentStamina -= stamina;
            staminaBar.value = currentStamina;

            staminaText.text = (int)currentStamina + " / " + totalStamina;


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
        regenSpeed = playerStat.staminaRegen;

        float snowRegenDelimiter = 0.5f;
        if (biome == "snow")
        {
            if (biomeResistSkillValue == 0)
            {
                snowRegenDelimiter = 0.7f;
            }
            if (biomeResistSkillValue == 1)
            {
                snowRegenDelimiter = 0.5f;
            }
            else if (biomeResistSkillValue == 2)
            {
                snowRegenDelimiter = 0.3f;
            }
            else if (biomeResistSkillValue == 3)
            {
                snowRegenDelimiter = 0.1f;
            }

            yield return new WaitForSeconds(2);
            while (currentStamina < totalStamina)
            {
                //Debug.Log("regening stamina in snow is " + (regenSpeed - snowRegenDelimiter));
                //regen slower in snow region

                //currentStamina += totalStamina / 150;
                currentStamina += (regenSpeed - snowRegenDelimiter);
                staminaBar.value = currentStamina;
                staminaText.text = (int)currentStamina + " / " + totalStamina;

                yield return regenTick;
            }
            regen = null;


        }
        else
        {
            yield return new WaitForSeconds(2);
            while (currentStamina < totalStamina)
            {
                //currentStamina += totalStamina / 70;
                currentStamina += regenSpeed;
                staminaBar.value = currentStamina;
                staminaText.text = (int)currentStamina + " / " + totalStamina;

                yield return regenTick;
            }
            regen = null;

        }

    }

    public void recoverStaminaFull()
    {
        currentStamina = totalStamina;
    }

    public void LoadData(GameData data)
    {
        biome = data.biome;
        currentStamina = data.currentStamina;
        biomeResistSkillValue = data.biomeResistSkillValue;
    }

    public void SaveData(ref GameData data)
    {
        data.currentStamina = currentStamina;

    }


}
