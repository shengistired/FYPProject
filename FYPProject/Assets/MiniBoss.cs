using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    public float walkSpeed, range, timeBTWshots, shootSpeed, stop, damage;
    private float disToPlayer;

    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn, canShoot;
    //public int enemyMin;

    public Rigidbody2D rb;
    public Transform groundcheckPos;
    public Transform player, shootPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public GameObject bullet;
    //public GameObject boom;

    //health
    public float hitPts;
    public float maxHitPts = 20;
    public EnemyHB healthBar;


    void Start()
    {
        mustPatrol = true;
        canShoot = true;
        Physics2D.IgnoreLayerCollision(7, 7, true);
        //EnemyHealthBar.SetActive(false);

        player = GameObject.Find("Mage").transform;
        //enemyMin = GameObject.Find("Spawn_Shoot").GetComponent<Spawn_Shoot>().enemyMin;

        hitPts = maxHitPts;
        healthBar.setHealth(hitPts, maxHitPts);
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

            if (disToPlayer <= stop)
            {
                mustPatrol = false;
                rb.velocity = Vector2.zero;
            }

            if (canShoot)
            {
                StartCoroutine(Shot());
            }
        }
        else
        {
            mustPatrol = true;
        }

        /*Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0)
        {
            Destroy(gameObject);
            GameObject.Find("Spawn_Shoot").GetComponent<Spawn_Shoot>().enemyMin -= 1;
            Debug.Log("Invisible");
        }*/
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.name)
        {
            case "Fireball(Clone)":
                //Instantiate(boom, col.gameObject.transform.position, Quaternion.identity);
                TakeDamage(damage);
                //GameObject.Find("Spawn_Shoot").GetComponent<Spawn_Shoot>().enemyMin -= 1;
                Debug.Log("Killed shoot");
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

    //shooting
    IEnumerator Shot()
    {
        canShoot = false;

        yield return new WaitForSeconds(timeBTWshots);
        GameObject newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);
        //newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * walkSpeed * Time.fixedDeltaTime, 0f);

        if (gameObject.transform.localScale.x < 0)
        {
            newBullet.transform.localScale = new Vector2(newBullet.transform.localScale.x * -1, newBullet.transform.localScale.y);
        }

        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * walkSpeed * Time.fixedDeltaTime, 0f);

        canShoot = true;
    }

    public void TakeDamage(float damage)
    {
        hitPts -= damage;
        healthBar.setHealth(hitPts, maxHitPts);
        if (hitPts <= 0)
        {
            Destroy(gameObject);
        }
    }
}