using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IDataPersistence
{
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private GameObject staff;
    [SerializeField] private Image[] background;
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private UI_Craft uiCraft;
    //  [SerializeField] private UI_EquipmentSlot[] uiEquipmentSlot;
    [SerializeField] private UI_EquipmentSlot uiEquipmentSlot;
    [SerializeField] private UI_Equipment uiEquip;
    
    //player stat info


    [SerializeField] private PlayerAttack attack;
    [SerializeField] private StaminaBar stamina;

    [Header("Player stats and HP")]
    [SerializeField] private PlayerStat playerStat;
    [SerializeField] private PlayerBars playerBars;
    [SerializeField] private Stats_UI stats_UI;



    [Header("set your own title")]
    [SerializeField] private GameObject axe;
    [SerializeField] private GameObject pickAxe;

    [Header("settings")]
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject others;
    [SerializeField] private CustomCursor customCursor;
    [SerializeField] private Image craftButton;
    [SerializeField] private TileAtlas tileAtlas;
    [SerializeField] private GameObject content;
    [SerializeField] private Settings settings;
    public audio_manager music;
    public HungerBar hungerBar;

    private KeyCode[] keys =
{
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
        KeyCode.Alpha0,
    };



    Color backgroundColor = new Color32(0, 0, 0, 255);
    Color selectedColor = new Color32(60, 60, 60, 255);
    Color craftColor = new Color32(255, 255, 255, 255);
    Color selectedCraftColor = new Color32(200, 200, 200, 255);


    public UnityEngine.Vector2Int mousePosition;
    public UnityEngine.Vector2 spawnPosition;

    public bool placeTiles;
    public static bool mine = false;
    private bool staffActive = false;
    private bool axeActive = false;
    private bool othersActive = false;
    private bool axeJump = false;
    private bool miningCounter = false;
    public bool moved;
    public static bool openCraft = false;
    public static bool running;
    public static bool normalAttack = false;


    public TileClass[] tile;
    public TileClass selectedTile;

    public int playerPlaceRange;
    public int miningPower;
    public static int directionNum;
    int lockMousePointerX;
    int lockMousePointerY;
    public int rotationOffset = 0;
    public int direct;
    private int index;


    private Item.ItemType itemType;
    private float timeRemaining;
    private float cooldownTimer = Mathf.Infinity;
    private float attackCoolDown = 1;
    private float animationTime = 0;
    private float wallJumpCoolDown;
    private float horizontalInput;
    public float runSpeed;
    private float horizontalMove = 0f;
    private float normalAttackCooldown = 0f;

    private Rigidbody2D body;
    private Animator ani;
    private EdgeCollider2D edgeCollider;
    private Inventory inventory;
    public static Equipment equipment;
    private Item item;
    private Item original;
    public TerrainGeneration terrainGenerator;
    //public BossMapGenerator bossMapGenerator;
    private int newItemIndex;
    Vector3 difference;
    Vector3 equipmentPosition;
    private Rigidbody2D rigid;
    //leveling
    public Level level;


    private TileClass tileWood;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        //SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Save.save");
        //DontDestroyOnLoad(transform.gameObject);
        tileWood = tileAtlas.treeWood;


        directionNum = 1;
        body = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        edgeCollider = GetComponent<EdgeCollider2D>();

        /*
        if (SaveData.current.inventory == null)
        {
            SaveData.current.inventory = new Inventory(UseItem);

        }


        if (SaveData.current.equipment == null)
        {
            SaveData.current.equipment = new Equipment(UseItem);

        }
        if (SaveData.current.craftItem == null)
        {
            SaveData.current.craftItem = new CraftItem();

        }
        inventory = SaveData.current.inventory;
        equipment = SaveData.current.equipment;
        craftItem = SaveData.current.craftItem;
        */
        inventory = new Inventory(UseItem);
        equipment = new Equipment(UseItem);
        direct = 1;
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);
        uiEquipmentSlot.SetEquipment(equipment);

        foreach (CraftingRecipeUI craftingRecipeUI in content.GetComponentsInChildren<CraftingRecipeUI>())
        {
            craftingRecipeUI.setPlayer(this);
        }

    }


    public void spawn()
    {
        GetComponent<Transform>().position = spawnPosition;
    }

    public UnityEngine.Vector3 getPosition()
    {
        return transform.position;
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Potion:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Potion, amount = 1 });
                break;

            case Item.ItemType.Food:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Food, amount = 1 });
                break;

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();

        if (itemWorld != null)
        {

            if (!equipment.isFilled())
            {
                if (!equipment.AddItemCollide(itemWorld.GetItem(), equipment.filledArray()))
                {
                    equipment.AddItem(itemWorld.GetItem(), equipment.filledArray());
                }
            }
            else if (!equipment.AddItemCollide(itemWorld.GetItem(), equipment.filledArray()))
            {
                inventory.AddItem(itemWorld.GetItem());
            }
            else
            {

                equipment.AddItemCollide(itemWorld.GetItem(), 0);

            }
            itemWorld.DestroySelf();
        }
    }
    public Item getEquipment(int index)
    {
        return equipment.GetEquipment(index);
    }
    public Item getInventoryItem(int index)
    {
        return inventory.GetItem(index);
    }
    public Inventory getInventory()
    {
        return inventory;
    }
    public void AddEquipment(Item item, int index)
    {
        equipment.AddItem(item, index);

        if (Equipment.addInventory)
        {
            inventory.AddItem(equipment.previousItem());
        }
    }


    public void AddItemInventory(Item item, int index)
    {
        equipment.DeleteEquipment(index);
        inventory.AddItem(item);
    }
    public void AddInventory(Item item)
    {
        inventory.AddItem(item);
    }

    public void MoveEquipment(Item item, int oldindex, int newindex)
    {
        equipment.MoveItem(item, oldindex, newindex);

        newItemIndex = newindex;

    }
    private void Update()
    {

        //makes player not able to walk out of camera
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minScreenBounds.x + 1, maxScreenBounds.x - 1), Mathf.Clamp(transform.position.y, minScreenBounds.y + 1, maxScreenBounds.y - 3), transform.position.z);

        mousePosition.x = Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
        mousePosition.y = Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        horizontalInput = Input.GetAxis("Horizontal");
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        ani.SetFloat("Speed", Mathf.Abs(horizontalMove));

        uiInventory.inventory_Position();
        uiEquip.equipment_Position();
        uiCraft.craft_Position();

        //press F to open stat screen
        playerStat.openStatScreen();
        playerStat.openSkillScreen();


        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10f));
        float distance = position.x - transform.position.x;
        float distanceY = position.y - transform.position.y;
        if (horizontalInput > 0)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                music.walk_Play();
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                music.walk_stop();
            }
            //transform.localScale = Vector3.one;
            transform.localScale = new Vector3(1, 1, 1);
            direct = 1;
            directionNum = 1;


        }
        else if (horizontalInput < 0)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                music.walk_Play();
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                music.walk_stop();
            }
            transform.localScale = new Vector3(-1, 1, 1);
            direct = -1;
            directionNum = -1;
        }
        if (stamina.currentStamina > playerBars.runningStamina)
        {

            if (Input.GetKeyDown(KeyCode.LeftShift) && horizontalMove != 0)
            {

                running = true;
                runSpeed = 75f;

            }
            if (Input.GetKeyUp(KeyCode.LeftShift) || horizontalMove == 0 || running == false)
            {
                running = false;
                runSpeed = 40f;
            }
        }
        else
        {
            runSpeed = 40f;
        }
        if (staffActive == true)
        {
            staff.SetActive(true);
        }
        else
        {
            staff.SetActive(false);
        }

        if (axeActive == true)
        {
            axe.SetActive(true);
        }
        else
        {
            axe.SetActive(false);
        }
        if (othersActive == true)
        {
            others.SetActive(true);
        }
        else
        {
            others.SetActive(false);
        }
        if (placeTiles == true)
        {
            mine = false;
        }

        for (int i = 0; i < background.Length; i++)
        {
            if (equipment.GetEquipment(i) == null)
            {

                background[i].color = backgroundColor;

            }

            else if (ReturnDragDrop.move == true)
            {
                background[i].color = backgroundColor;
                staffActive = false;
                othersActive = false;
                axeActive = false;
                axeJump = false;
                mine = false;

            }


        }

        ReturnDragDrop.move = false;
        if (original != null && (equipment.GetEquipment(index) == null || equipment.GetEquipment(index) != original))
        {
            if (original.itemType == Item.ItemType.Weapon)
            {
                staffActive = false;

            }
            else if (original.isAxe())
            {
                axeActive = false;
                axeJump = false;
                mine = false;

            }
            else
            {
                othersActive = false;
            }
            keySelected(index);

            if (equipment.GetEquipment(index) != null)
            {
                background[index].color = selectedColor;

            }
        }

        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i]))
            {
                keySelected(i);
                if (item != null)
                {
                    background[i].color = selectedColor;
                }
            }
        }
        equipmentPosition = uiEquipmentSlot.GetComponent<RectTransform>().position;
        difference = position - equipmentPosition;

        if (Input.GetMouseButtonDown(0) && item != null)
        {

            if ((itemType == Item.ItemType.Food || itemType == Item.ItemType.Meat || itemType == Item.ItemType.cactusFruit) && item.amount > 0 && (difference.y > 1.5 || difference.x < 0 || difference.x > 15))
            {
                if (hungerBar.UseFood(20f) == true)
                {
                    equipment.RemoveItem(item, index);
                }

                if (item.amount == 0)
                {
                    background[index].color = backgroundColor;
                    item = null;
                    othersActive = false;
                }
            }


        }
        if (Input.GetMouseButtonDown(0) && staffActive == true)
        {
            normalAttack = true;
            ani.SetTrigger("isAttack");

        }
        if (normalAttack)
        {
            normalAttackCooldown += Time.deltaTime;
        }
        if (normalAttackCooldown >= 0.1f)
        {
            normalAttack = false;
            normalAttackCooldown = 0f;
        }

        //shoot fireball
        if (Input.GetMouseButtonDown(1) && cooldownTimer > attackCoolDown && staffActive == true)
        {
            // Reduce mana each usage of staff
            if (ManaBar.instance.useMana(10) == true)
            {
                attack.attack();
                cooldownTimer = 0;
                animationTime = 0;

                ani.SetTrigger("isAttack");
                //play fireball sound effect
                music.fireBall_play();

            }


        }
        if (Vector2.Distance(transform.position, mousePosition) <= playerPlaceRange && Vector2.Distance(transform.position, mousePosition) > 0.5f)
        {
            if (Input.GetMouseButtonDown(0) && placeTiles == true && item != null && (difference.y > 1.5 || difference.x < 0 || difference.x > 15))
            {
                bool checker = terrainGenerator.TileCheck(selectedTile, mousePosition.x, mousePosition.y, false);
                if (checker == true)
                {
                    equipment.RemoveItem(item, index);
                }

                if (item.amount == 0)
                {
                    customCursor.gameObject.SetActive(false);
                    Cursor.visible = true;
                    background[index].color = backgroundColor;
                    item = null;
                    othersActive = false;

                }



                // if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Map"))
                // {
                //     bool checker = terrainGenerator.TileCheck(selectedTile, mousePosition.x, mousePosition.y, false);
                //     if (checker == true)
                //     {
                //         equipment.RemoveItem(item, index);
                //     }

                //     if (item.amount == 0)
                //     {
                //         customCursor.gameObject.SetActive(false);
                //         Cursor.visible = true;
                //         background[index].color = backgroundColor;
                //         item = null;
                //         othersActive = false;

                //     }
                // }

                // else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MiniBoss1"))
                // {
                //     bool checker = bossMapGenerator.TileCheck(selectedTile, mousePosition.x, mousePosition.y);
                //     if (checker == true)
                //     {
                //         equipment.RemoveItem(item, index);
                //     }

                //     if (item.amount == 0)
                //     {
                //         customCursor.gameObject.SetActive(false);
                //         Cursor.visible = true;
                //         background[index].color = backgroundColor;
                //         item = null;
                //         othersActive = false;

                //     }

                // }


            }
            if (Input.GetMouseButton(0) && mine == true)
            {

                // placeTiles = true;
                //The tile's health

                ani.SetBool("isMining", true);
                int tileHealth = terrainGenerator.checkTileHealth(miningPower, Mathf.RoundToInt(position.x - 0.1f), Mathf.RoundToInt(position.y - 0.1f));
                if (tileHealth > 0)
                {

                    miningCounter = true;


                    if (miningCounter == true)
                    {
                        timeRemaining += Time.deltaTime;
                        //Debug.Log("tilehealth..." + tileHealth);
                        //Debug.Log("timeRemaining..." + timeRemaining);
                        //Debug.Log("mining..." + Time.deltaTime);
                        music.chopTag();
                        if (timeRemaining >= tileHealth - miningPower)
                        {

                            terrainGenerator.BreakTile(Mathf.RoundToInt(position.x - 0.1f), Mathf.RoundToInt(position.y - 0.1f));
                            miningCounter = false;
                            item.durablilty -= 1;
                            if(item.durablilty == 0)
                            {
                                equipment.RemoveItem(item, index);
                                if(item.amount != 0)
                                {
                                    item.durablilty = item.getDurability();
                                }
                            }
                            tileHealth = 0;
                            timeRemaining = 0;
                        }

                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        miningCounter = false;
                        timeRemaining = 0;
                        ani.SetBool("isMining", false);
                    }

                }

                //MineBlock(tileHealth, position);

                // if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Map"))
                // {
                //     ani.SetBool("isMining", true);
                //     int tileHealth = terrainGenerator.checkTileHealth(miningPower, Mathf.RoundToInt(position.x - 0.1f), Mathf.RoundToInt(position.y - 0.1f));
                //     MineBlock(tileHealth, position);
                // }


                // else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MiniBoss1"))
                // {
                //     ani.SetBool("isMining", true);
                //     int tileHealth = bossMapGenerator.checkTileHealth(miningPower, Mathf.RoundToInt(position.x - 0.1f), Mathf.RoundToInt(position.y - 0.1f));
                //     MineBlock(tileHealth, position);
                // }


            }
            else
            {
                ani.SetBool("isMining", false);
            }



        }


        if (isGrounded())
        {
            ani.SetBool("isJumping", false);
            if (axeJump == true)
            {
                axeActive = true;
            }

        }
        cooldownTimer += Time.deltaTime;

        animationTime += Time.deltaTime;



        body.velocity = new Vector2(Input.GetAxis("Horizontal") * runSpeed / 10, body.velocity.y);

        //                body.velocity = Vector2.zero;


        body.gravityScale = 1;

        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            Jump();
        }

    }

    // public void MineBlock(int tileHealth, Vector3 position)
    // {
    //     ani.SetBool("isMining", true);
    //     //int tileHealth = terrainGenerator.checkTileHealth(miningPower, Mathf.RoundToInt(position.x - 0.1f), Mathf.RoundToInt(position.y - 0.1f));

    //     //mining code
    //     // add equipment's mining power to this to mine faster.
    //     // miningPower = ??; <<< add equipment value to current mining power
    //     if (tileHealth > 0)
    //     {

    //         miningCounter = true;


    //         if (miningCounter == true)
    //         {
    //             timeRemaining += Time.deltaTime;
    //             //Debug.Log("tilehealth..." + tileHealth);
    //             //Debug.Log("timeRemaining..." + timeRemaining);
    //             //Debug.Log("mining..." + Time.deltaTime);

    //             if (timeRemaining >= tileHealth - miningPower)
    //             {


    //                 if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Map"))
    //                 {
    //                     terrainGenerator.BreakTile(Mathf.RoundToInt(position.x - 0.1f), Mathf.RoundToInt(position.y - 0.1f));
    //                     miningCounter = false;
    //                     tileHealth = 0;
    //                     timeRemaining = 0;
    //                 }


    //                 else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MiniBoss1"))
    //                 {
    //                     bossMapGenerator.BreakTile(Mathf.RoundToInt(position.x - 0.1f), Mathf.RoundToInt(position.y - 0.1f));
    //                     miningCounter = false;
    //                     tileHealth = 0;
    //                     timeRemaining = 0;
    //                 }


    //             }

    //         }
    //         if (Input.GetMouseButtonUp(0))
    //         {
    //             miningCounter = false;
    //             timeRemaining = 0;
    //             ani.SetBool("isMining", false);
    //         }

    //     }

    // }

    public void craftOpen()
    {
        if (openCraft == false)
        {
            openCraft = true;
            uiCraft.gameObject.SetActive(true);
            craftButton.color = selectedCraftColor;
        }
        else
        {
            openCraft = false;
            uiCraft.gameObject.SetActive(false);
            craftButton.color = craftColor;

        }
    }

    public int getDirection()
    {
        if (transform.localScale == Vector3.one)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    private void Jump()
    {

        //ani.SetBool("isMining", false);
        axeActive = false;

        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            ani.SetBool("isJumping", true);

        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z); ;
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpCoolDown = 0;

        }

    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(edgeCollider.bounds.center, edgeCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(edgeCollider.bounds.center, edgeCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return !onWall();
    }

    public void keySelected(int indexSelected)
    {
        if (indexSelected == -1)
        {
            others.SetActive(false);
        }
        try
        {
            item = equipment.GetEquipment(indexSelected);

            itemType = item.itemType;
            staffActive = false;
            othersActive = false;
            axeActive = false;
            axeJump = false;
            mine = false;
            index = indexSelected;
            for (int i = 0; i < background.Length; i++)
            {
                background[i].color = backgroundColor;
            }

            if (itemType == Item.ItemType.Weapon)
            {
                staffActive = true;

            }
            if (item.isAxe())
            {
                axe.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
                miningPower = item.miningPower();
                mine = true;
                axeActive = true;
                axeJump = true;
            }
            if (item.isPickAxe())
            {
                axe.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
                miningPower = item.miningPower();
                mine = true;
                axeActive = true;
                axeJump = true;
            }
            if (itemType != Item.ItemType.Weapon && !item.isAxe() && !item.isPickAxe())
            {
                others.GetComponent<Image>().sprite = item.GetSprite();
                othersActive = true;
            }
            if (itemType != Item.ItemType.Weapon && itemType != Item.ItemType.Food && itemType != Item.ItemType.Meat && itemType != Item.ItemType.cactusFruit  && itemType != Item.ItemType.Coin && !item.isAxe() && !item.isPickAxe() && itemType != Item.ItemType.Potion)
            {
                for (int i = 0; i < tile.Length; i++)
                {

                    if (tile[i].name == itemType.ToString())
                    {
                        selectedTile = tile[i];
                        customCursor.gameObject.SetActive(true);
                        customCursor.GetComponent<SpriteRenderer>().sprite = selectedTile.tileSprites[0];



                        Cursor.visible = false;
                        placeTiles = true;

                    }
                }
            }
            else
            {
                customCursor.gameObject.SetActive(false);
                Cursor.visible = true;
                placeTiles = false;

            }
            original = item;
        }
        catch
        {
            item = null;
            othersActive = false;
            original = null;
        }


    }
    // leveling
    public void OnLevelUp()
    {
        print("levelUp");
        int oldEXP = level.experience;
        int newexp = level.GetXPforLevel(level.currentLevel);
        level.experience = 0;
        level.experience = (oldEXP - newexp);
    }

    public void LoadData(GameData data)
    {
        foreach (Item item in data.inventoryItems)
        {
            if (item != null)
            {
                inventory.AddItem(item);

            }
        }
        Item[] equipmentLoaded = data.equipmentItems.ToArray();
        for (int i = 0; i < equipmentLoaded.Length; i++)
        {
            if (equipmentLoaded[i] != null)
            {
                equipment.AddItem(equipmentLoaded[i], i);
            }
        }
        //transform.position = data.playerPosition;

        if (!EnterPortal.sceneLoaded)
        {

        }
    }

    public void SaveData(ref GameData data)
    {
        data.inventoryItems = inventory.GetItemList();
        data.equipmentItems = equipment.arrayToList();
        data.playerPosition = transform.position;
    }
}
