using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour, IDataPersistence
{
    public float hitPts;
    public float maxHitPts;
    //public static float addPts;
    public EnemyHB healthBar;
    //leveling
    public int XP;

    public static int lvl;
    public int portalEnteredText, addPts;
    //public int addPts;

    public int enemyMin;
    public static int damageLvl;

    public string difficulty;

    private void Start()
    {
        try
        {
            portalEnteredText = GameObject.Find("NumberPortal").GetComponent<PortalEnteredText>().portalCount;
            addPts = GameObject.Find("Spawn_Enemies").GetComponent<Spawn_Enemies>().addPts;
            lvl = portalEnteredText;
        }
        catch{

        }


        if (difficulty == "")
        {
            difficulty = NewGame.difficultySelection;
        }

        if (difficulty == "easy")
        {
            damageLvl = lvl + 1;
          //  Debug.Log("damage easy");
        }

        if (difficulty == "normal")
        {
            damageLvl = lvl + 2;
          //  Debug.Log("damage normal");
        }

        if (difficulty == "hard")
        {
            damageLvl = lvl + 3;
          //  Debug.Log("damage hard");
        }

        maxHitPts = (damageLvl * 30);
        if (Spawn_Enemies.spawning == true)
        {
            maxHitPts += addPts;
        }
       // Debug.Log(maxHitPts + " MAXHITPTS");
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
}
