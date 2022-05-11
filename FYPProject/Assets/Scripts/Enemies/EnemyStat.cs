using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour, IDataPersistence
{
    public float hitPts;
    public float maxHitPts;
    public EnemyHB healthBar;
    //leveling
    public int XP;

    public static int lvl;
    public int portalEnteredText;

    public int enemyMin;
    public static int damageLvl;

    public string difficulty;

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
 
        maxHitPts = damageLvl * 60;
        Debug.Log("Enemy Level " + damageLvl + " " + maxHitPts);
        hitPts = maxHitPts;
        healthBar.setHealth(hitPts, maxHitPts);

        XP = (lvl + 1) * 10;
    }

    public void TakeDamage(float damage)
    {
        hitPts -= damage;
        healthBar.setHealth(hitPts, maxHitPts);
        if (hitPts <= 0)
        {
            Item item = new Item();
            Destroy(gameObject);
            int randomNum = Random.Range(0, 4);
            Debug.Log(randomNum);
            if (randomNum == 0)
            {
                item.itemType = Item.ItemType.PickAxeMaterial;
                item.amount = 1;
                ItemWorld.SpawnItemWorld(new Vector2(transform.position.x, transform.position.y), item);
            }
            if (randomNum == 1)
            {
                item.itemType = Item.ItemType.HammerMaterial;
                item.amount = 1;
                ItemWorld.SpawnItemWorld(new Vector2(transform.position.x, transform.position.y), item);
            }
            if (randomNum == 2)
            {
                item.itemType = Item.ItemType.ShovelMaterial;
                item.amount = 1;
                ItemWorld.SpawnItemWorld(new Vector2(transform.position.x, transform.position.y), item);
            }
            if (randomNum == 3)
            {
                item.itemType = Item.ItemType.AxeMaterial;
                item.amount = 1;
                ItemWorld.SpawnItemWorld(new Vector2(transform.position.x, transform.position.y), item);

            }


            GameObject.Find("Spawn_Enemies").GetComponent<Spawn_Enemies>().enemyMin -= 1;
            GameObject.Find("Mage").GetComponent<PlayerStat>().currentExp += XP; //player exp

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
}
