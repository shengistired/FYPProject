using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Collide : MonoBehaviour
{
    public GameObject enemies;
    //GameObject collide;
    //public Transform[] spawnPoint;

    private int rand;
    private int randPosition;

    //[HideInInspector]
    //public bool spawnAllowed;

    //public float startTimeBtweenSpawn;
    //private float timeBtweenSpawn;

    public int enemyMin, enemyMax;
    public LayerMask groundLayer;
    public Transform player;

    public int lvl;

    // Start is called before the first frame update
    private void Start()
    {
        try
        {
            lvl = SaveData.current.lvlC;

        }
        catch
        {
            lvl = 1;
        }

        player = GameObject.Find("Mage").transform;
        InvokeRepeating("SpawnEnemies", 0f, 10f);

        
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
        if (EnterPortal.sceneLoaded == true)
        {
            lvl += 1;
            SaveData.current.lvlC = lvl;
            Debug.Log("lvl up enemy");
        }
    }

    private void SpawnEnemies()
    {
        /*if (spawnAllowed)
         {
             randPosition = Random.Range(0, spawnPoint.Length);
             rand = Random.Range(0, enemies.Length);
             Instantiate(enemies[rand], spawnPoint[randPosition].position, Quaternion.identity);
         }*/
        
       if(enemyMin < enemyMax)
        {

            bool spawnAllowed = false;
            while (!spawnAllowed)
            {
                //Vector3 playerPos = player.transform.position;
                float height = GameObject.Find("Terrain").GetComponent<TerrainGeneration>().heightAddition;
                float x = Random.Range(player.position.x - 15, player.position.x + 15);
                Vector3 spawnPos = new Vector3(x, height + 10, 0.0f);

                if ((spawnPos - player.position).magnitude < 10)
                //if (Vector3.Distance(spawnPos, player.position) < 15)
                {
                    continue;
                }
                else
                {
                    for (int i = 0; i < 1; i++)
                    {
                        GameObject col = Instantiate(enemies, spawnPos, Quaternion.identity) as GameObject;
                        ++enemyMin;
                        spawnAllowed = true;
                    }
                        
                }
            }
        }
    }
}