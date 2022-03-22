using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Patrol : MonoBehaviour
{
    public float walkSpeed, range;
    private float disToPlayer;

    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn;

    public Rigidbody2D rb;
    public Transform groundcheckPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public Transform newplayer;

    void Start()
    {
        mustPatrol = true;
        //Physics2D.IgnoreCollision(rb.GetComponent<Collider2D>(), rb.GetComponent<Collider2D>(), false);
        Physics2D.IgnoreLayerCollision(7, 7, true);
        newplayer = GameObject.Find("NewPlayer").transform;
    }

    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }

        disToPlayer = Vector2.Distance(transform.position, newplayer.position);

        if(disToPlayer <= range)
        {
            if(newplayer.position.x > transform.position.x && transform.localScale.x < 0
               || newplayer.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }

            mustPatrol = false;   
            Shoot();
        }
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

    void Shoot()
    {
        //Shoot
    }
}