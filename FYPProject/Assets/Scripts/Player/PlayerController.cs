using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
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
    [SerializeField] private PlayerAttack attack;
    [SerializeField] private StaminaBar stamina;
    [SerializeField] private GameObject axe;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject others;
    [SerializeField] private CustomCursor customCursor;
    [SerializeField] private Image craftButton;
    [SerializeField] private TileAtlas tileAtlas;
    [SerializeField] private GameObject content;
    [SerializeField] private Settings settings;
    public audio_manager music;

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

    private Rigidbody2D body;
    private Animator ani;
    private EdgeCollider2D edgeCollider;
    private Inventory inventory;
    public static Equipment equipment;
    public static CraftItem craftItem;
    private Item item;
    private Item original;
    public TerrainGeneration terrainGenerator;
    //public BossMapGenerator bossMapGenerator;
    private int newItemIndex;
    Vector3 difference;
    Vector3 equipmentPosition;

    //leveling
    public Level level;


    private TileClass tileWood;
    private void Awake()
    {
        settings.changeVolume();
        AudioListener.volume =  PlayerPrefs.GetFloat("musicVolume");
        //DontDestroyOnLoad(transform.gameObject);
        tileWood = tileAtlas.treeWood;


        directionNum = 1;
        body = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);
        equipment = new Equipment(UseItem);
        craftItem = new CraftItem();
        direct = 1;
        uiEquipmentSlot.SetEquipment(equipment);

        foreach(CraftingRecipeUI craftingRecipeUI in content.GetComponentsInChildren<CraftingRecipeUI>())
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

            if (equipment.filledList().Count > 0)
            {
                if (!equipment.AddItemCollide(itemWorld.GetItem(), equipment.filledList()[0]))
                {
                    equipment.AddItem(itemWorld.GetItem(), equipment.filledList()[0]);
                }
            }
            else if (!equipment.AddItemCollide(itemWorld.GetItem(), equipment.filledList()[0]))
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

    public void AddCraftItem(Item item, int index)
    {
        craftItem.AddItem(item, index);

        if (CraftItem.addInventory)
        {
            inventory.AddItem(equipment.previousItem());
        }
    }
    public void CraftToInventory(int index, Item item)
    {
        craftItem.RemoveItem(index);
        inventory.AddItem(item);
    }
    public void CraftSuccessful(Item item)
    {
        craftItem.RemoveItem(0);
        craftItem.RemoveItem(1);
        inventory.AddItem(item);

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

        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10f));
        float distance = position.x - transform.position.x;
        float distanceY = position.y - transform.position.y;
        if (horizontalInput > 0)
        {
            //transform.localScale = Vector3.one;
            transform.localScale = new Vector3(1, 1, 1);
            direct = 1;
            directionNum = 1;
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            direct = -1;
            directionNum = -1;
        }
        if (stamina.currentStamina != 0)
        {

            if (Input.GetKeyDown(KeyCode.LeftShift) && horizontalMove != 0)
            {

                running = true;
                runSpeed = 75f;

            }
            if (Input.GetKeyUp(KeyCode.LeftShift) || horizontalMove == 0)
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
        if(placeTiles == true)
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

            if ((itemType == Item.ItemType.Food || itemType == Item.ItemType.Meat) && item.amount > 0 && (difference.y > 1.5 || difference.x < 0 || difference.x > 15))
            {
                equipment.RemoveItem(item, index);
                if (item.amount == 0)
                {
                    background[index].color = backgroundColor;
                    item = null;
                    othersActive = false;
                    FoodBar.food += 40f;
                }
            }


        }
        if (Input.GetMouseButtonDown(0) && staffActive == true)
        {
            ani.SetTrigger("isAttack");
            
        }

        if (Input.GetMouseButtonDown(1) && cooldownTimer > attackCoolDown && staffActive == true)
        {
            attack.attack();
            cooldownTimer = 0;
            animationTime = 0;
            // Reduce mana each usage of staff
            ManaBar.instance.UseMana(10);
            ani.SetTrigger("isAttack");
            //play fireball sound effect
            music.fireBall_play();
    
        }
        if (Vector2.Distance(transform.position, mousePosition) <= playerPlaceRange && Vector2.Distance(transform.position, mousePosition) > 0.5f)
        {
            if (Input.GetMouseButtonDown(0) && placeTiles == true && item != null && (difference.y > 1.5 || difference.x < 0 || difference.x > 15))
            {
                Debug.Log(selectedTile.name);
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

                        if (timeRemaining >= tileHealth - miningPower)
                        {



                            terrainGenerator.BreakTile(Mathf.RoundToInt(position.x - 0.1f), Mathf.RoundToInt(position.y - 0.1f));
                            miningCounter = false;
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

        if (wallJumpCoolDown > 0.2f)
        {

            body.velocity = new Vector2(Input.GetAxis("Horizontal") * runSpeed / 10, body.velocity.y);

            if (onWall() && isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 1;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
            wallJumpCoolDown += Time.deltaTime;
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
                mine = true;
                axeActive = true;
                axeJump = true;
            }
            if (itemType != Item.ItemType.Weapon && !item.isAxe())
            {
                others.GetComponent<Image>().sprite = item.GetSprite();
                othersActive = true;
            }
            if (itemType != Item.ItemType.Weapon && itemType != Item.ItemType.Food  && itemType != Item.ItemType.Meat && itemType != Item.ItemType.Coin && !item.isAxe() && itemType != Item.ItemType.Potion)
            {
                for (int i = 0; i < tile.Length; i++)
                {
                    //string name  = tile[i].name.Substring(0, tile[i].name.Length - 12);
                    //Debug.Log(name);
                     Debug.Log(itemType);
                     Debug.Log(tile[i].name);

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

}
