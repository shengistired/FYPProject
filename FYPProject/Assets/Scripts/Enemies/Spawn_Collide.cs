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

    

    // Start is called before the first frame update
    private void Start()
    {
        //timeBtweenSpawn = startTimeBtweenSpawn;
        //SpawnEnemies();
        
        player = GameObject.Find("Mage").transform;
        InvokeRepeating("SpawnEnemies", 0f, 7f); 
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
            /*//rand = Random.Range(0, enemies.Length);
            //float x = Random.Range(0.05f, 0.95f);
            //float y = Random.Range(0.05f, 0.95f);
            //Vector3 pos = new Vector3(x, y, 10.0f);
            //pos = Camera.main.ViewportToWorldPoint(pos);
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), (Screen.height / 2), Camera.main.farClipPlane));
            //var enemies = Instantiate(Resources.Load("EnemyAI_Collide") as GameObject, pos, Quaternion.identity);
            GameObject col = Instantiate(enemies, pos, Quaternion.identity) as GameObject;
            ++enemyMin;*/

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