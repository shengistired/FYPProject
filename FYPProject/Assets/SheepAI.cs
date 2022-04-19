using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepAI : MonoBehaviour
{
    public float walkSpeed;

    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn;

    public Rigidbody2D rb;
    public Transform groundcheckPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    //public GameObject boom;


    void Start()
    {
        mustPatrol = true;
        Physics2D.IgnoreLayerCollision(7, 7, true);
    }

    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0)
        {
            Destroy(gameObject);
            GameObject.Find("Spawn_Sheep").GetComponent<Spawn_Sheep>().animalMin -= 1;
            Debug.Log("Invisible");
        }

        //mustPatrol = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.name)
        {
            case "Fireball(Clone)":
                //Instantiate(boom, col.gameObject.transform.position, Quaternion.identity);
                //gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                Item item = new Item();
                item.itemType = Item.ItemType.Meat;
                item.amount = 1;
                ItemWorld.SpawnItemWorld(new Vector2(transform.position.x, transform.position.y), item);

                Destroy(gameObject);
                Debug.Log("Killed sheep");
                break;
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