using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemies : MonoBehaviour, IDataPersistence
{
    public GameObject collide;
    public GameObject shoot;
    //public Transform[] spawnPoint;

    private int rand;
    private int randPosition;

    //public float startTimeBtweenSpawn;
    //private float timeBtweenSpawn;

    public int enemyMin, enemyMax;
    public LayerMask groundLayer;
    public Transform player;

    public PortalEnteredText portalEnteredText;
    public string mode;

    public static bool spawning;
    public int addPts = 0;

    public void LoadData(GameData data)
    {
        mode = data.mode;
    }

    public void SaveData(ref GameData data)
    {
        data.mode = mode;
    }

    private void Start()
    {
        if (mode == "")
        {
            mode = NewGame.modeSelection;
        }

        player = GameObject.Find("Mage").transform;

        if (mode == "casual")
        {
            InvokeRepeating("SpawnEnemiesCasual", 0f, 13f);
            Debug.Log("Spawn casual");
        }

        if (mode == "timer")
        {
            InvokeRepeating("SpawnEnemiesTimer", 0f, 13f);
            Debug.Log("Spawn timers");
        }

        if (portalEnteredText.portalCount == 5 || portalEnteredText.portalCount == 10)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

        spawning = false;
    }

    private void SpawnEnemiesCasual()
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
                Vector3 spawnPosS = new Vector3(xs, height + 9, 0.0f);
                Vector3 spawnPosC = new Vector3(xc, height + 9, 0.0f);

                if ((spawnPosC - player.position).magnitude < 10 && (spawnPosS - player.position).magnitude < 10)
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

    private void SpawnEnemiesTimer()
    {
        if (enemyMin < enemyMax)
        {
            bool spawnAllowed = false;
            while (!spawnAllowed)
            {
                //Vector3 playerPos = player.transform.position;
                float height = GameObject.Find("Terrain").GetComponent<TerrainGeneration>().heightAddition;
                float xs = Random.Range(player.position.x - 9, player.position.x + 9);
                float xc = Random.Range(player.position.x - 8, player.position.x + 8);
                Vector3 spawnPosS = new Vector3(xs, height + 9, 0.0f);
                Vector3 spawnPosC = new Vector3(xc, height + 9, 0.0f);

                if ((spawnPosC - player.position).magnitude < 7 && (spawnPosS - player.position).magnitude < 7)
                {
                    continue;
                }
                else
                {
                    for (int i = 0; i < 1; i++)
                    {
                        GameObject golem = Instantiate(collide, spawnPosC, Quaternion.identity) as GameObject;
                        GameObject goblin = Instantiate(shoot, spawnPosS, Quaternion.identity) as GameObject;
                        enemyMin+=2;
                        spawnAllowed = true;
                        spawning = true;
                        addPts += 10;
                    }
                }
            }
        }
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
}