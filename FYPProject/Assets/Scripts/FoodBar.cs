using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FoodBar : MonoBehaviour
{
    public Slider foodBar;
    
    private int maxFood = 200;
    private int currentFood;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    public static FoodBar instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentFood = maxFood;
        foodBar.maxValue = maxFood;
        foodBar.value = maxFood;
    }

    public void UseFood(int amount)
    {
            if(currentFood - amount >= 0)
            {
                currentFood -= amount;
                foodBar.value = currentFood;

                if (regen != null)
                    StopCoroutine(regen);

                regen = StartCoroutine (RegenFood());
            }
            else 
            {
                Debug.Log("Not enough food!");
            }
    }
    
    private IEnumerator RegenFood()
    {
        yield return new WaitForSeconds(2);

        while(currentFood < maxFood)
        {
            currentFood += maxFood / 100;
            foodBar.value = currentFood;
            yield return regenTick;
        }
        regen = null;
    }
}
