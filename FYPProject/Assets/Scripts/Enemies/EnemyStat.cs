using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public float hitPts;
    public float maxHitPts = 3;
    public EnemyHB healthBar;
    //leveling
    public float XP;

    public static int lvl;
    public int portalEnteredText;

    private void Start()
    {
        Debug.Log("Enemy Level " + lvl);
        hitPts = maxHitPts;
        healthBar.setHealth(hitPts, maxHitPts);

        portalEnteredText = GameObject.Find("NumberPortal").GetComponent<PortalEnteredText>().portalCount;
        
        lvl = portalEnteredText + 1;
    }

    public void TakeDamage(float damage)
    {
        hitPts -= damage;
        healthBar.setHealth(hitPts, maxHitPts);
        if (hitPts <= 0)
        {
            Destroy(gameObject);
            GameObject.Find("Mage").GetComponent<PlayerStat>().currentExp += 100; //player exp
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
