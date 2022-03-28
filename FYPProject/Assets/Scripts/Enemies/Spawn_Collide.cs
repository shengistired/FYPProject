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

    //public static bool spawnAllowed;

    //public float startTimeBtweenSpawn;
    //private float timeBtweenSpawn;

    public int enemyMin, enemyMax;

    public LayerMask groundLayer;
    //float radius;
    //public float height = 2f;
    //public TileAtlas tileAtlas;

    // Start is called before the first frame update
    private void Start()
    {
        //timeBtweenSpawn = startTimeBtweenSpawn;
        //SpawnEnemies();
        //spawnAllowed = true;

        /*if (GetComponent<Collider>() != null)
        {
            radius = GetComponent<Collider>().bounds.extents.y;
        }
        else
        {
            radius = 1f;
        }

        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up * 100, Vector3.down);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            if (hit.collider != null)
            {
                // this is where the gameobject is actually put on the ground
                transform.position = new Vector3(transform.position.x, hit.point.y + radius, transform.position.z);
                InvokeRepeating("SpawnEnemies", 0f, 8f);
                Debug.Log("ground found");
            }
        }*/
        //float heightPos = Random.Range (0-height, 0+height);
       
        
            InvokeRepeating("SpawnEnemies", 0f, 8f);
        
            
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


        /*TileClass tileClass;
        for (int i = 0; i < 3; i++)
        {
            if (tileClass = tileAtlas.grass)
            {
                rand = Random.Range(0, enemies.Length);
                float x = Random.Range(0.05f, 0.95f);
                float y = Random.Range(0.05f, 0.95f);
                Vector3 pos = new Vector3(x, y, 10.0f);
                pos = Camera.main.ViewportToWorldPoint(pos);
                //Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), (Screen.height / 2), Camera.main.farClipPlane));
                Instantiate(enemies[rand], pos, Quaternion.identity);
            }
        }*/
        
       if(enemyMin < enemyMax)
        {
            for(int i = 0; i < 2; i++)
            {
                //rand = Random.Range(0, enemies.Length);
                float x = Random.Range(0.05f, 0.95f);
                float y = Random.Range(0.05f, 0.95f);
                Vector3 pos = new Vector3(x, y, 10.0f);
                pos = Camera.main.ViewportToWorldPoint(pos);
                //Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), (Screen.height / 2), Camera.main.farClipPlane));
                //var enemies = Instantiate(Resources.Load("EnemyAI_Collide") as GameObject, pos, Quaternion.identity);
                GameObject col = Instantiate(enemies, pos, Quaternion.identity) as GameObject;
                ++enemyMin;
            }
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

    /*void OnTriggerEnter2D(Collider2D col)
    {
       
        switch (col.gameObject.name)
        {
            case "Fireball(Clone)":
                //EnemySpawn.spawnAllowed = false;
                //Instantiate(boom, col.gameObject.transform.position, Quaternion.identity);
                --enemyMin;
                Destroy(gameObject);
                
                Debug.Log("Killed collide spawn");

                break;
        }
        
        
    }*/
}