using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Patrol : MonoBehaviour
{
    public float walkSpeed;
    // public float distance;
    //public float thrust = 1.0f;

    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn;

    public Rigidbody2D rb;
    public Transform groundcheckPos;
    public LayerMask groundLayer;
    //public LayerMask Enemies;
    public Collider2D bodyCollider;

    void Start()
    {
        mustPatrol = true;
        //Physics2D.IgnoreCollision(rb.GetComponent<Collider2D>(), rb.GetComponent<Collider2D>(), false);
        Physics2D.IgnoreLayerCollision(7, 7, true);
        //Rigidbody rb = GetComponent<Rigidbody>();
        //rb.AddForce(thrust, 0, 0, ForceMode.Impulse);
    }

    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }
        
        /*transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);
         RaycastHit2D groundInfo = Physics2D.Raycast(groundcheckPos.position, Vector2.down, groundLayer);
         if (groundInfo.collider == false)
         {
             if (mustTurn == true)
             {
                 transform.eulerAngles = new Vector3(0, -180, 0);
                 mustTurn = false;
             }
             else
             {
                 transform.eulerAngles = new Vector3(0, 0, 0);
                 mustTurn = true;
             }
         }*/
    }

    private void FixedUpdate()
    {
        if (mustPatrol==true)
        {
            mustTurn = !Physics2D.OverlapCircle(groundcheckPos.position, 0.1f, groundLayer);
            // Patrol();
        }
    }

    void Patrol()
    {
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
        if (mustTurn == true || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
    }

    void Flip()
    {
        //mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustTurn = false;
    }
}