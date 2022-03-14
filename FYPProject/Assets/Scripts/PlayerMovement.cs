using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;   
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator ani;
    private BoxCollider2D boxCollider;
    private float wallJumpCoolDown;
    private float horizontalInput; 

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();    
    }
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //flip character
        if(horizontalInput > 0.001f)
        {
            transform.localScale = Vector3.one;
        }      
        else if(horizontalInput < -0.001f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
       
        ani.SetBool("run", horizontalInput!=0);
        ani.SetBool("grounded", isGrounded());

        if (wallJumpCoolDown > 0.2f)
        {

            body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

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
        print(onWall());
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            ani.SetTrigger("jump");
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
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
