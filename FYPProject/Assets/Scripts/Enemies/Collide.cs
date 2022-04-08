using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide : MonoBehaviour
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
    public Transform player;

    public int enemyMin;

    void Start()
    {
        mustPatrol = true;
        Physics2D.IgnoreLayerCollision(7, 7, true);
        player = GameObject.Find("Mage").transform;
        enemyMin = GameObject.Find("Spawn_Collide").GetComponent<Spawn_Collide>().enemyMin;
    }

    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }

        disToPlayer = Vector2.Distance(transform.position, player.position);

        if (disToPlayer <= range)
        {
            if (player.position.x > transform.position.x && transform.localScale.x < 0
               || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }

            //mustPatrol = false;
            //rb.velocity = Vector2.zero;
        }
        else
        {
            mustPatrol = true;
        }

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0)
        {
            Destroy(gameObject);
            GameObject.Find("Spawn_Collide").GetComponent<Spawn_Collide>().enemyMin -= 1;
            Debug.Log("Invisible");
        }
       
    }

    /*void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.name)
        {
            case "Fireball(Clone)":
            //EnemySpawn.spawnAllowed = false;
            //Instantiate(boom, col.gameObject.transform.position, Quaternion.identity);

            Die();
            Debug.Log("Killed collide");

            break;
        }
    }*/

    private void FixedUpdate()
    {
        if (mustPatrol == true)
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

    void Die()
    {
        Destroy(gameObject);
        GameObject.Find("Spawn_Collide").GetComponent<Spawn_Collide>().enemyMin -= 1;
        Debug.Log("Die");
    }
}