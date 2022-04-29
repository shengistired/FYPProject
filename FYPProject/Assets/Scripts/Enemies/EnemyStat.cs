using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public float hitPts;
    //public float maxHitPts = 4;
    public float maxHitPts;
    public EnemyHB healthBar;
    //leveling
    public int XP;

    public static int lvl;
    public int portalEnteredText;

    public int enemyMin;

    private void Start()
    {
        Debug.Log("Enemy Level " + lvl);
        int damageLvl = GameObject.Find("NumberPortal").GetComponent<PortalEnteredText>().portalCount + 1;
        maxHitPts = damageLvl * 2;
        hitPts = maxHitPts;
        healthBar.setHealth(hitPts, maxHitPts);

        portalEnteredText = GameObject.Find("NumberPortal").GetComponent<PortalEnteredText>().portalCount;
        lvl = portalEnteredText;

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
            GameObject.Find("Spawn_Enemies").GetComponent<Spawn_Enemies>().enemyMin -= 1;
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
