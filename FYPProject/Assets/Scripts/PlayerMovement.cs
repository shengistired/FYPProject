using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private GameObject staff;

    private float cooldownTimer = Mathf.Infinity;
    private float attackCoolDown = 1;
    private float animationTime = 0;
    private float wallJumpCoolDown;
    private float horizontalInput;

    public int rotationOffset = 0;

    private Rigidbody2D body;
    private Animator ani;

    private BoxCollider2D boxCollider;

    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;
    public float runSpeed = 50f;
    private float horizontalMove = 0f;
    [SerializeField] private GameObject axe;
    private int count = 0;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);

    }
    public Vector3 getPosition()
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
        if(itemWorld!= null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        ani.SetFloat("Speed", Mathf.Abs(horizontalMove));
        Vector3 centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.6f, 10f));
        if (Input.GetKeyDown("i"))
        {
            if(count == 0)
            {
                uiInventory.transform.position = centerPos;
                uiInventory.gameObject.SetActive(true);
                count = 1;
            }
            else
            {
                uiInventory.gameObject.SetActive(false);
                count = 0;
            }

        }


        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10f));
        float distance = position.x - transform.position.x;


        if (horizontalInput > 0.001f)
        {
            transform.localScale = Vector3.one;
        }      
        else if(horizontalInput < -0.001f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (isGrounded()){
            ani.SetBool("isJumping", false);
            if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && position.y < 76.6 && distance <= 1.5 && distance >= -1.5)
            {
                ani.SetBool("isMining", true);
                axe.SetActive(true);

            }
            else
            {
                ani.SetBool("isMining", false);
                axe.SetActive(false);
                if( Input.GetMouseButtonDown(0) && cooldownTimer > attackCoolDown)
                {        
                    attack();
                }


            }

        }
        cooldownTimer += Time.deltaTime;
        if (animationTime > 0.5)
        {
            staff.SetActive(false);
        }
        animationTime += Time.deltaTime;

        if (wallJumpCoolDown > 0.2f)
        {

            body.velocity = new Vector2(Input.GetAxis("Horizontal") * runSpeed/10, body.velocity.y);

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

        ani.SetBool("isMining", false);
        axe.SetActive(false);
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            ani.SetBool("isJumping", true);
        }
        else if(onWall() && !isGrounded())
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

    public void attack()
    {
        
        ani.SetTrigger("Attack");
        staff.SetActive(true);


        cooldownTimer = 0;
        animationTime = 0;
        fireballs[findFireball()].transform.position = firePoint.position;
        fireballs[findFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int findFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
