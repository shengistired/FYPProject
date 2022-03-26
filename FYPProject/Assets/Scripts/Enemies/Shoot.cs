using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float walkSpeed, range, timeBTWshots, shootSpeed;
    private float disToPlayer;

    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn , canShoot;

    public Rigidbody2D rb;
    public Transform groundcheckPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public Transform player, shootPos;
    public GameObject bullet;
    //public GameObject boom;

    void Start()
    {
        mustPatrol = true;
        Physics2D.IgnoreLayerCollision(7, 7, true);
        player = GameObject.Find("Mage").transform;
        canShoot = true;
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

            mustPatrol = false;
            rb.velocity = Vector2.zero;

            if(canShoot)
            {
                StartCoroutine(Shot());
            }
        }
        else
        {
            mustPatrol = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.name)
        {
            case "Fireball(Clone)":
                //EnemySpawn.spawnAllowed = false;
                //Instantiate(boom, col.gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
                Debug.Log("Killed");
                break;
        }
    }

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

    IEnumerator Shot()
     {
         canShoot = false;

         yield return new WaitForSeconds(timeBTWshots);
         GameObject newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);

         newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * walkSpeed * Time.fixedDeltaTime, 0f);
         canShoot = true;
     }
}