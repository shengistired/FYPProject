using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private GameObject staff;
    [SerializeField] private Image[] background;
    [SerializeField] private UI_Inventory uiInventory;
    //  [SerializeField] private UI_EquipmentSlot[] uiEquipmentSlot;
    [SerializeField] private UI_EquipmentSlot uiEquipmentSlot;
    [SerializeField] private UI_Equipment uiEquip;
    [SerializeField] private PlayerAttack attack;
    [SerializeField] private StaminaBar stamina;
    [SerializeField] private GameObject axe;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject others;
    [SerializeField] private CustomCursor customCursor;


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


    public UnityEngine.Vector2Int mousePosition;
    public UnityEngine.Vector2 spawnPosition;

    public bool placeTiles;
    private bool mine = false;
    private bool staffActive = false;
    private bool axeActive = false;
    private bool othersActive = false;
    private bool axeJump = false;
    private bool miningCounter = false;
    public bool moved;
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
    private Item item;
    private Item original;
    public TerrainGeneration terrainGenerator;
    private int newItemIndex;
    Vector3 difference;
    Vector3 equipmentPosition;
    private void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);

        placeTiles = false;

        directionNum = 1;
        body = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);
        equipment = new Equipment(UseItem);
        direct = 1;
        uiEquipmentSlot.SetEquipment(equipment);


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
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }
    public Item getEquipment(int index)
    {
        return equipment.GetEquipment(index);
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
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                running = false;
                runSpeed = 40f;
            }
        }
        else
        {
            runSpeed = 50f;
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
            else if (original.itemType == Item.ItemType.Axe)
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

            if (itemType == Item.ItemType.Food && item.amount > 0 && (difference.y > 1.5 || difference.x <0 || difference.x > 15))
            {
                equipment.RemoveItem(item, index);
                if (item.amount == 0)
                {
                    background[index].color = backgroundColor;
                    item = null;
                    othersActive = false;
                }
            }


        }
        if (Input.GetMouseButtonDown(1) && cooldownTimer > attackCoolDown && staffActive == true)
        {
            ani.SetTrigger("Attack");
            attack.attack();
            cooldownTimer = 0;
            animationTime = 0;
            ManaBar.instance.UseMana(10);
            // Reduce mana each usage of staff
        }
        if (Vector2.Distance(transform.position, mousePosition) <= playerPlaceRange && Vector2.Distance(transform.position, mousePosition) > 0.5f)
        {
            if (Input.GetMouseButtonDown(0) && placeTiles == true && item!= null && (difference.y > 1.5 || difference.x < 0 || difference.x > 15))
            {
                for (int i = 0; i < tile.Length; i++)
                {

                    if (tile[i].name == itemType.ToString())
                    {
                        selectedTile = tile[i];

                    }
                }
                terrainGenerator.TileCheck(selectedTile, mousePosition.x, mousePosition.y, true);
                equipment.RemoveItem(item, index);
                if (item.amount == 0)
                {
                    customCursor.gameObject.SetActive(false);
                    Cursor.visible = true;
                    background[index].color = backgroundColor;
                    item = null;
                    othersActive = false;

                }
            }
            if (Input.GetMouseButton(0) && mine == true)
            {
                ani.SetBool("isMining", true);
                // placeTiles = true;
                //The tile's health
                int tileHealth = terrainGenerator.checkTileHealth(miningPower, Mathf.RoundToInt(position.x - 0.1f), Mathf.RoundToInt(position.y - 0.1f));


                //mining code
                // add equipment's mining power to this to mine faster.
                // miningPower = ??; <<< add equipment value to current mining power
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
                    }

                }


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
            //if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && position.y < 76.6 && distance <= 1.5 && distance >= -1.5)

            //if ((Input.GetMouseButton(0)) && distance <= 1.5 && distance >= -1.5 && distanceY <= 3 && distanceY >= -1.5)
        }
        cooldownTimer += Time.deltaTime;
        //     if (animationTime > 0.3)
        //     {
        //         staff.SetActive(false);
        //   }
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
            if (itemType == Item.ItemType.Axe)
            {
                mine = true;
                axeActive = true;
                axeJump = true;
            }
            if (itemType != Item.ItemType.Weapon && itemType != Item.ItemType.Axe)
            {
                others.GetComponent<Image>().sprite = item.GetSprite();
                othersActive = true;
            }
            if (itemType != Item.ItemType.Weapon && itemType != Item.ItemType.Food && itemType != Item.ItemType.Coin && itemType != Item.ItemType.Axe && itemType != Item.ItemType.Potion)
            {
                for (int i = 0; i < tile.Length; i++)
                {
                    //string name  = tile[i].name.Substring(0, tile[i].name.Length - 12);
                    //Debug.Log(name);                   
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


        /*
        if (Input.GetKeyDown("1"))
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].color = backgroundColor;
            }

            background[0].color = selectedColor;
            item = equipment.GetEquipment(0);
            itemTypeString = item.itemType.ToString();
            staffActive = true;
            axeActive = false;
            axeJump = false;
            mine = false;
            staff.SetActive(true);
            others.SetActive(false);

            if (itemTypeString != "Weapon" && itemTypeString != "Food" && itemTypeString != "Coin" && itemTypeString != "Axe" && itemTypeString != "Potion")
            {
                for (int i = 0; i < tile.Length; i++)
                {
                    //string name  = tile[i].name.Substring(0, tile[i].name.Length - 12);
                    //Debug.Log(name);
                    if (tile[i].name == itemTypeString)
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
        }

        if (Input.GetKeyDown("2"))
        {
            mine = true;
            for (int i = 0; i < background.Length; i++)
            {
                background[i].color = backgroundColor;

            }
            item = equipment.GetEquipment(1);
            itemTypeString = item.itemType.ToString();
            staffActive = false;
            axeActive = true;
            axeJump = true;
            background[1].color = selectedColor;
            staff.SetActive(false);
            others.SetActive(false);
            if (itemTypeString != "Weapon" && itemTypeString != "Food" && itemTypeString != "Coin" && itemTypeString != "Axe" && itemTypeString != "Potion")
            {
                for (int i = 0; i < tile.Length; i++)
                {
                    //string name  = tile[i].name.Substring(0, tile[i].name.Length - 12);
                    //Debug.Log(name);
                    if (tile[i].name == itemTypeString)
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

        }
        if (Input.GetKeyDown("3") && equipment.GetEquipment(2) != null)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].color = backgroundColor;
            }

            background[2].color = selectedColor;
            index = 2;
            item = equipment.GetEquipment(2);
            itemTypeString = item.itemType.ToString();
            staffActive = false;
            axeActive = false;
            axeJump = false;
            mine = false;
            staff.SetActive(false);


            if (itemTypeString != "Weapon" && itemTypeString != "Food" && itemTypeString != "Coin" && itemTypeString != "Axe" && itemTypeString != "Potion")
            {
                for (int i = 0; i < tile.Length; i++)
                {
                    //string name  = tile[i].name.Substring(0, tile[i].name.Length - 12);
                    //Debug.Log(name);
                    if (tile[i].name == itemTypeString)
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
        }
        if (Input.GetKeyDown("4") && equipment.GetEquipment(3) != null)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].color = backgroundColor;
            }

            background[3].color = selectedColor;
            index = 3;
            item = equipment.GetEquipment(3);
            itemTypeString = item.itemType.ToString();
            staffActive = false;
            axeActive = false;
            axeJump = false;
            mine = false;
            staff.SetActive(false);
            others.GetComponent<Image>().sprite = item.GetSprite();
            others.SetActive(true);
            if (itemTypeString != "Weapon" && itemTypeString != "Food" && itemTypeString != "Coin" && itemTypeString != "Axe" && itemTypeString != "Potion")
            {
                for (int i = 0; i < tile.Length; i++)
                {
                    //string name  = tile[i].name.Substring(0, tile[i].name.Length - 12);
                    //Debug.Log(name);
                    if (tile[i].name == itemTypeString)
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
        }
        if (Input.GetKeyDown("5") && equipment.GetEquipment(4) != null)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].color = backgroundColor;
            }

            background[4].color = selectedColor;
            index = 4;
            item = equipment.GetEquipment(4);
            itemTypeString = item.itemType.ToString();
            staffActive = false;
            axeActive = false;
            axeJump = false;
            mine = false;
            staff.SetActive(false);
            others.GetComponent<Image>().sprite = item.GetSprite();
            others.SetActive(true);
            if (itemTypeString != "Weapon" && itemTypeString != "Food" && itemTypeString != "Coin" && itemTypeString != "Axe" && itemTypeString != "Potion")
            {
                for (int i = 0; i < tile.Length; i++)
                {
                    //string name  = tile[i].name.Substring(0, tile[i].name.Length - 12);
                    //Debug.Log(name);
                    if (tile[i].name == itemTypeString)
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
        }
        if (Input.GetKeyDown("6") && equipment.GetEquipment(5) != null)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].color = backgroundColor;
            }

            background[5].color = selectedColor;
            index = 5;
            item = equipment.GetEquipment(5);
            itemTypeString = item.itemType.ToString();
            staffActive = false;
            axeActive = false;
            axeJump = false;
            mine = false;
            staff.SetActive(false);
            others.GetComponent<Image>().sprite = item.GetSprite();
            others.SetActive(true);
            if (itemTypeString != "Weapon" && itemTypeString != "Food" && itemTypeString != "Coin" && itemTypeString != "Axe" && itemTypeString != "Potion")
            {
                for (int i = 0; i < tile.Length; i++)
                {
                    //string name  = tile[i].name.Substring(0, tile[i].name.Length - 12);
                    //Debug.Log(name);
                    if (tile[i].name == itemTypeString)
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
        }
        if (Input.GetKeyDown("7") && equipment.GetEquipment(6) != null)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].color = backgroundColor;
            }

            background[6].color = selectedColor;
            index = 6;
            item = equipment.GetEquipment(6);
            itemTypeString = item.itemType.ToString();
            staffActive = false;
            axeActive = false;
            axeJump = false;
            mine = false;
            staff.SetActive(false);
            others.GetComponent<Image>().sprite = item.GetSprite();
            others.SetActive(true);
            if (itemTypeString != "Weapon" && itemTypeString != "Food" && itemTypeString != "Coin" && itemTypeString != "Axe" && itemTypeString != "Potion")
            {
                for (int i = 0; i < tile.Length; i++)
                {
                    //string name  = tile[i].name.Substring(0, tile[i].name.Length - 12);
                    //Debug.Log(name);
                    if (tile[i].name == itemTypeString)
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
        }
        if (Input.GetKeyDown("8") && equipment.GetEquipment(7) != null)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].color = backgroundColor;
            }

            background[7].color = selectedColor;
            index = 7;

            item = equipment.GetEquipment(7);
            itemTypeString = item.itemType.ToString();
            staffActive = false;
            axeActive = false;
            axeJump = false;
            mine = false;
            staff.SetActive(false);
            others.GetComponent<Image>().sprite = item.GetSprite();
            others.SetActive(true);
            if (itemTypeString != "Weapon" && itemTypeString != "Food" && itemTypeString != "Coin" && itemTypeString != "Axe" && itemTypeString != "Potion")
            {
                for (int i = 0; i < tile.Length; i++)
                {
                    //string name  = tile[i].name.Substring(0, tile[i].name.Length - 12);
                    //Debug.Log(name);
                    if (tile[i].name == itemTypeString)
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
                placeTiles = false ;

            }
        }
        if (Input.GetKeyDown("9") && equipment.GetEquipment(8) != null)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].color = backgroundColor;
            }

            background[8].color = selectedColor;
            index = 8;

            item = equipment.GetEquipment(8);
            itemTypeString = item.itemType.ToString();
            staffActive = false;
            axeActive = false;
            axeJump = false;
            mine = false;
            staff.SetActive(false);
            others.GetComponent<Image>().sprite = item.GetSprite();
            others.SetActive(true);
            if (itemTypeString != "Weapon" && itemTypeString != "Food" && itemTypeString != "Coin" && itemTypeString != "Axe" && itemTypeString != "Potion")
            {
                for (int i = 0; i < tile.Length; i++)
                {
                    //string name  = tile[i].name.Substring(0, tile[i].name.Length - 12);
                    //Debug.Log(name);
                    if (tile[i].name == itemTypeString)
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
        }
        if (Input.GetKeyDown("0") && equipment.GetEquipment(9) != null)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].color = backgroundColor;
            }

            background[9].color = selectedColor;
            index = 9;
            item = equipment.GetEquipment(9);
            itemTypeString = item.itemType.ToString();
            staffActive = false;
            axeActive = false;
            axeJump = false;
            mine = false;
            staff.SetActive(false);
            others.GetComponent<Image>().sprite = item.GetSprite();
            others.SetActive(true);
            if (itemTypeString != "Weapon" && itemTypeString != "Food" && itemTypeString != "Coin" && itemTypeString != "Axe" && itemTypeString != "Potion")
            {
                for (int i = 0; i < tile.Length; i++)
                {
                    //string name  = tile[i].name.Substring(0, tile[i].name.Length - 12);
                    //Debug.Log(name);
                    if (tile[i].name == itemTypeString)
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
        }
        */


    }


}
