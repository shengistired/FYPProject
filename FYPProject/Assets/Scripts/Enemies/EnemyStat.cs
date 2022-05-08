using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour, IDataPersistence
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

    public string difficulty;

    public void LoadData(GameData data)
    {
        difficulty = data.difficulty;
    }

    public void SaveData(ref GameData data)
    {
        data.difficulty = difficulty;
    }

    private void Start()
    {
        portalEnteredText = GameObject.Find("NumberPortal").GetComponent<PortalEnteredText>().portalCount;
        lvl = portalEnteredText;

        if (difficulty == "")
        {
            difficulty = NewGame.difficultySelection;
        }

        if (difficulty == "easy")
        {
            int damageLvl = lvl + 1;
            maxHitPts = damageLvl * 60;
            Debug.Log("Enemy Level " + damageLvl + " " + maxHitPts);
        }

        if (difficulty == "normal")
        {
            int damageLvl = lvl + 2;
            maxHitPts = damageLvl * 60;
            Debug.Log("Enemy Level " + damageLvl + " " + maxHitPts);
        }

        if (difficulty == "hard")
        {
            int damageLvl = lvl + 3;
            maxHitPts = damageLvl * 60;
            Debug.Log("Enemy Level " + damageLvl + " " + maxHitPts);
        }

        hitPts = maxHitPts;
        healthBar.setHealth(hitPts, maxHitPts);

        XP = 10;
    }

    private void Update()
    {
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
