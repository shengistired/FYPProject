using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class TerrainGeneration : MonoBehaviour
{
    [Header("Tile Atlas")]
    public TileAtlas tileAtlas;


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
    public int heightAddition = 100;



    [Header("Noise settings")]
    public float caveFreq = 0.05f;
    public float terrainFreq = 0.1f;
    public float seed;
    public Texture2D caveNoiseTexture;

    [Header("Ore settings")]
    public OreClass[] ores;
    // public float coalRarity;
    // public float coalSize;
    // public float ironRarity, ironSize;
    // public float goldRarity, goldSize;
    // public float diamondRarity, diamondSize;
    // public Texture2D coalSpread;
    // public Texture2D ironSpread;
    // public Texture2D goldSpread;
    // public Texture2D diamondSpread;



    private GameObject[] worldChunks;
    private List<Vector2> worldTiles = new List<Vector2>();


    // private void OnValidate()
    // {
    //     if (worldSizeSet == "small")
    //     {
    //         worldSize = 250;
    //     }
    //     if (worldSizeSet == "medium")
    //     {
    //         worldSize = 500;
    //     }
    //     if (worldSizeSet == "large")
    //     {
    //         worldSize = 1000;
    //     }

    //     if (caveNoiseTexture == null)
    //     {
    //         caveNoiseTexture = new Texture2D(worldSize, worldSize);
    //         coalSpread = new Texture2D(worldSize, worldSize);
    //         ironSpread = new Texture2D(worldSize, worldSize);
    //         goldSpread = new Texture2D(worldSize, worldSize);
    //         diamondSpread = new Texture2D(worldSize, worldSize);
    //     }
    //     GenerateNoiseTexture(caveFreq, surfaceValue, caveNoiseTexture);

    //     //generate ores
    //     GenerateNoiseTexture(coalRarity, coalSize, coalSpread);
    //     GenerateNoiseTexture(ironRarity, ironSize, ironSpread);
    //     GenerateNoiseTexture(goldRarity, goldSize, goldSpread);
    //     GenerateNoiseTexture(diamondRarity, diamondSize, diamondSpread);

    // }


    private void Start()
    {
        // if (caveNoiseTexture == null)
        // {
        caveNoiseTexture = new Texture2D(worldSize, worldSize);
        ores[0].spreadTexture = new Texture2D(worldSize, worldSize);
        ores[1].spreadTexture = new Texture2D(worldSize, worldSize);
        ores[2].spreadTexture = new Texture2D(worldSize, worldSize);
        ores[3].spreadTexture = new Texture2D(worldSize, worldSize);
        // }


        if (seed == 0)
        {
            seed = UnityEngine.Random.Range(-10000, 10000);
            GenerateNoiseTexture(caveFreq, surfaceValue, caveNoiseTexture);

            //generate ores
            GenerateNoiseTexture(ores[0].rarity, ores[0].size, ores[0].spreadTexture);
            GenerateNoiseTexture(ores[1].rarity, ores[1].size, ores[1].spreadTexture);
            GenerateNoiseTexture(ores[2].rarity, ores[2].size, ores[2].spreadTexture);
            GenerateNoiseTexture(ores[3].rarity, ores[3].size, ores[3].spreadTexture);

            CreateChunks();
            GenerateTerrain();

        }
        else
        {
            GenerateNoiseTexture(caveFreq, surfaceValue, caveNoiseTexture);

            //generate ores
            GenerateNoiseTexture(ores[0].rarity, ores[0].size, ores[0].spreadTexture);
            GenerateNoiseTexture(ores[1].rarity, ores[1].size, ores[1].spreadTexture);
            GenerateNoiseTexture(ores[2].rarity, ores[2].size, ores[2].spreadTexture);
            GenerateNoiseTexture(ores[3].rarity, ores[3].size, ores[3].spreadTexture);

            CreateChunks();
            GenerateTerrain();
        }

    }

    public void CreateChunks()
    {
        if (worldSizeSet == "small")
        {
            worldSize = 260;
        }
        if (worldSizeSet == "medium")
        {
            worldSize = 480;
        }
        if (worldSizeSet == "large")
        {
            worldSize = 760;
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
                Sprite[] tileSprites;

                if (y < height - dirtLayerHeight)
                {
                    //make stones first
                    tileSprites = tileAtlas.stone.tileSprites;

                    //coal
                    if (ores[0].spreadTexture.GetPixel(x, y).r > 0.5f && height - y > ores[0].minSpawnHeight && height - y <= ores[0].maxSpawnHeight )
                    {
                        tileSprites = tileAtlas.coal.tileSprites;
                    }
                    if (ores[1].spreadTexture.GetPixel(x, y).r > 0.5f && height - y > ores[1].minSpawnHeight && height - y <= ores[1].maxSpawnHeight)
                    {
                        tileSprites = tileAtlas.iron.tileSprites;
                    }
                    if (ores[2].spreadTexture.GetPixel(x, y).r > 0.5f && height - y > ores[2].minSpawnHeight && height - y <= ores[2].maxSpawnHeight)
                    {
                        tileSprites = tileAtlas.gold.tileSprites;
                    }
                    if (ores[3].spreadTexture.GetPixel(x, y).r > 0.5f && height - y > ores[3].minSpawnHeight && height - y <= ores[3].maxSpawnHeight)
                    {
                        tileSprites = tileAtlas.diamond.tileSprites;
                    }



                }
                else if (y < height - 1)
                {
                    tileSprites = tileAtlas.dirt.tileSprites;
                }
                else
                {
                    //top layer of map
                    tileSprites = tileAtlas.grass.tileSprites;

                }
                if (generateCave)
                {
                    if (caveNoiseTexture.GetPixel(x, y).r > 0.5f)
                    {
                        PlaceTiles(tileSprites, x, y);
                    }
                }
                else
                {
                    PlaceTiles(tileSprites, x, y);
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


    public void GenerateNoiseTexture(float frequency, float limit, Texture2D noiseTexture)
    {

        for (int x = 0; x < noiseTexture.width; x++)
        {
            for (int y = 0; y < noiseTexture.height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * frequency, (y + seed) * frequency);
                if (v > limit)
                {
                    noiseTexture.SetPixel(x, y, Color.white);
                }
                else
                {
                    noiseTexture.SetPixel(x, y, Color.black);
                }
            }
        }

        noiseTexture.Apply();
    }

    public void PlaceTiles(Sprite[] tileSprites, int x, int y)
    {

        GameObject newTile = new GameObject();

        //int chunkCoordinate = (Mathf.RoundToInt(Mathf.Round(x / chunkSize)) * chunkSize);
        float chunkCoordinate = (Mathf.FloorToInt((x / chunkSize)) * chunkSize);
        chunkCoordinate /= chunkSize;
        newTile.transform.parent = worldChunks[(int)chunkCoordinate].transform;

        newTile.AddComponent<SpriteRenderer>();
        newTile.AddComponent<BoxCollider2D>();
        newTile.AddComponent<BoxCollider2D>().size = Vector2.one;
        
        //uncomment the one below after player walking is done.
        //newTile.tag = "Ground";

        int spriteIndex = UnityEngine.Random.Range(0, tileSprites.Length);
        newTile.GetComponent<SpriteRenderer>().sprite = tileSprites[spriteIndex];
        newTile.name = tileSprites[0].name;
        newTile.transform.position = new Vector2(x + 0.5f, y + 0.5f);

        worldTiles.Add(newTile.transform.position - Vector3.one * 0.5f);
    }

    void GenerateTree(int x, int y)
    {
        //generate tree log
        int treeHeight = UnityEngine.Random.Range(minTreeHeight, maxTreeHeight);
        for (int i = 0; i <= treeHeight; i++)
        {
            PlaceTiles(tileAtlas.treeLog.tileSprites, x, y + i);
        }

        //generate leaves
        PlaceTiles(tileAtlas.leaf.tileSprites, x, y + treeHeight);
        PlaceTiles(tileAtlas.leaf.tileSprites, x, y + treeHeight + 1);
        PlaceTiles(tileAtlas.leaf.tileSprites, x, y + treeHeight + 2);

        PlaceTiles(tileAtlas.leaf.tileSprites, x - 1, y + treeHeight);
        PlaceTiles(tileAtlas.leaf.tileSprites, x - 1, y + treeHeight + 1);

        PlaceTiles(tileAtlas.leaf.tileSprites, x + 1, y + treeHeight);
        PlaceTiles(tileAtlas.leaf.tileSprites, x + 1, y + treeHeight + 1);

    }

}
