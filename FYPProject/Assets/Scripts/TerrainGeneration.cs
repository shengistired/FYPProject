using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class TerrainGeneration : MonoBehaviour
{
    public BiomeClass ForestBiome;
    public BiomeClass DesertBiome;
    public BiomeClass SnowBiome;

    [Header("Tile Atlas")]
    public TileAtlas tileAtlas;

    // [Header("Biomes")]
    // public float biomeFrequency;
    // public Color grassland;
    // public Color desert;
    // public Color snow;
    // public Texture2D biomeMap;




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
    public string biome = "forest";
    public int worldSize = 0;
    public float heightMultiplier = 4f;
    public int heightAddition = 75;



    [Header("Noise settings")]
    public float caveFreq = 0.05f;
    public float terrainFreq = 0.1f;
    public float seed;
    public Texture2D caveNoiseTexture;

    [Header("Ore settings")]
    public OreClass[] ores;




    private GameObject[] worldChunks;

    //x,y pos of all tiles in the generated world.
    private List<Vector2> worldTiles = new List<Vector2>();


    // private void OnValidate()
    // {
    //     // if (worldSizeSet == "small")
    //     // {
    //     //     worldSize = 250;
    //     // }
    //     // if (worldSizeSet == "medium")
    //     // {
    //     //     worldSize = 500;
    //     // }
    //     // if (worldSizeSet == "large")
    //     // {
    //     //     worldSize = 1000;
    //     // } 

    //     //DrawTextures();
    //     //DrawBiomeTexture(grassland);

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

            DrawTextures();
            CreateChunks();
            GenerateTerrain();

        }
        else
        {
            DrawTextures();
            CreateChunks();
            GenerateTerrain();
        }

    }

    public void DrawTextures()
    {
        // biomeMap = new Texture2D(worldSize, worldSize);
        // biomeMap = DrawBiomeTexture(grassland, seed);
        // biomeMap = DrawBiomeTexture(desert, seed * 2);
        GenerateNoiseTexture(caveFreq, surfaceValue, caveNoiseTexture);

        //generate ores
        GenerateNoiseTexture(ores[0].rarity, ores[0].size, ores[0].spreadTexture);
        GenerateNoiseTexture(ores[1].rarity, ores[1].size, ores[1].spreadTexture);
        GenerateNoiseTexture(ores[2].rarity, ores[2].size, ores[2].spreadTexture);
        GenerateNoiseTexture(ores[3].rarity, ores[3].size, ores[3].spreadTexture);

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

    // public Texture2D DrawBiomeTexture(Color biomeColor, float seed)
    // {
    //     Texture2D tempTexture = new Texture2D(worldSize, worldSize);



    //     for (int x = 0; x < biomeMap.width; x++)
    //     {
    //         for (int y = 0; y < biomeMap.height; y++)
    //         {
    //             float v = Mathf.PerlinNoise((x + seed) * biomeFrequency, seed * biomeFrequency);
    //             if (v > 0.5f)
    //             {
    //                 tempTexture.SetPixel(x, y, biomeColor);
    //             }
    //             else
    //             {
    //                 tempTexture.SetPixel(x, y, Color.black);
    //             }
    //         }
    //     }

    //     tempTexture.Apply();
    //     return tempTexture;

    // }


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
                    if (ores[0].spreadTexture.GetPixel(x, y).r > 0.5f && height - y > ores[0].minSpawnHeight && height - y <= ores[0].maxSpawnHeight)
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
                    if (biome == "desert")
                    {
                        tileSprites = tileAtlas.sand.tileSprites;
                    }
                    else
                    {
                        tileSprites = tileAtlas.dirt.tileSprites;
                    }
                }
                else
                {
                    //top layer of map
                    if (biome != "desert")
                    {
                        tileSprites = tileAtlas.grass.tileSprites;
                    }
                    if (biome == "snow")
                    {
                        tileSprites = tileAtlas.snow.tileSprites;
                    }
                    if (biome == "forest")
                    {
                        tileSprites = tileAtlas.grass.tileSprites;
                    }

                    else
                    {
                        tileSprites = tileAtlas.sand.tileSprites;
                    }


                }
                //make cave spawn when below 5 height
                //if (generateCave && y < height - 5)
                if (generateCave && y < height)
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
        if (!worldTiles.Contains(new Vector2Int(x, y)))
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
    }

    void GenerateTree(int x, int y)
    {
        //generate tree log
        int treeHeight = UnityEngine.Random.Range(minTreeHeight, maxTreeHeight);
        if (biome != "desert")
        {
            for (int i = 0; i <= treeHeight; i++)
            {
                PlaceTiles(tileAtlas.treeLog.tileSprites, x, y + i);
            }

            if (biome == "snow")
            {
                PlaceTiles(tileAtlas.snowLeaf.tileSprites, x, y + treeHeight);
                PlaceTiles(tileAtlas.snowLeaf.tileSprites, x, y + treeHeight + 1);
                PlaceTiles(tileAtlas.snowLeaf.tileSprites, x, y + treeHeight + 2);

                PlaceTiles(tileAtlas.snowLeaf.tileSprites, x - 1, y + treeHeight);
                PlaceTiles(tileAtlas.snowLeaf.tileSprites, x - 1, y + treeHeight + 1);

                PlaceTiles(tileAtlas.snowLeaf.tileSprites, x + 1, y + treeHeight);
                PlaceTiles(tileAtlas.snowLeaf.tileSprites, x + 1, y + treeHeight + 1);

            }
            else
            {
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
        // if (biome == "desert")
        // {
        //     for (int i = 0; i <= treeHeight; i++)
        //     {
        //         PlaceTiles(tileAtlas.treeLog.tileSprites, x, y + i);
        //     }

        // }





    }

}
