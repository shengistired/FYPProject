using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class BossMapGenerator : MonoBehaviour
{
    // public BiomeClass ForestBiome;
    // public BiomeClass DesertBiome;
    // public BiomeClass SnowBiome;
    public PlayerMovement player;
    public GameObject tileDrop;
    public CameraFollow camera;
    public audio_manager music;

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





    [Header("World Generation settings")]
    public int chunkSize = 200;
    public bool generateCave = true;
    public float surfaceValue = 0.25f;
    public int dirtLayerHeight = 5;
    public string worldSizeSet;
    public string biome;
    public int worldSize;
    public float heightMultiplier = 4f;
    public int heightAddition = 75;



    [Header("Noise settings")]
    public float caveFreq = 0.01f;
    public float terrainFreq = 0.5f;
    public float seed = 1006;
    public Texture2D caveNoiseTexture;

    




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
        worldSizeSet = "small";
        biome = "MiniBoss";
        if (biome == "MiniBoss")
        {
            //music.forest_music_play();
        }


        if (worldSizeSet == "small")
        {
            worldSize = 200;
        }

        caveNoiseTexture = new Texture2D(worldSize, worldSize);





        if (seed == 1006)
        {
            // seed = UnityEngine.Random.Range(-10000, 10000);

            DrawTextures();
            CreateChunks();
            GenerateTerrain();
            //GeneratePortal(worldSize, 78);

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

    // public void Update()
    // {
    //     RefreshChunks();
    // }

    void RefreshChunks()
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

    public void DrawTextures()
    {
        
        GenerateNoiseTexture(caveFreq, surfaceValue, caveNoiseTexture);

    }

    public void CreateChunks()
    {
        if (worldSizeSet == "small")
        {
            worldSize = 200;
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




    public async void GenerateTerrain()
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
                        player.spawnPosition = new Vector2(60, height - 50);
                    }


                    //make stones first
                    tileClass = tileAtlas.unbreakableStone;

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
                if (generateCave)
                {
                    if (caveNoiseTexture.GetPixel(x, y).r > 0.5f)
                    {
                        PlaceTiles(tileClass, x, y, true);
                    }
                    else if (tileClass.backgroundVersion != null)
                    {
                        PlaceTiles(tileClass.backgroundVersion, x, y, true);
                    }
                }
                else
                {
                    PlaceTiles(tileClass, x, y, true);
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
            PlaceTiles(tileAtlas.portal, 1, height + 1, true);

        }
        else
        {
            PlaceTiles(tileAtlas.portal, worldSize - 15, height + 1, true);
        }

        if (portalSpawnLeftRight >= 50)
        {

            PlaceTiles(tileAtlas.portal, worldSize - 15, height + 1, true);

        }
        else
        {
            PlaceTiles(tileAtlas.portal, 1, height + 1, true);
        }

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

    public bool TileCheck(TileClass tile, int x, int y)
    {
        if (x >= 0 && x <= worldSize && y >= 0 && y <= worldSize)
        {
            //place blocks within world border
            if (!worldTiles.Contains(new Vector2Int(x, y)))
            {
                //place tile down
                PlaceTiles(tile, x, y, false);
                return true;
            }
            else
            {
                //check if tile is background tile and place tile
                if (!worldTileClasses[worldTiles.IndexOf(new Vector2(x, y))].isSolidTile)
                {
                    //remove and replace current tile       
                    BreakTile(x, y);
                    PlaceTiles(tile, x, y, false);
                    return true;
                }

            }
        }
        return false;

    }

    public void PlaceTiles(TileClass tile, int x, int y, bool isGenerated)
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

            if (tile.name.ToUpper().Contains("WALL"))
            {
                newTile.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 0.6f);
            }
            newTile.name = tile.tileSprites[0].name;
            newTile.transform.position = new Vector2(x + 0.5f, y + 0.5f);
            tile.generatedNaturally = isGenerated;
            worldTiles.Add(newTile.transform.position - (Vector3.one * 0.5f));
            worldTileObjects.Add(newTile);
            worldTileClasses.Add(tile);

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
                    PlaceTiles(tile.backgroundVersion, x, y, true);
                }

            }
            //breaks the tile of pos x y which is player's mouse pos
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
                        if (itemtype.ToString().ToUpper() == "TREELOGS")
                        {
                            item.itemType = Item.ItemType.treeWood;
                        }
                        else
                        {
                            item.itemType = itemtype;

                        }
                        item.amount = amount;
                        ItemWorld.SpawnItemWorld(new Vector2(x, y + 0.5f), item);

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



}


