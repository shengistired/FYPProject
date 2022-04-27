using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float hitPts;
    public float maxHitPts = 3;
    public EnemyHB healthBar;
    //leveling
    public float XP;

    private void Start()
    {
        hitPts = maxHitPts;
        healthBar.setHealth(hitPts, maxHitPts);
    }

    public void TakeDamage(float damage)
    {
        hitPts -= damage;
        healthBar.setHealth(hitPts, maxHitPts);
        if (hitPts <= 0)
        {
            Destroy(gameObject);
            GameObject.Find("Mage").GetComponent<PlayerStat>().currentExp += 1; //???
        }
    }
    /*void Die()
    {
        if(diePEffect != null)
        {
            Instantiate(diePEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
        GameObject.Find("Spawn_Shoot").GetComponent<Spawn_Shoot>().enemyMin -= 1;
    }*/
}
