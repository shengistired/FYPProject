using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide : MonoBehaviour
{
    public float walkSpeed, range, damage;
    private float disToPlayer;


    [HideInInspector]
    public bool mustPatrol, haveToFlip;
    private bool mustTurn;
    //public int lvl;

    public Rigidbody2D rb;
    public Transform groundcheckPos;
    public Transform player;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;

    public PortalEnteredText portalEnteredText;
    public PlayerStat playerStat;

    void Start()
    {
        mustPatrol = true;
        Physics2D.IgnoreLayerCollision(7, 7, true);

        player = GameObject.Find("Mage").transform;
        int portal = GameObject.Find("NumberPortal").GetComponent<PortalEnteredText>().portalCount;

        if (portal == 5 || portal == 10)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }

        disToPlayer = Vector2.Distance(transform.position, player.position);
        if (disToPlayer <= range && haveToFlip == false)
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
            haveToFlip = true;
        }

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0)
        {
            Destroy(gameObject);
            GameObject.Find("Spawn_Enemies").GetComponent<Spawn_Enemies>().enemyMin = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Staff" && PlayerController.normalAttack)
        {
            //Instantiate(boom, col.gameObject.transform.position, Quaternion.identity);
            damage = GameObject.Find("Mage").GetComponent<PlayerStat>().damageDealt(1);
            gameObject.GetComponent<EnemyStat>().TakeDamage(damage);
            Debug.Log("COLLIDE " + damage);
            //Debug.Log("Killed collide");
        }

        switch (col.gameObject.name)
        {
            case "Fireball(Clone)":
                //Instantiate(boom, col.gameObject.transform.position, Quaternion.identity);
                damage = GameObject.Find("Mage").GetComponent<PlayerStat>().damageDealt(2);
                gameObject.GetComponent<EnemyStat>().TakeDamage(damage);
                //Debug.Log("Killed collide");
                Debug.Log("COLLIDE " + damage);
                break;

        }


    }

    private void FixedUpdate()
    {
        if (mustPatrol == true)
        {
            mustTurn = !Physics2D.OverlapCircle(groundcheckPos.position, 0.1f, groundLayer);
            // Patrol();
            haveToFlip = false;
        }
    }

    //enemy move
    void Patrol()
    {
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
        if (mustTurn == true || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
            haveToFlip = true;
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

    //enemy die
    void Die()
    {
        Destroy(gameObject);
        GameObject.Find("Spawn_Enemies").GetComponent<Spawn_Enemies>().enemyMin -= 1;
    }
}