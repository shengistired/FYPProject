using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform[] spawnPoint;

    private int rand;
    private int randPosition;

    public static bool spawnAllowed;

    //public float startTimeBtweenSpawn;
    //private float timeBtweenSpawn;

    //public int enemyMin, enemyMax;

    // Start is called before the first frame update
    private void Start()
    {
        //timeBtweenSpawn = startTimeBtweenSpawn;
        //SpawnEnemies();
        spawnAllowed = true;
        InvokeRepeating("SpawnEnemies", 0f, 1f);
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

    void SpawnEnemies()
    {
       if (spawnAllowed)
        {
            randPosition = Random.Range(0, spawnPoint.Length);
            rand = Random.Range(0, enemies.Length);
            Instantiate(enemies[rand], spawnPoint[randPosition].position, Quaternion.identity);
        }
        
        /*if (enemyMin < enemyMax)
        {
            Instantiate(enemies[rand], spawnPoint[randPosition].position, Quaternion.identity);
            //enemies[rand].GetComponent<Rigidbody2D>().velocity = mousePos - transform.position;
            ++enemyMin;
        }*/
    }
}