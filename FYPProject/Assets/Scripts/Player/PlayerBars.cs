using System.Diagnostics.Tracing;
using UnityEngine;

public class PlayerBars : MonoBehaviour
{
    public Spawn_Enemies enemy;
    public float runningStamina = 0.5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) && PlayerController.running == true)
        {
            staminaUse(runningStamina);
        }
    }

    private bool staminaUse(float stamina)
    {
        if (StaminaBar.instance.UseStamina(stamina) == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collide)
    {
        if (collide.gameObject.tag.Equals("EnemyCollide"))
        {
            // insert enemy damage >>>>>>
            int damageC = Spawn_Enemies.lvl + 1;
            HealthBar.instance.takeDamage(damageC * 10);
            //Collide damage
        }

        if (collide.gameObject.tag.Equals("Bullet"))
        {
            // insert enemy damage >>>>>>
            int damageS = Spawn_Enemies.lvl + 1;
            Debug.Log("Damage " + damageS);
            HealthBar.instance.takeDamage(damageS * 5);
            //Collide damage
        }
    }
}
