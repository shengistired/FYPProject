using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FoodBar : MonoBehaviour
{
    public Slider foodBar;
    
    public float food;
    float maxFood = 200f;

    void start()
    {
        food = maxFood;
    }

    void Update()
    {
        foodBar.value = food;

        if (food >= 0)
        {
            food -= 1f * Time.deltaTime;
        
            if (Input.GetKey(KeyCode.LeftShift))
            {
            food -= 2f * Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
            food -= 5f;
            }
        }
    }
}
