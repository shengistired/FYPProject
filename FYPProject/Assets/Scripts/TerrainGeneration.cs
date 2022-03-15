using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class TerrainGeneration : MonoBehaviour
{
    [Header("Tile Sprites")]
    public Sprite grass;
    public Sprite dirt;
    public Sprite stone;
    public Sprite treeLog;
    public Sprite leaf;


    [Header("Tree settings")]
    public int treeSpawnRate = 15;
    public int minTreeHeight = 3;
    public int maxTreeHeight = 7;

    [Header("World Generation settings")]
    public int chunkSize = 10;
    public bool generateCave = true;
    public float surfaceValue = 0.25f;
    public int dirtLayerHeight = 5;
    public string worldSizeSet = "small";
    public int worldSize = 0;
    public float heightMultiplier = 4f;
    public int heightAddition = 25;



    [Header("Noise settings")]
    public float caveFreq = 0.05f;
    public float terrainFreq = 0.05f;
    public float seed;
    public Texture2D noiseTexture;

    public GameObject[] worldChunks;
    private List<Vector2> worldTiles = new List<Vector2>();



    private void Start()
    {
        if (seed == 0)
        {
            seed = UnityEngine.Random.Range(-10000, 10000);
            GenerateNoiseTexture();
            CreateChunks();
            GenerateTerrain();

        }
        else
        {
            GenerateNoiseTexture();
            CreateChunks();
            GenerateTerrain();
        }

    }

    public void CreateChunks()
    {
        if (worldSizeSet == "small")
        {
            worldSize = 250;
        }
        if (worldSizeSet == "medium")
        {
            worldSize = 500;
        }
        if (worldSizeSet == "large")
        {
            worldSize = 1000;
        }

        int numChunks = worldSize / chunkSize;
        worldChunks = new GameObject[numChunks];

        for (int i = 0; i < numChunks; i++)
        {
            GameObject newChunk = new GameObject();
            newChunk.name = i.ToString();
            newChunk.transform.parent = this.transform;
            worldChunks[i] = newChunk;
        }

    }

    public void GenerateTerrain()
    {

        // if (worldSizeSet == "small")
        // {
        //     worldSize = 250;
        // }
        // if (worldSizeSet == "medium")
        // {
        //     worldSize = 500;
        // }
        // if (worldSizeSet == "large")
        // {
        //     worldSize = 1000;
        // }


        for (int x = 0; x < worldSize; x++)
        {
            float height = Mathf.PerlinNoise((x + seed) * terrainFreq, seed * terrainFreq) * heightMultiplier + heightAddition;

            for (int y = 0; y < height; y++)
            {
                Sprite tileSprite;

                if (y < height - dirtLayerHeight)
                {
                    tileSprite = stone;
                }
                else if (y < height - 1)
                {
                    tileSprite = dirt;
                }
                else
                {
                    //top layer of map
                    tileSprite = grass;

                }
                if (generateCave)
                {
                    if (noiseTexture.GetPixel(x, y).r > surfaceValue)
                    {
                        PlaceTiles(tileSprite, x, y);
                    }
                }
                else
                {
                    PlaceTiles(tileSprite, x, y);
                }

                if (y >= height - 1)
                {
                    //the more treespawnrate the lesser trees will spawn
                    int tree = UnityEngine.Random.Range(0, treeSpawnRate);

                    if (tree == 1)
                    {
                        //spawn tree on top of grass
                        if (worldTiles.Contains(new Vector2(x, y)))
                        {
                            GenerateTree(x, y + 1);
                        }

                    }

                }




            }
        }

    }


    public void GenerateNoiseTexture()
    {
        noiseTexture = new Texture2D(worldSize, worldSize);

        for (int x = 0; x < noiseTexture.width; x++)
        {
            for (int y = 0; y < noiseTexture.height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * caveFreq, (y + seed) * caveFreq);
                noiseTexture.SetPixel(x, y, new Color(v, v, v));
            }
        }

        noiseTexture.Apply();
    }

    public void PlaceTiles(Sprite tileSprite, int x, int y)
    {

        GameObject newTile = new GameObject();

        //int chunkCoordinate = (Mathf.RoundToInt(Mathf.Round(x / chunkSize)) * chunkSize);
        float chunkCoordinate = (Mathf.FloorToInt((x / chunkSize)) * chunkSize);
        chunkCoordinate /= chunkSize;
        newTile.transform.parent = worldChunks[(int)chunkCoordinate].transform;

        newTile.AddComponent<SpriteRenderer>();
        newTile.GetComponent<SpriteRenderer>().sprite = tileSprite;
        newTile.name = tileSprite.name;
        newTile.transform.position = new Vector2(x + 0.5f, y + 0.5f);

        worldTiles.Add(newTile.transform.position - Vector3.one * 0.5f);
    }

    void GenerateTree(int x, int y)
    {


        //generate tree log
        int treeHeight = UnityEngine.Random.Range(minTreeHeight, maxTreeHeight);
        for (int i = 0; i <= treeHeight; i++)
        {
            PlaceTiles(treeLog, x, y + i);
        }

        //generate leaves
        PlaceTiles(leaf, x, y + treeHeight);
        PlaceTiles(leaf, x, y + treeHeight + 1);
        PlaceTiles(leaf, x, y + treeHeight + 2);

        PlaceTiles(leaf, x - 1, y + treeHeight);
        PlaceTiles(leaf, x - 1, y + treeHeight + 1);

        PlaceTiles(leaf, x + 1, y + treeHeight);
        PlaceTiles(leaf, x + 1, y + treeHeight + 1);

    }

}
