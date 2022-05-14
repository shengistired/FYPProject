using System.Diagnostics.Tracing;
using UnityEngine;

public class PlayerBars : MonoBehaviour
{
    public Spawn_Enemies enemy;
    public float runningStamina = 0.5f;
    public audio_manager music;

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
            int damageC = EnemyStat.lvl + 1;
            HealthBar.instance.takeDamage(damageC * 10);
            music.hurt.Play();
            //Collide damage
        }

        if (collide.gameObject.tag.Equals("Bullet"))
        {
            // insert enemy damage >>>>>>
            int damageS = EnemyStat.lvl + 1;
            Debug.Log("Damage " + damageS);
            HealthBar.instance.takeDamage(damageS * 5);
            music.hurt.Play();

            //Collide damage
        }

        if (collide.gameObject.tag.Equals("BossBullet"))
        {
            // insert enemy damage >>>>>>
            int damageB = 40;
            Debug.Log("Damage " + damageB);
            HealthBar.instance.takeDamage(damageB);
            music.hurt.Play();

            //Collide damage
        }

        if (collide.gameObject.tag.Equals("MiniBoss"))
        {
            // insert enemy damage >>>>>>
            int damageM = 40;
            Debug.Log("Damage " + damageM);
            HealthBar.instance.takeDamage(damageM);
            music.hurt.Play();

            //Collide damage
        }
        // if (collide.gameObject.tag.Equals("Ground"))
        // {
        //     if (!TerrainGeneration.justSpawn)
        //     {
        //         if (PlayerController.freeFall != 0 && PlayerController.freeFallMagnitude > 6f)
        //         {
        //             Debug.Log(PlayerController.freeFall);
        //             HealthBar.instance.takeDamage(PlayerController.freeFall * -7);
        //             music.hurt.Play();
        //         }
        //     }

        //     PlayerController.freeFall = 0;
        //     PlayerController.freeFallMagnitude = 0;
        //     TerrainGeneration.justSpawn = false;


        //     //Collide damage
        // }
    }
}
