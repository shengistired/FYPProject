using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FoodBar : MonoBehaviour
{
    public Slider foodBar;
    
    public static float food = 200f;
    float maxFood;

    void start()
    {
        food = maxFood;
    }

      public void UseFood(float amount)
    {
            if(food - amount >= 0)
            {
                food += amount;
                foodBar.value = food;
            }
    }

    void Update()
    {
        foodBar.value = food;

        if (food >= 0)
        {
            food -= 0.5f * Time.deltaTime;
        
            if (Input.GetKey(KeyCode.LeftShift))
            {
            food -= 2f * Time.deltaTime;
            }

            // if (Input.GetKey(KeyCode.Mouse0)){
            //     food +=10f;
            // }
        }
    }
}