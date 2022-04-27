using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemies : MonoBehaviour
{
    public GameObject collide;
    public GameObject shoot;
    //GameObject collide;
    //public Transform[] spawnPoint;

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
        //lvl = Spawn_Shoot.lvl;

        Debug.Log("Enemy Level " + lvl);
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
        //Debug.Log("Enemy Level: " + lvl);
    }

    private void SpawnEnemies()
    {   
       if(enemyMin < enemyMax)
        {

            bool spawnAllowed = false;
            while (!spawnAllowed)
            {
                //Vector3 playerPos = player.transform.position;
                float height = GameObject.Find("Terrain").GetComponent<TerrainGeneration>().heightAddition;
                float xs = Random.Range(player.position.x - 15, player.position.x + 15);
                float xc = Random.Range(player.position.x - 14, player.position.x + 14);
                Vector3 spawnPosS = new Vector3(xs, height + 10, 0.0f);
                Vector3 spawnPosC = new Vector3(xc, height + 10, 0.0f);

                if ((spawnPosC - player.position).magnitude < 10)
                {
                    continue;
                }
                else
                {
                    for (int i = 0; i < 1; i++)
                    {
                        GameObject golem = Instantiate(collide, spawnPosC, Quaternion.identity) as GameObject;
                        GameObject goblin = Instantiate(shoot, spawnPosS, Quaternion.identity) as GameObject;
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