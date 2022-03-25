using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBars : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
            HealthBar.instance.UseHealth(1); 
        if (Input.GetKeyDown(KeyCode.Mouse1))
            ManaBar.instance.UseMana(10);     
        if (Input.GetKey(KeyCode.LeftShift))
            StaminaBar.instance.UseStamina(1);   
        // if (Input.GetKeyDown(KeyCode.T))
        //     FoodBar.instance.UseFood(1); 
    }
}
