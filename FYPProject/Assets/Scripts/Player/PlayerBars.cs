using UnityEngine;

public class PlayerBars : MonoBehaviour
{
    public Shoot shootEnemy;
    public Collide colEnemy;

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
            HealthBar.instance.takeDamage(colEnemy.lvl * 20);
            //Collide damage
        }

        if (collide.gameObject.tag.Equals("Bullet"))
        {
            // insert enemy damage >>>>>>
            HealthBar.instance.takeDamage(shootEnemy.lvl * 10);
            //Collide damage
        }
    }
}
