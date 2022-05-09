using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HungerBar : MonoBehaviour, IDataPersistence
{
    public Slider hungerBar;
    public Text hungerText;
    public PlayerStat playerStat;
    public static float currentHunger;
    public float maxFood = 200f;
    public HealthBar healthBar;
    public audio_manager music;
    private float degenSpeed;



    public bool UseFood(float amount)
    {
        Debug.Log("Current Hunger " + currentHunger + "eat amount" + amount);
        if (currentHunger + amount <= maxFood)
        {

            if (currentHunger + amount > maxFood)
            {
                currentHunger = maxFood;
            }
            else
            {
                currentHunger += amount;
            }
            Debug.Log("yummy");
            hungerBar.value = currentHunger;
            hungerText.text = (int)currentHunger + " / " + maxFood;

            healthBar.checkFoodBarRegen();
            music.eat_Play();
            return true;
        }

        else
        {
            return false;
        }


    }


    public void onHungerResistSkillUp(float hungerResist)
    {
        degenSpeed = hungerResist;
    }

    void FixedUpdate()
    {
        degenSpeed = playerStat.hungerDegen;
        hungerBar.value = currentHunger;
        hungerText.text = (int)currentHunger + " / " + maxFood;


        if (currentHunger >= 0)
        {
            // currentHunger -= 0.5f * Time.deltaTime;
            currentHunger -= degenSpeed * Time.deltaTime;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //currentHunger -= 2f * Time.deltaTime;
                currentHunger -= degenSpeed * 2 * Time.deltaTime;
            }

        }
    }

    public void recoverHungerBar()
    {
        currentHunger = 200f;
    }

    public void LoadData(GameData data)
    {
        currentHunger = data.currentFood;
    }

    public void SaveData(ref GameData data)
    {
        data.currentFood = currentHunger;
    }
}
