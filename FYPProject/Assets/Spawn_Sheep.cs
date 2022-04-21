using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Sheep : MonoBehaviour
{
    public GameObject animals;

    private int rand;
    private int randPosition;

    public int animalMin, animalMax;
    public LayerMask groundLayer;
    public Transform player;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find("Mage").transform;
        InvokeRepeating("SpawnAnimals", 0f, 8f);
    }

    private void SpawnAnimals()
    {
        if (animalMin < animalMax)
        {
            bool spawnAllowed = false;
            while (!spawnAllowed)
            {
                float height = GameObject.Find("Terrain").GetComponent<TerrainGeneration>().heightAddition;
                float x = Random.Range(player.position.x - 12, player.position.x + 12);
                Vector3 spawnPos = new Vector3(x, height + 10, 0.0f);

                if ((spawnPos - player.position).magnitude < 10)
                {
                    continue;
                }
                else
                {
                    for (int i = 0; i < 1; i++)
                    {
                        GameObject food = Instantiate(animals, spawnPos, Quaternion.identity) as GameObject;
                        ++animalMin;
                        spawnAllowed = true;
                    }

                }
            }
        }
    }
}