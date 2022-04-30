using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossStats : MonoBehaviour
{
    public float hitPts;
    public float maxHitPts=20;
    public EnemyHB healthBar;
    //leveling
    public int XP;

    public int portalEnteredText;

    public int enemyMin;

    private void Start()
    {
        hitPts = maxHitPts;
        healthBar.setHealth(hitPts, maxHitPts);

        XP = 10;
        if (PortalEnteredText.newPortal == true)
        {
            XP += 5;
        }
    }

    public void TakeDamage(float damage)
    {
        hitPts -= damage;
        healthBar.setHealth(hitPts, maxHitPts);
        if (hitPts <= 0)
        {
            Destroy(gameObject);
            GameObject.Find("Mage").GetComponent<PlayerStat>().currentExp += XP; //player exp

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

    /*public void LoadData(GameData data)
    {
        lvl = data.enemyLvl;
    }

    public void SaveData(ref GameData data)
    {
        data.enemyLvl = lvl;
    }*/
}
