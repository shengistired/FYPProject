using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemies;
    //public Transform[] spawnPoint;

    private int rand;
    private int randPosition;

    //public static bool spawnAllowed;

    //public float startTimeBtweenSpawn;
    //private float timeBtweenSpawn;

    public int enemyMin, enemyMax;

    //public Transform player;
    //float randPos;
    //Vector3 spawnPos;
    //private float disToPlayer;

    // Start is called before the first frame update
    private void Start()
    {
        //timeBtweenSpawn = startTimeBtweenSpawn;
        //SpawnEnemies();
        //spawnAllowed = true;
        //player = GameObject.Find("Mage").transform;
        InvokeRepeating("SpawnEnemies", 0f, 4.5f);
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
        /*if (spawnAllowed)
         {
             randPosition = Random.Range(0, spawnPoint.Length);
             rand = Random.Range(0, enemies.Length);
             Instantiate(enemies[rand], spawnPoint[randPosition].position, Quaternion.identity);
         }*/

        //disToPlayer = Vector2.Distance(transform.position, player.position);

        /*Vector3 playerPos = player.position;
        Vector3 playerDir = player.forward;
        Quaternion playerTurn = player.rotation;
        float spawnDist = 10;
        Vector3 spawnPos = playerPos + playerDir * spawnDist;
        //spawnPos = new Vector3(player.position.x + 10, player.position.y, player.position.z);

        if (enemyMin < enemyMax)
        {
            //randPosition = Random.Range(0, spawnPoint.Length);
            rand = Random.Range(0, enemies.Length);
            Instantiate(enemies[rand], spawnPos, playerTurn);
            //enemies[rand].GetComponent<Rigidbody2D>().velocity = mousePos - transform.position;
            ++enemyMin;
        } */

        if(enemyMin < enemyMax)
        {
            rand = Random.Range(0, enemies.Length);
            float x = Random.Range(0.10f, 0.95f);
            float y = Random.Range(0.10f, 0.95f);
            Vector3 pos = new Vector3(x, y, 15.0f);
            pos = Camera.main.ViewportToWorldPoint(pos);
            //Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), Camera.main.farClipPlane));
            Instantiate(enemies[rand], pos, Quaternion.identity);
            ++enemyMin;
        }

        /*if (enemyMin < enemyMax)
        {
            float spawnY = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
            rand = Random.Range(0, enemies.Length);
            Vector2 spawnPosition = new Vector2(spawnX, spawnY);
            Instantiate(enemies[rand], spawnPosition, Quaternion.identity);
            ++enemyMin;
        }*/
    }
}