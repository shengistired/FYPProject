using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossStats : MonoBehaviour, IDataPersistence
{
    public float hitPts;
    public float maxHitPts;
    public BossHB healthBar;
    //leveling
    public int XP;

    public static int lvl;
    public int portalEnteredText;

    public int enemyMin;
    public static int damageLvl;

    public string difficulty;

    private UnityEngine.Object explosionObject;


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
            damageLvl = lvl + 1;
            Debug.Log("damage easy");
        }

        if (difficulty == "normal")
        {
            damageLvl = lvl + 2;
            Debug.Log("damage normal");
        }

        if (difficulty == "hard")
        {
            damageLvl = lvl + 3;
            Debug.Log("damage hard");
        }

        maxHitPts = damageLvl * 30;
        hitPts = maxHitPts;
        healthBar.setHealth(hitPts, maxHitPts);

        XP = (lvl + 1) * 10;

        explosionObject = Resources.Load("Explosion");
    }

    public void TakeDamage(float damage)
    {
        bool bossDead = false;
        hitPts -= damage;
        healthBar.setHealth(hitPts, maxHitPts);
        if (hitPts <= 0)
        {
            int expNeeded;
            bossDead = true;
            if (bossDead == true)
            {
                GameObject.Find("DeathObject").GetComponent<Death_UI>().winGame();
            }
            
            Destroy(gameObject);
            expNeeded = GameObject.Find("Mage").GetComponent<PlayerStat>().expNeededToNextLevel;
            GameObject.Find("Mage").GetComponent<PlayerStat>().currentExp += expNeeded; //player exp

            GameObject explosion = (GameObject)Instantiate(explosionObject);
            explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);




        }
    }







    public void LoadData(GameData data)
    {
        difficulty = data.difficulty;
    }

    public void SaveData(ref GameData data)
    {
        data.difficulty = difficulty;
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
