using System.Reflection;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class TerrainGeneration : MonoBehaviour, IDataPersistence
{
    // public BiomeClass ForestBiome;
    // public BiomeClass DesertBiome;
    // public BiomeClass SnowBiome;
    public PlayerController player;
    public GameObject tileDrop;
    public CameraFollow camera;
    public audio_manager music;
    public PortalEnteredText portalEnteredText;

    [Header("Tile Atlas")]
    public TileAtlas tileAtlas;

    // [Header("Biomes")]
    // public float biomeFrequency;
    // public Color grassland;
    // public Color desert;
    // public Color snow;
    // public Texture2D biomeMap;


    [Header("Portal settings")]
    public int portalsEntered = 0;



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
    public static string biome;
    public string mode;
    public int worldSize;
    public static string difficulty;
    public string playerClass;
    public int life;
    public float heightMultiplier = 4f;
    public int heightAddition = 75;



    [Header("Noise settings")]
    public float caveFreq = 0.05f;
    public float terrainFreq = 0.1f;
    public float seed;
    public Texture2D caveNoiseTexture;
    public bool isPlayerPlace = false;
    [Header("Ore settings")]
    public OreClass[] ores;


    private GameObject[] worldChunks;

    //x,y pos of all tiles in the generated world.
    private List<Vector2> worldTiles = new List<Vector2>();
    //object of every tile
    private List<GameObject> worldTileObjects = new List<GameObject>();
    //tile class list
    private List<TileClass> worldTileClasses = new List<TileClass>();
    private SerializeDictionary<string, Vector3> tilePosition = new SerializeDictionary<string, Vector3>();
    private SerializeDictionary<string, Vector3> treePosition = new SerializeDictionary<string, Vector3>();
    private SerializeDictionary<string, Vector3> tilePositionDuplicate = new SerializeDictionary<string, Vector3>();
    private SerializeDictionary<string, Vector3> treePositionDuplicate = new SerializeDictionary<string, Vector3>();
    private SerializeDictionary<string, int> tileSprite = new SerializeDictionary<string, int>();
    private SerializeDictionary<string, string> treeTypeDictionary = new SerializeDictionary<string, string>();
    private SerializeDictionary<string, string> treeTypeDictionaryDuplicate = new SerializeDictionary<string, string>();
    private SerializeDictionary<string, string> allTilesType = new SerializeDictionary<string, string>();
    private SerializeDictionary<string, string> allTilesTypeDuplicate = new SerializeDictionary<string, string>();
    private Vector3 playerStartPosition;
    private bool worldGenerated = false;
    private int treeHeightIncrement = 0;


    private void OnValidate()
    {

        //     //DrawTextures();
        //     //DrawBiomeTexture(grassland); 
        //GenerateNoiseTexture(caveFreq, surfaceValue, caveNoiseTexture);
    }


    private void Start()
    {
        if (difficulty == "")
        {
            life = NewGame.life;
        }
        if (worldSizeSet == "")
        {
            worldSizeSet = NewGame.worldsizeSelection;

        }
        if (biome == "")
        {
            biome = NewGame.biomeSelection;

        }
        if (difficulty == "")
        {
            difficulty = NewGame.difficultySelection;
        }
        if (playerClass == "")
        {
            playerClass = NewGame.playerClass;
        }

        if (portalEnteredText.portalCount == 5 || portalEnteredText.portalCount == 10)
        {

            //worldSizeSet = NewGame.worldsizeSelection;
            //biome = NewGame.biomeSelection;             

            music.miniBoss_music_play();

            // worldSize = 200;
            // chunkSize = 200;
            // caveFreq = 0.01f;
            // surfaceValue = 0.4f;

            //for boss arena 
            seed = 1006;
            caveFreq = 0.01f;
            surfaceValue = 0.4f;
            worldSize = 200;
            terrainFreq = 0.5f;
            heightMultiplier = 4f;
            heightAddition = 75;
            dirtLayerHeight = 5;
            worldSizeSet = "small";

            DrawTextures();
            CreateChunks();
            generateBossMap();
            camera.Spawn(new Vector3(player.spawnPosition.x, player.spawnPosition.y, camera.transform.position.z));
            camera.worldSize = worldSize;
            player.spawn();



        }


        // if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MiniBoss1"))
        // {
        //     biome = "MiniBoss";
        //     if (biome == "MiniBoss")
        //     {
        //         music.miniBoss_music_play();
        //     }
        //     worldSize = 200;
        //     chunkSize = 200;
        //     caveFreq = 0.01f;
        //     surfaceValue = 0.4f;

        //     DrawTextures();
        //     CreateChunks();
        //     generateBossMap();
        //     camera.Spawn(new Vector3(player.spawnPosition.x, player.spawnPosition.y, camera.transform.position.z));
        //     camera.worldSize = worldSize;
        //     player.spawn();

        // }


        else
        {

            if (biome == "forest")
            {
                music.forest_music_play();
            }

            if (biome == "desert")
            {
                music.desert_music_play();
            }
            if (biome == "snow")
            {
                music.snow_music_play();
            }


            if (biome == "random")
            {
                string[] array = { "forest", "desert", "snow" };
                biome = array[UnityEngine.Random.Range(0, 2)];

                if (biome == "forest")
                {
                    music.forest_music_play();
                }

                if (biome == "desert")
                {
                    music.desert_music_play();
                }
                if (biome == "snow")
                {
                    music.snow_music_play();
                }
            }
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
                if (!worldGenerated)
                {
                    GeneratePortal(worldSize, 78);

                }

                camera.Spawn(new Vector3(player.spawnPosition.x, player.spawnPosition.y, camera.transform.position.z));
                camera.worldSize = worldSize;
                player.spawn();
                //cameraView.spawn(new Vector3(player.spawnPosition.x, player.spawnPosition.y, cameraView.transform.position.z));
                //cameraView.worldSize = worldSize;

            }
            else
            {
                DrawTextures();
                CreateChunks();
                GenerateTerrain();
                camera.Spawn(new Vector3(player.spawnPosition.x, player.spawnPosition.y, camera.transform.position.z));
                camera.worldSize = worldSize;
                //cameraView.spawn(new Vector3(player.spawnPosition.x, player.spawnPosition.y, cameraView.transform.position.z));
                //cameraView.worldSize = worldSize;
                player.spawn();
            }

        }

        worldGenerated = true;

    }

    public void Update()
    {
        RefreshChunks();
    }

    void RefreshChunks()
    {

        if (portalEnteredText.portalCount == 5 || portalEnteredText.portalCount == 10)
        {
            worldChunks[0].SetActive(true);
        }

        else
        {
            for (int i = 0; i < worldChunks.Length; i++)
            {
                if (Vector2.Distance(new Vector2((i * chunkSize) + (chunkSize / 2), 0), new Vector2(player.transform.position.x, 0)) > Camera.main.orthographicSize * 5f)
                {
                    worldChunks[i].SetActive(false);
                }
                else
                {
                    worldChunks[i].SetActive(true);
                }

            }

        }


    }

    public void DrawTextures()
    {

        //only for boss map
        // if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MiniBoss1"))
        // {
        //     caveNoiseTexture = new Texture2D(worldSize, worldSize);
        // }

        //only for boss map
        if (portalEnteredText.portalCount == 5 || portalEnteredText.portalCount == 10)
        {
            caveNoiseTexture = new Texture2D(200, 200);
        }

        GenerateNoiseTexture(caveFreq, surfaceValue, caveNoiseTexture);
        //generate ores
        // if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("MiniBoss1"))
        // {
        //     GenerateNoiseTexture(ores[0].rarity, ores[0].size, ores[0].spreadTexture);
        //     GenerateNoiseTexture(ores[1].rarity, ores[1].size, ores[1].spreadTexture);
        //     GenerateNoiseTexture(ores[2].rarity, ores[2].size, ores[2].spreadTexture);
        //     GenerateNoiseTexture(ores[3].rarity, ores[3].size, ores[3].spreadTexture);

        // }

        if (portalEnteredText.portalCount == 5 || portalEnteredText.portalCount == 10)
        {

        }
        else
        {
            GenerateNoiseTexture(ores[0].rarity, ores[0].size, ores[0].spreadTexture);
            GenerateNoiseTexture(ores[1].rarity, ores[1].size, ores[1].spreadTexture);
            GenerateNoiseTexture(ores[2].rarity, ores[2].size, ores[2].spreadTexture);
            GenerateNoiseTexture(ores[3].rarity, ores[3].size, ores[3].spreadTexture);

        }







    }

    public void CreateChunks()
    {
        //only for boss map
        if (portalEnteredText.portalCount == 5 || portalEnteredText.portalCount == 10)
        {
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
        else
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



    }



    public async void GenerateTerrain()
    {
        if (worldGenerated)
        {
            foreach (KeyValuePair<string, Vector3> pair in tilePosition.ToList())
            {
                int x = (int)(pair.Value.x - 0.5f);
                int y = (int)(pair.Value.y - 0.5f);
                float height = Mathf.PerlinNoise((x + seed) * terrainFreq, seed * terrainFreq) * heightMultiplier + heightAddition;

                TileClass tileClass;
                Vector3 value;
                if (treePosition.TryGetValue(pair.Key, out value))
                {
                    int xTree = (int)(value.x - 0.5f);
                    int yTree = (int)(value.y - 0.5f);
                    GenerateTree(xTree, yTree, treeTypeDictionary[pair.Key]);

                }
                else
                {
                    if (allTilesType[pair.Key] == "Stone")
                    {
                        PlaceTiles(tileAtlas.stone, x, y, true, false, allTilesType[pair.Key]);
                    }
                    if (allTilesType[pair.Key] == "stone_wall")
                    {
                        PlaceTiles(tileAtlas.stone.backgroundVersion, x, y, true, false, allTilesType[pair.Key]);
                    }
                    if (allTilesType[pair.Key] == "iron")
                    {
                        PlaceTiles(tileAtlas.iron, x, y, true, false, allTilesType[pair.Key]);
                    }
                    if (allTilesType[pair.Key] == "coal")
                    {
                        PlaceTiles(tileAtlas.coal, x, y, true, false, allTilesType[pair.Key]);
                    }
                    if (allTilesType[pair.Key] == "gold")
                    {
                        PlaceTiles(tileAtlas.gold, x, y, true, false, allTilesType[pair.Key]);
                    }

                    if (allTilesType[pair.Key] == "Grass")
                    {
                        PlaceTiles(tileAtlas.grass, x, y, true, false, allTilesType[pair.Key]);
                    }
                    if (allTilesType[pair.Key] == "Dirt")
                    {
                        PlaceTiles(tileAtlas.dirt, x, y, true, false, allTilesType[pair.Key]);
                    }
                    if (allTilesType[pair.Key] == "dirt_wall")
                    {
                        PlaceTiles(tileAtlas.dirt.backgroundVersion, x, y, true, false, allTilesType[pair.Key]);
                    }
                    if (allTilesType[pair.Key] == "diamond")
                    {
                        PlaceTiles(tileAtlas.diamond, x, y, true, false, allTilesType[pair.Key]);
                    }
                    if (allTilesType[pair.Key] == "sand")
                    {
                        PlaceTiles(tileAtlas.sand, x, y, true, false, allTilesType[pair.Key]);
                    }
                    if (allTilesType[pair.Key] == "sand_wall")
                    {
                        PlaceTiles(tileAtlas.sand.backgroundVersion, x, y, true, false, allTilesType[pair.Key]);
                    }
                    if (allTilesType[pair.Key] == "snow")
                    {
                        PlaceTiles(tileAtlas.snow, x, y, true, false, allTilesType[pair.Key]);
                    }
                    if (allTilesType[pair.Key] == "treeWood")
                    {
                        PlaceTiles(tileAtlas.treeWood, x, y, true, false, allTilesType[pair.Key]);
                    }
                    if (allTilesType[pair.Key] == "portal")
                    {
                        PlaceTiles(tileAtlas.portal, x, y, true, false, allTilesType[pair.Key]);
                    }
                    /*
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
                    //top layer of map
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

                    if (generateCave && y < height - 5)
                    {
                        if (caveNoiseTexture.GetPixel(x, y).r > 0.5f)
                        {
                            PlaceTiles(tileClass, x, y, true, false, "");
                        }
                        else if (tileClass.backgroundVersion != null)
                        {
                            PlaceTiles(tileClass.backgroundVersion, x, y, true, false, "");
                        }
                    }
                    else
                    {
                        PlaceTiles(tileClass, x, y, true, false, "");
                    }
                    */

                }

            }

        }
        else
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
                            if(playerStartPosition == Vector3.zero)
                            {
                                player.spawnPosition = new Vector2(x, height + 15);
                                playerStartPosition = player.spawnPosition;
                            }            

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
                    //top layer of map
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
                    if (generateCave && y < height - 5)
                    {
                        if (caveNoiseTexture.GetPixel(x, y).r > 0.5f)
                        {
                            PlaceTiles(tileClass, x, y, true, false, tileClass.name);
                        }
                        else if (tileClass.backgroundVersion != null)
                        {
                            PlaceTiles(tileClass.backgroundVersion, x, y, true, false, tileClass.backgroundVersion.name);
                        }
                    }
                    else
                    {
                        PlaceTiles(tileClass, x, y, true, false, tileClass.name);
                    }

                    if (y >= height - 1 && x >= 5 && x <= worldSize - 20)
                    {
                        //the more treespawnrate the lesser trees will spawn
                        int tree = UnityEngine.Random.Range(0, treeSpawnRate);

                        if (tree == 1)
                        {
                            //spawn tree on top of grass
                            if (worldTiles.Contains(new Vector2(x, y)))
                            {
                                GenerateTree(x, y + 1, "");
                            }

                        }

                    }




                }

            }
        }


    }

    public void GeneratePortal(int worldSize, int height)
    {
        int portalSpawnLeftRight = UnityEngine.Random.Range(1, 100);
        //run once per map generation
        // for (int i = 0; i < 1; i++)
        //  {
        //if 0 spawn at x = 0.5 , if 1 spawn at y = end of map
        //int portalSpawnLeftRight = UnityEngine.Random.Range(0, 1);

        if (portalSpawnLeftRight <= 50)
        {
            PlaceTiles(tileAtlas.portal, 1, height + 1, true, false, tileAtlas.portal.name);

        }
        else
        {
            PlaceTiles(tileAtlas.portal, worldSize - 15, height + 1, true, false, tileAtlas.portal.name);
        }
        /*
        if (portalSpawnLeftRight >= 50)
        {

            PlaceTiles(tileAtlas.portal, worldSize - 15, height + 1, true);

        }
        else
        {
            PlaceTiles(tileAtlas.portal, 1, height + 1, true);
        }
        */

        //}


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

    public bool TileCheck(TileClass tile, int x, int y, bool generatedNaturally)
    {
        isPlayerPlace = true;
        if (x >= 0 && x <= worldSize && y >= 0 && y <= worldSize)
        {
            //place blocks within world border
            if (!worldTiles.Contains(new Vector2Int(x, y)))
            {
                //place tile down
                PlaceTiles(tile, x, y, false, false, tile.name);
                return true;
            }
            else
            {
                //check if tile is background tile and place tile
                if (!worldTileClasses[worldTiles.IndexOf(new Vector2(x, y))].isSolidTile)
                {
                    //remove and replace current tile       
                    BreakTile(x, y);
                    PlaceTiles(tile, x, y, generatedNaturally, false, tile.name);
                    return true;
                }

            }
        }
        return false;

    }
    private string GenerateGUID()
    {
        return System.Guid.NewGuid().ToString();
    }

    public void PlaceTiles(TileClass tile, int x, int y, bool isGenerated, bool isTree, string type)
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
            if (isSolidTile && tile.name != "portal")
            {
                newTile.AddComponent<BoxCollider2D>();
                newTile.AddComponent<BoxCollider2D>().size = Vector2.one;
            }

            if (tile.name == "portal")
            {
                BoxCollider2D portal = newTile.AddComponent<BoxCollider2D>();
                portal.isTrigger = true;
                portal.size = new Vector2(2f, 2f);
                newTile.AddComponent<EnterPortal>();
                newTile.GetComponent<EnterPortal>().portalEnteredText = portalEnteredText;
                newTile.GetComponent<EnterPortal>().music = music;
            }

            newTile.tag = "Ground";
            newTile.layer = 6;
            int spriteIndex = UnityEngine.Random.Range(0, tile.tileSprites.Length);
            newTile.GetComponent<SpriteRenderer>().sprite = tile.tileSprites[spriteIndex];
            if (tile.isSolidTile)
            {
                //normal world tiles
                newTile.GetComponent<SpriteRenderer>().sortingOrder = -5;

            }
            else
            {
                //background tiles minus surface tiles
                newTile.GetComponent<SpriteRenderer>().sortingOrder = -10;


            }

            // if (tile.name.ToUpper().Contains("WALL"))
            // {
            //     newTile.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 0.6f);
            // }
            newTile.name = GenerateGUID();
            // if (isPlayerPlace)
            // {
            //     newTile.transform.position = new Vector2(x, y);
            //     worldTiles.Add(newTile.transform.position - (Vector3.one * 0f));


            // }
            // else
            //{
            newTile.transform.position = new Vector2(x + 0.5f, y + 0.5f);
            worldTiles.Add(newTile.transform.position - (Vector3.one * 0.5f));
            tilePosition.Add(newTile.name, newTile.transform.position);
            tilePositionDuplicate.Add(newTile.name, newTile.transform.position);
            if (isTree)
            {
                //Debug.Log("Tree");
                treePosition.Add(newTile.name, newTile.transform.position);
                treePositionDuplicate.Add(newTile.name, newTile.transform.position);
                treeTypeDictionary.Add(newTile.name, type);
                treeTypeDictionaryDuplicate.Add(newTile.name, type);

            }
            else
            {
                allTilesType.Add(newTile.name, type);
                allTilesTypeDuplicate.Add(newTile.name, type);

            }
            //}

            //tile.generatedNaturally = isGenerated;

            TileClass newTileClass = TileClass.CreateInstance(tile, isGenerated);
            //TileClass newTileClass = new TileClass(tile, isGenerated);

            worldTileObjects.Add(newTile);
            worldTileClasses.Add(newTileClass);

        }
    }



    void GenerateTree(int x, int y, string treeType)
    {
        //generate tree log
        int treeHeight = UnityEngine.Random.Range(minTreeHeight, maxTreeHeight);

        if (worldGenerated)
        {
            //Debug.Log(treeType);
            if (treeType == "treeLogs")
            {
                PlaceTiles(tileAtlas.treeLog, x, y, true, true, treeType);
            }
            if (treeType == "leaf")
            {
                PlaceTiles(tileAtlas.leaf, x, y, true, true, treeType);
            }
            if (treeType == "cactus")
            {
                PlaceTiles(tileAtlas.cactus, x, y, true, true, treeType);
            }
            if (treeType == "snowLeaf")
            {
                PlaceTiles(tileAtlas.snowLeaf, x, y, true, true, treeType);
            }

        }
        else
        {
            if (biome != "desert")
            {

                for (int i = 0; i <= treeHeight; i++)
                {
                    PlaceTiles(tileAtlas.treeLog, x, y + i, true, true, tileAtlas.treeLog.name);
                }

                if (biome == "snow")
                {
                    PlaceTiles(tileAtlas.snowLeaf, x, y + treeHeight, true, true, tileAtlas.snowLeaf.name);
                    PlaceTiles(tileAtlas.snowLeaf, x, y + treeHeight + 1, true, true, tileAtlas.snowLeaf.name);
                    PlaceTiles(tileAtlas.snowLeaf, x, y + treeHeight + 2, true, true, tileAtlas.snowLeaf.name);

                    PlaceTiles(tileAtlas.snowLeaf, x - 1, y + treeHeight, true, true, tileAtlas.snowLeaf.name);
                    PlaceTiles(tileAtlas.snowLeaf, x - 1, y + treeHeight + 1, true, true, tileAtlas.snowLeaf.name);

                    PlaceTiles(tileAtlas.snowLeaf, x + 1, y + treeHeight, true, true, tileAtlas.snowLeaf.name);
                    PlaceTiles(tileAtlas.snowLeaf, x + 1, y + treeHeight + 1, true, true, tileAtlas.snowLeaf.name);

                }
                else
                {
                    //generate leaves
                    PlaceTiles(tileAtlas.leaf, x, y + treeHeight, true, true, tileAtlas.leaf.name);
                    PlaceTiles(tileAtlas.leaf, x, y + treeHeight + 1, true, true, tileAtlas.leaf.name);
                    PlaceTiles(tileAtlas.leaf, x, y + treeHeight + 2, true, true, tileAtlas.leaf.name);

                    PlaceTiles(tileAtlas.leaf, x - 1, y + treeHeight, true, true, tileAtlas.leaf.name);
                    PlaceTiles(tileAtlas.leaf, x - 1, y + treeHeight + 1, true, true, tileAtlas.leaf.name);

                    PlaceTiles(tileAtlas.leaf, x + 1, y + treeHeight, true, true, tileAtlas.leaf.name);
                    PlaceTiles(tileAtlas.leaf, x + 1, y + treeHeight + 1, true, true, tileAtlas.leaf.name);
                }

            }

            if (biome == "desert")
            {
                for (int i = 0; i <= 4; i++)
                {
                    PlaceTiles(tileAtlas.cactus, x, y + i, true, true, tileAtlas.cactus.name);
                }

            }
        }






    }

    public int checkTileHealth(int miningPower, int x, int y)
    {
        //only check for blocks that are generated
        if (worldTiles.Contains(new Vector2Int(x, y)) && x >= 0 && x <= worldSize && y >= 0 && y <= worldSize)
        {
            int tileHealth = worldTileClasses[worldTiles.IndexOf(new Vector2(x, y))].tileHealth;

            return tileHealth;

        }
        else
        {
            return 0;
        }



    }
    public bool checkTileIsTree(int x, int y)
    {
        if (worldTiles.Contains(new Vector2Int(x, y)) && x >= 0 && x <= worldSize && y >= 0 && y <= worldSize)
        {
            return worldTileClasses[worldTiles.IndexOf(new Vector2(x, y))].isTree;

        }
        else
        {
            return false;
        }

    }
    public bool checkTileIsGround(int x, int y)
    {
        if (worldTiles.Contains(new Vector2Int(x, y)) && x >= 0 && x <= worldSize && y >= 0 && y <= worldSize)
        {
            return worldTileClasses[worldTiles.IndexOf(new Vector2(x, y))].isGround;

        }
        else
        {
            return false;
        }

    }
    public bool checkTileIsBackground(int x, int y)
    {
        if (worldTiles.Contains(new Vector2Int(x, y)) && x >= 0 && x <= worldSize && y >= 0 && y <= worldSize)
        {
            return worldTileClasses[worldTiles.IndexOf(new Vector2(x, y))].isBackground;

        }
        else
        {
            return false;
        }

    }
    public bool checkTileIsDirt(int x, int y)
    {
        if (worldTiles.Contains(new Vector2Int(x, y)) && x >= 0 && x <= worldSize && y >= 0 && y <= worldSize)
        {
            return worldTileClasses[worldTiles.IndexOf(new Vector2(x, y))].isDirt;

        }
        else
        {
            return false;
        }

    }
    public bool checkTileBottom(int x, int y)
    {

        if (worldTiles.Contains(new Vector2Int(x, y)) && x >= 0 && x <= worldSize && y >= 0 && y <= worldSize)
        {
            if(y <= 1.5)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        else
        {
            return false;
        }

    }

    public void BreakTile(int x, int y)
    {
        TileClass tile = worldTileClasses[worldTiles.IndexOf(new Vector2(x, y))];

        //only check blocks that are generated in the world
        if (worldTiles.Contains(new Vector2Int(x, y)) && x >= 0 && x <= worldSize && y >= 0 && y <= worldSize)
        {
            //check if tile has a background version
            if (tile.backgroundVersion != null)
            {
                //replace broken tile with background version if tile is generated.
                if (tile.generatedNaturally)
                {
                    PlaceTiles(tile.backgroundVersion, x, y, true, false, tile.backgroundVersion.name);
                }

            }
            //breaks the tile of pos x y which is player's mouse pos

            tilePosition.Remove(worldTileObjects[worldTiles.IndexOf(new Vector2(x, y))].name);
            tilePositionDuplicate.Remove(worldTileObjects[worldTiles.IndexOf(new Vector2(x, y))].name);
            Destroy(worldTileObjects[worldTiles.IndexOf(new Vector2(x, y))]);


            if (tile.tileDrop)
            {
                //Debug.Log(worldTileClasses[worldTiles.IndexOf(new Vector2(x, y))]);
                //drop a tile as an item
                //GameObject newTileDrop = Instantiate(tileDrop, new Vector2(x, y + 0.5f), Quaternion.identity); 

                string tileName = worldTileClasses[worldTiles.IndexOf(new Vector2(x, y))].tileSprites[0].name.ToUpper();

                foreach (Item.ItemType itemtype in Item.ItemType.GetValues(typeof(Item.ItemType)))
                {

                    int amount = 0;
                    if (itemtype.ToString().ToUpper().Equals(tileName))
                    {
                        amount = 1;
                        Item item = new Item();
                        if (itemtype.ToString().ToUpper() == "LEAF" || itemtype.ToString().ToUpper() == "SNOWLEAF")
                        {
                            float randomNumber = Random.Range(0, 4);
                            if (randomNumber == 0)
                            {
                                item.itemType = Item.ItemType.Food;

                            }
                            else
                            {
                                amount = 0;
                            }
                        }
                        else
                        {

                            if (itemtype.ToString().ToUpper() == "TREELOGS")
                            {
                                item.itemType = Item.ItemType.treeWood;
                            }
                            else
                            {
                                item.itemType = itemtype;
                                if (itemtype.ToString().ToUpper() == "CACTUS")
                                {
                                    float randomNumber = Random.Range(0, 3);
                                    if (randomNumber == 0)
                                    {
                                        Item cactusFruit = new Item();
                                        cactusFruit.itemType = Item.ItemType.cactusFruit;
                                        cactusFruit.amount = amount;
                                        ItemWorld.SpawnItemWorld(new Vector2(x, y + 0.5f), cactusFruit);

                                    }

                                }

                            }
                        }

                        item.amount = amount;
                        if ((itemtype.ToString().ToUpper() != "LEAF" || itemtype.ToString().ToUpper() != "SNOWLEAF") && amount != 0)
                        {
                            ItemWorld.SpawnItemWorld(new Vector2(x, y + 0.5f), item);

                        }

                    }

                }

                //newTileDrop.GetComponent<SpriteRenderer>().sprite = worldTileClasses[worldTiles.IndexOf(new Vector2(x, y))].tileSprites[0];

            }
            //remove the object from list
            worldTileObjects.RemoveAt(worldTiles.IndexOf(new Vector2(x, y)));
            worldTileClasses.RemoveAt(worldTiles.IndexOf(new Vector2(x, y)));
            worldTiles.RemoveAt(worldTiles.IndexOf(new Vector2(x, y)));

        }
    }

    public async void generateBossMap()
    {
        seed = 1006;
        caveFreq = 0.02f;
        surfaceValue = 0.4f;
        worldSize = 200;
        terrainFreq = 0.5f;
        heightMultiplier = 4f;
        heightAddition = 75;
        dirtLayerHeight = 5;
        worldSizeSet = "small";

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
                        player.spawnPosition = new Vector2(65, height - 50);
                    }


                    //make stones first
                    tileClass = tileAtlas.unbreakableStone;


                }
                //top layer of map
                else if (y < height - 1)
                {

                    tileClass = tileAtlas.dirt;

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

                    if (biome != "desert" && biome != "snow")
                    {
                        tileClass = tileAtlas.grass;
                    }


                    else
                    {

                        tileClass = tileAtlas.grass;

                    }





                }

                if (generateCave)
                {
                    if (caveNoiseTexture.GetPixel(x, y).r > 0.5f)
                    {
                        PlaceTiles(tileClass, x, y, true, false, tileClass.name);
                    }
                    else if (tileClass.backgroundVersion != null)
                    {
                        PlaceTiles(tileClass.backgroundVersion, x, y, true, false, tileClass.backgroundVersion.name);
                    }
                }
                else
                {
                    PlaceTiles(tileClass, x, y, true, false, tileClass.name);
                }

            }

        }



    }
    public void LoadData(GameData data)
    {
        biome = data.biome;
        difficulty = data.difficulty;
        worldSizeSet = data.worldSizeSet;
        //life = data.life;
        //playerClass = data.playerClass;
        tilePosition = data.tilePosition;
        worldGenerated = data.worldGenerated;
        treePosition = data.treePosition;
        treeTypeDictionary = data.treeTypeDictionary;
        player.spawnPosition = data.playerPosition;
        
        allTilesType = data.allTilesTypeDictionary;
        if (data.worldRegenerated)
        {
            worldGenerated = false;
            tilePosition = new SerializeDictionary<string, Vector3>();
            treePosition = new SerializeDictionary<string, Vector3>();
            treeTypeDictionary = new SerializeDictionary<string, string>();
            allTilesType = new SerializeDictionary<string, string>();


        }
    }

    public void SaveData(ref GameData data)
    {
        //if (difficulty == "")
        //{
        //data.life = life;

        //}
        
        data.biome = biome;
        data.difficulty = difficulty;
        data.worldSizeSet = worldSizeSet;
        //data.life = life;
        data.playerClass = playerClass;
        data.tilePosition = tilePosition;
        data.playerStartPosition = playerStartPosition;
        if (worldGenerated)
        {
            data.tilePosition = tilePositionDuplicate;
            data.treePosition = treePositionDuplicate;
            data.treeTypeDictionary = treeTypeDictionaryDuplicate;
            data.allTilesTypeDictionary = allTilesTypeDuplicate;

        }
        else
        {
            data.tilePosition = tilePosition;
            data.treePosition = treePosition;
            data.treeTypeDictionary = treeTypeDictionary;
            data.allTilesTypeDictionary = allTilesType;

        }
        data.worldGenerated = worldGenerated;
    }





}


