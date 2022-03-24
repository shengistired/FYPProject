
using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class TerrainGeneration : MonoBehaviour
{
    // public BiomeClass ForestBiome;
    // public BiomeClass DesertBiome;
    // public BiomeClass SnowBiome;
    public PlayerMovement player;
    public GameObject tileDrop;

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
    public string worldSizeSet;
    public string biome;
    public int worldSize;
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
    //object of every tile
    private List<GameObject> worldTileObjects = new List<GameObject>();
    //tile class list
    private List<TileClass> worldTileClasses = new List<TileClass>();


    private void OnValidate()
    {

        //     //DrawTextures();
        //     //DrawBiomeTexture(grassland); 
        //GenerateNoiseTexture(caveFreq, surfaceValue, caveNoiseTexture);
    }


    private void Start()
    {
        worldSizeSet = NewGame.worldsizeSelection;
        biome = NewGame.biomeSelection;

        if (biome == "random")
        {
            string[] array = { "forest", "desert", "snow" };
            biome = array[UnityEngine.Random.Range(0, 2)];
        }
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
        caveNoiseTexture = new Texture2D(worldSize, worldSize);
        //coal
        ores[0].spreadTexture = new Texture2D(worldSize, worldSize);
        //iron
        ores[1].spreadTexture = new Texture2D(worldSize, worldSize);
        //gold
        ores[2].spreadTexture = new Texture2D(worldSize, worldSize);
        //diamond
        ores[3].spreadTexture = new Texture2D(worldSize, worldSize);



        if (seed == 0)
        {
            seed = UnityEngine.Random.Range(-10000, 10000);

            DrawTextures();
            CreateChunks();
            GenerateTerrain();
            player.spawn();
            //cameraView.spawn(new Vector3(player.spawnPosition.x, player.spawnPosition.y, cameraView.transform.position.z));
            //cameraView.worldSize = worldSize;

        }
        else
        {
            DrawTextures();
            CreateChunks();
            GenerateTerrain();
            //cameraView.spawn(new Vector3(player.spawnPosition.x, player.spawnPosition.y, cameraView.transform.position.z));
            //cameraView.worldSize = worldSize;
            player.spawn();
        }

    }

    public void Update()
    {
        RefreshChunks();
    }

    void RefreshChunks()
    {
        for (int i = 0; i < worldChunks.Length; i++)
        {
            if (Vector2.Distance(new Vector2((i * chunkSize) + (chunkSize / 2), 0), new Vector2(player.transform.position.x, 0)) > Camera.main.orthographicSize * 4f)
            {
                worldChunks[i].SetActive(false);
            }
            else
            {
                worldChunks[i].SetActive(true);
            }

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

        for (int x = 0; x < worldSize; x++)
        {
            float height = Mathf.PerlinNoise((x + seed) * terrainFreq, seed * terrainFreq) * heightMultiplier + heightAddition;

            for (int y = 0; y < height; y++)
            {
                TileClass tileClass;

                if (y < height - dirtLayerHeight)
                {

                    if (x == worldSize / 2)
                    {
                        player.spawnPosition = new Vector2(x, height + 15);
                    }


                    //make stones first
                    tileClass = tileAtlas.stone;

                    //coal
                    if (ores[0].spreadTexture.GetPixel(x, y).r > 0.5f && height - y > ores[0].minSpawnHeight && height - y <= ores[0].maxSpawnHeight)
                    {
                        tileClass = tileAtlas.coal;
                    }
                    if (ores[1].spreadTexture.GetPixel(x, y).r > 0.5f && height - y > ores[1].minSpawnHeight && height - y <= ores[1].maxSpawnHeight)
                    {
                        tileClass = tileAtlas.iron;
                    }
                    if (ores[2].spreadTexture.GetPixel(x, y).r > 0.5f && height - y > ores[2].minSpawnHeight && height - y <= ores[2].maxSpawnHeight)
                    {
                        tileClass = tileAtlas.gold;
                    }
                    if (ores[3].spreadTexture.GetPixel(x, y).r > 0.5f && height - y > ores[3].minSpawnHeight && height - y <= ores[3].maxSpawnHeight)
                    {
                        tileClass = tileAtlas.diamond;
                    }



                }
                else if (y < height - 1)
                {
                    if (biome == "desert")
                    {
                        tileClass = tileAtlas.sand;
                    }
                    else
                    {
                        tileClass = tileAtlas.dirt;
                    }
                }
                else
                {
                    //top layer of map
                    if (biome == "snow")
                    {
                        tileClass = tileAtlas.snow;
                    }
                    else if (biome == "desert")
                    {
                        tileClass = tileAtlas.sand;
                    }

                    if (biome != "desert" && biome != "snow")
                    {
                        tileClass = tileAtlas.grass;
                    }
                    else
                    {
                        if (biome == "snow")
                        {
                            tileClass = tileAtlas.snow;
                        }
                        else if (biome == "desert")
                        {
                            tileClass = tileAtlas.sand;
                        }
                        else
                        {

                            tileClass = tileAtlas.grass;

                        }


                    }


                }
                //make cave spawn when below 5 height
                //if (generateCave && y < height - 5)
                //if (generateCave && y < height)
                if (generateCave)
                {
                    if (caveNoiseTexture.GetPixel(x, y).r > 0.5f)
                    {
                        PlaceTiles(tileClass, x, y);
                    }
                }
                else
                {
                    PlaceTiles(tileClass, x, y);
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

    public void TileCheck(TileClass tile, int x, int y, bool isSolid)
    {
        if (x >= 0 && x <= worldSize && y >= 0 && y <= worldSize)
        {
            //place blocks within world border
            if (!worldTiles.Contains(new Vector2Int(x, y)))
            {
                //place tile down
                PlaceTiles(tile, x, y);
            }
            else
            {
                //check if tile is background tile
                if (!worldTileClasses[worldTiles.IndexOf(new Vector2(x, y))].isSolidTile)
                {
                    //remove and replace current tile       
                    BreakTile(x, y);
                    PlaceTiles(tile, x, y);
                }

            }
        }

    }

    public void PlaceTiles(TileClass tile, int x, int y)
    {
        //if (!worldTiles.Contains(new Vector2Int(x, y)))

        bool isSolidTile = tile.isSolidTile;

        if (x >= 0 && x <= worldSize && y >= 0 && y <= worldSize)
        {

            GameObject newTile = new GameObject();

            //int chunkCoordinate = (Mathf.RoundToInt(Mathf.Round(x / chunkSize)) * chunkSize);
            //float chunkCoordinate = (Mathf.FloorToInt((x / chunkSize)) * chunkSize);
            int chunkCoordinate = Mathf.RoundToInt(Mathf.Round(x / chunkSize) * chunkSize);
            chunkCoordinate /= chunkSize;

            newTile.transform.parent = worldChunks[(int)chunkCoordinate].transform;

            newTile.AddComponent<SpriteRenderer>();

            // adds a collider2D if the tileClass is not a background tile.
            if (isSolidTile)
            {
                newTile.AddComponent<BoxCollider2D>();
                newTile.AddComponent<BoxCollider2D>().size = Vector2.one;

            }
            newTile.tag = "Ground";
            newTile.layer = 6;

            int spriteIndex = UnityEngine.Random.Range(0, tile.tileSprites.Length);
            newTile.GetComponent<SpriteRenderer>().sprite = tile.tileSprites[spriteIndex];
            if (tile.isSolidTile)
            {
                //background tiles
                newTile.GetComponent<SpriteRenderer>().sortingOrder = -10;
            }
            else
            {
                //normal world tiles
                newTile.GetComponent<SpriteRenderer>().sortingOrder = -5;
            }
            newTile.name = tile.tileSprites[0].name;
            newTile.transform.position = new Vector2(x + 0.5f, y + 0.5f);

            worldTiles.Add(newTile.transform.position - (Vector3.one * 0.5f));
            worldTileObjects.Add(newTile);
            worldTileClasses.Add(tile);
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
                PlaceTiles(tileAtlas.treeLog, x, y + i);
            }

            if (biome == "snow")
            {
                PlaceTiles(tileAtlas.snowLeaf, x, y + treeHeight);
                PlaceTiles(tileAtlas.snowLeaf, x, y + treeHeight + 1);
                PlaceTiles(tileAtlas.snowLeaf, x, y + treeHeight + 2);

                PlaceTiles(tileAtlas.snowLeaf, x - 1, y + treeHeight);
                PlaceTiles(tileAtlas.snowLeaf, x - 1, y + treeHeight + 1);

                PlaceTiles(tileAtlas.snowLeaf, x + 1, y + treeHeight);
                PlaceTiles(tileAtlas.snowLeaf, x + 1, y + treeHeight + 1);

            }
            else
            {
                //generate leaves
                PlaceTiles(tileAtlas.leaf, x, y + treeHeight);
                PlaceTiles(tileAtlas.leaf, x, y + treeHeight + 1);
                PlaceTiles(tileAtlas.leaf, x, y + treeHeight + 2);

                PlaceTiles(tileAtlas.leaf, x - 1, y + treeHeight);
                PlaceTiles(tileAtlas.leaf, x - 1, y + treeHeight + 1);

                PlaceTiles(tileAtlas.leaf, x + 1, y + treeHeight);
                PlaceTiles(tileAtlas.leaf, x + 1, y + treeHeight + 1);
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

    public void BreakTile(int x, int y)
    {
        if (worldTiles.Contains(new Vector2Int(x, y)) && x >= 0 && x <= worldSize && y >= 0 && y <= worldSize)
        {
            //breaks the tile of pos x y which is player's mouse pos
            Destroy(worldTileObjects[worldTiles.IndexOf(new Vector2(x, y))]);

            if (worldTileClasses[worldTiles.IndexOf(new Vector2(x, y))].tileDrop)
            {
                //drop a tile as an item
                GameObject newTileDrop = Instantiate(tileDrop, new Vector2(x, y + 0.5f), Quaternion.identity);
                newTileDrop.GetComponent<SpriteRenderer>().sprite = worldTileClasses[worldTiles.IndexOf(new Vector2(x, y))].tileSprites[0];

            }
            //remove the object from list
            worldTileObjects.RemoveAt(worldTiles.IndexOf(new Vector2(x, y)));
            worldTileClasses.RemoveAt(worldTiles.IndexOf(new Vector2(x, y)));
            worldTiles.RemoveAt(worldTiles.IndexOf(new Vector2(x, y)));

        }
    }

}
