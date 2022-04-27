using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Shoot : MonoBehaviour, IDataPersistence
{
    public GameObject enemies;
    //public Transform[] spawnPoint;
    //GameObject shoot;

    private int rand;
    private int randPosition;

    //public float startTimeBtweenSpawn;
    //private float timeBtweenSpawn;

    public int enemyMin, enemyMax;
    public LayerMask groundLayer;
    public Transform player;

    public static int lvl;

    // Start is called before the first frame update
    private void Start()
    {
        /*
        try
        {
            lvl = SaveData.current.lvlS;

        }
        catch
        {
            lvl = 1;
        }
        */
        
        Debug.Log("Enemy Level " + lvl);
        player = GameObject.Find("Mage").transform;
        InvokeRepeating("SpawnEnemies", 0f, 12f);

        
    }

    /* // Update is called once per frame
    private void Update()
    {
        if (timeBtweenSpawn <= 0)
        {
            rand = Random.Range(0, enemies.Length);
            randPosition = Random.Range(0, spawnPoint.Length);
            if (enemyMin < enemyMax)
            {
                Instantiate(enemies[rand], spawnPoint[rand].transform.position, Quaternion.identity);
                ++enemyMin;
            }
        }
        else
        {
            timeBtweenSpawn -= Time.deltaTime;
        }
    } */

    void Update()
    {
        /*
        if (EnterPortal.sceneLoaded == true)
        {
            lvl += 1;
           
            Debug.Log("lvl up enemy");
        }
        */
        Debug.Log("Enemy Level: " + lvl);
    }
    private void SpawnEnemies()
    {
        if (enemyMin < enemyMax)
        {

            bool spawnAllowed = false;
            while (!spawnAllowed)
            {
                //Vector3 playerPos = player.transform.position;
                float height = GameObject.Find("Terrain").GetComponent<TerrainGeneration>().heightAddition;
                float x = Random.Range(player.position.x - 15, player.position.x + 15);
                Vector3 spawnPos = new Vector3(x, height + 10, 0.0f);

                if((spawnPos - player.position).magnitude < 10)
                //if(Vector3.Distance(spawnPos, player.position) < 15)
                {
                    continue;
                }
                else
                {
                    for (int i = 0; i < 1; i++)
                    {
                        GameObject shoot = Instantiate(enemies, spawnPos, Quaternion.identity) as GameObject;
                        ++enemyMin;
                        spawnAllowed = true;
                    }
                        
                }
            }
        }
    }

    public void LoadData(GameData data)
    {
        lvl = data.enemyLvl;
    }

    public void SaveData(ref GameData data)
    {
       data.enemyLvl = lvl;
    }
}