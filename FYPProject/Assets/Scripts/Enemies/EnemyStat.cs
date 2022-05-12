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
    public string mode;

    private void Start()
    {
        portalEnteredText = GameObject.Find("NumberPortal").GetComponent<PortalEnteredText>().portalCount;
        lvl = portalEnteredText;

        if (difficulty == "")
        {
            difficulty = NewGame.difficultySelection;
        }

        if (mode == "")
        {
            mode = NewGame.modeSelection;
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

            if (mode == "casual")
            {
                GameObject.Find("Spawn_Enemies").GetComponent<Spawn_Enemies>().enemyMin -= 1;
            }

            if (mode == "timer")
            {
                GameObject.Find("Spawn_Enemies").GetComponent<Spawn_Enemies>().count -= 1;
            }

            if (GameObject.Find("Spawn_Enemies").GetComponent<Spawn_Enemies>().count == 0)
            {
                Debug.Log("ALL ENEMIES KILLED");
            }

            GameObject.Find("Mage").GetComponent<PlayerStat>().currentExp += XP; //player exp
        }
    }

    public void LoadData(GameData data)
    {
        difficulty = data.difficulty;
        mode = data.mode;
    }

    public void SaveData(ref GameData data)
    {
        data.difficulty = difficulty;
        data.mode = mode;
    }
}
