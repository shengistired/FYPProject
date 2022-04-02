using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private GameObject staff;
    [SerializeField] private Image[] background;

    public TerrainGeneration terrainGenerator;


    public UnityEngine.Vector2Int mousePosition;
    public UnityEngine.Vector2 spawnPosition;
    [SerializeField] private GameObject options;

    public bool placeTiles;
    public TileClass selectedTile;
    public int playerPlaceRange;

    public int miningPower;
    private Boolean miningCounter = false;
    private float timeRemaining;
    int lockMousePointerX;
    int lockMousePointerY;

    private float cooldownTimer = Mathf.Infinity;
    private float attackCoolDown = 1;
    private float animationTime = 0;
    private float wallJumpCoolDown;
    private float horizontalInput;
    private bool mine = false;
    private bool staffActive = false;
    private bool axeActive = false;
    private bool axeJump = false;
    public int rotationOffset = 0;

    private Rigidbody2D body;
    private Animator ani;

    private BoxCollider2D boxCollider;
    private EdgeCollider2D edgeCollider;

    private Inventory inventory;
    public static Equipment equipment;

    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private UI_EquipmentSlot[] uiEquipmentSlot;
    [SerializeField] private UI_Equipment uiEquip;
    [SerializeField] private PlayerAttack attack;
    public static int directionNum;
    private Item item;
    public float runSpeed;
    private float horizontalMove = 0f;
    [SerializeField] private StaminaBar stamina;
    [SerializeField] private GameObject axe;
    private int count = 0;
    public int direct;
    private void Awake()
    {

        runSpeed = 50f;
        directionNum = 1;
        body = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);
        equipment = new Equipment(UseItem);
        for(int i = 0; i< 9; i++)
        {
            try
            {
                uiEquipmentSlot[i].SetEquipment(equipment);
            }
            catch
            {

            }

        }

        direct = 1;

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

    public void AddEquipment(Item item, int index)
    {

        equipment.AddItem(item, index);
        //uiEquipmentSlot[index].SetEquipment(equipment);
    }
    private void Update()
    {

        mousePosition.x = Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
        mousePosition.y = Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        horizontalInput = Input.GetAxis("Horizontal");
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        ani.SetFloat("Speed", Mathf.Abs(horizontalMove));

        uiInventory.inventory_Position();
        uiEquip.equipment_Position();
        keySelected();

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
            //transform.localScale = new Vector3(-1, 1, 1);
            transform.localScale = new Vector3(-1, 1, 1);
            direct = -1;
            directionNum = -1;
        }
        if (stamina.currentStamina != 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                runSpeed = 75f;

            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                runSpeed = 50f;
            }
        }
        else
        {
            runSpeed = 50f;
        }

        if(axeActive == true)
        {
            axe.SetActive(true);
        }
        else
        {
            axe.SetActive(false);
        }

        placeTiles = Input.GetMouseButton(2);

        if (Input.GetMouseButtonDown(1) && cooldownTimer > attackCoolDown && staffActive == true)
        {
            ani.SetTrigger("Attack");
            attack.attack();
            cooldownTimer = 0;
            animationTime = 0;
        }
        if (Vector2.Distance(transform.position, mousePosition) <= playerPlaceRange && Vector2.Distance(transform.position, mousePosition) > 0.5f)
        {

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
                        Debug.Log("tilehealth..." + tileHealth);
                        Debug.Log("timeRemaining..." + timeRemaining);
                        Debug.Log("mining..." + Time.deltaTime);

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
            if (Input.GetMouseButton(2))
            {
                //place down a block 
                terrainGenerator.TileCheck(selectedTile, mousePosition.x, mousePosition.y, true);
            }


        }


        if (isGrounded())
        {
            ani.SetBool("isJumping", false);
            if(axeJump == true)
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
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return !onWall();
    }

    public void keySelected()
    {
        Color backgroundColor = new Color32(0,0,0,255);
        Color selectedColor = new Color32(60, 60, 60, 255);
        if (Input.GetKeyDown("1"))
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].color = backgroundColor;
            }   

            background[0].color = selectedColor;
            staffActive = true;
            axeActive = false;
            axeJump = false;
            mine = false;
            staff.SetActive(true);
        }

        if (Input.GetKeyDown("2"))
        {
            mine = true;
            for(int i = 0; i < background.Length; i++)
            {
                background[i].color = backgroundColor;

            }
            staffActive = false;
            axeActive = true;
            axeJump = true;
            background[1].color = selectedColor;
            staff.SetActive(false);

            // Debug.Log(equipment.GetEquipment(0));

        }
        if (Input.GetKeyDown("3"))
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].color = backgroundColor;
            }

            background[2].color = selectedColor;
            Debug.Log(equipment.GetEquipment(1));
            staffActive = false;
            axeActive = false;
            axeJump = false;
            mine = false;
            staff.SetActive(false);

            //staff.SetActive(true);
        }
        if (Input.GetKeyDown("4"))
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].color = backgroundColor;
            }

            background[3].color = selectedColor;
            staffActive = false;
            axeActive = false;
            axeJump = false;
            mine = false;
            staff.SetActive(false);
            //staff.SetActive(true);
        }
        if (Input.GetKeyDown("5"))
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].color = backgroundColor;
            }

            background[4].color = selectedColor;
            staffActive = false;
            axeActive = false;
            axeJump = false;
            mine = false;
            staff.SetActive(false);
            //staff.SetActive(true);
        }
        if (Input.GetKeyDown("6"))
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].color = backgroundColor;
            }

            background[5].color = selectedColor;
            staffActive = false;
            axeActive = false;
            axeJump = false;
            mine = false;
            staff.SetActive(false);
            //staff.SetActive(true);
        }
        if (Input.GetKeyDown("7"))
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].color = backgroundColor;
            }

            background[6].color = selectedColor;
            staffActive = false;
            axeActive = false;
            axeJump = false;
            mine = false;
            staff.SetActive(false);
            //staff.SetActive(true);
        }
        if (Input.GetKeyDown("8"))
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].color = backgroundColor;
            }

            background[7].color = selectedColor;
            staffActive = false;
            axeActive = false;
            axeJump = false;
            mine = false;
            staff.SetActive(false);
            //staff.SetActive(true);
        }
        if (Input.GetKeyDown("9"))
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].color = backgroundColor;
            }

            background[8].color = selectedColor;
            staffActive = false;
            axeActive = false;
            axeJump = false;
            mine = false;
            staff.SetActive(false);
            //staff.SetActive(true);
        }
        if (Input.GetKeyDown("0"))
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].color = backgroundColor;
            }

            background[9].color = selectedColor;
            staffActive = false;
            axeActive = false;
            axeJump = false;
            mine = false;
            staff.SetActive(false);
            //staff.SetActive(true);
        }

    }

}
