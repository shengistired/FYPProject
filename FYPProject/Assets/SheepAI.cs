using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepAI : MonoBehaviour
{
    public float walkSpeed, range;
    private float disToPlayer;

    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn;

    public Rigidbody2D rb;
    public Transform groundcheckPos;
    public Transform player;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;

    void Start()
    {
        mustPatrol = true;
        Physics2D.IgnoreLayerCollision(7, 7, true);
        player = GameObject.Find("Mage").transform;
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
                mustPatrol = true;
            }
            else
            {
                Flip();
            }
        }
        else
        {
            mustPatrol = true;
        }

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0)
        {
            Destroy(gameObject);
            GameObject.Find("Spawn_Sheep").GetComponent<Spawn_Sheep>().animalMin -= 1;
        }

        //mustPatrol = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Item item = new Item();
        if (col.gameObject.name == "Staff" && PlayerController.normalAttack)
        {
            item.itemType = Item.ItemType.Meat;
            item.amount = 1;
            ItemWorld.SpawnItemWorld(new Vector2(transform.position.x, transform.position.y), item);

            Destroy(gameObject);
        }
        else
        {
            switch (col.gameObject.name)
            {
                case "Fireball(Clone)":
                    //gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                    item.itemType = Item.ItemType.Meat;
                    item.amount = 1;
                    ItemWorld.SpawnItemWorld(new Vector2(transform.position.x, transform.position.y), item);

                    Destroy(gameObject);
                    GameObject.Find("Spawn_Sheep").GetComponent<Spawn_Sheep>().animalMin -= 1;
                    break;
            }
        }

    }

    private void FixedUpdate()
    {
        if (mustPatrol == true)
        {
            mustTurn = !Physics2D.OverlapCircle(groundcheckPos.position, 0.1f, groundLayer);
            //Patrol();
        }
    }

    //enemy move
    void Patrol()
    {
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
        if (mustTurn == true || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
    }

    //enemy flip
    void Flip()
    {
        //mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustTurn = false;
    }
}