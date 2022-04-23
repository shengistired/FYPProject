using UnityEngine;

public class PlayerBars : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && PlayerController.running == true)
        {
            StaminaBar.instance.UseStamina(1);
            //Deplete stamina
        }

    }

    private void staminaUse()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collide)
    {
        if (collide.gameObject.tag.Equals("EnemyCollide"))
        {
            // insert enemy damage >>>>>>
            HealthBar.instance.takeDamage(20);
            //Collide damage
        }

        if (collide.gameObject.tag.Equals("Bullet"))
        {
            // insert enemy damage >>>>>>
            HealthBar.instance.takeDamage(10);
            //Collide damage
        }
    }
}
