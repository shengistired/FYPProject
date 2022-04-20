using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBars : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {  
        if (Input.GetKey(KeyCode.LeftShift) && PlayerMovement.running == true)
        {
            StaminaBar.instance.UseStamina(1);
            //Deplete stamina
        }

    }
    
       private void OnCollisionEnter2D (Collision2D collide)
    {
        if (collide.gameObject.tag.Equals ("Enemy")){
        HealthBar.instance.UseHealth(20); 
        //Collide damage
        }   
    }
}
