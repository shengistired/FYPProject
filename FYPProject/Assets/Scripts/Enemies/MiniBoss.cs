using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    private Animator animator;
    public float walkSpeed, range, timeBTWshots, shootSpeed, damage, distance, stop;
    private float disToPlayer;

    [HideInInspector]
    public bool mustPatrol, haveToFlip;
    private bool mustTurn, canShoot;

    public Rigidbody2D rb;
    public Transform groundcheckPos;
    public Transform player, shootPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public GameObject bullet;

    public PortalEnteredText portalEnteredText;

    private Vector2 target;

    private Material matWhite;
    private Material matRed;
    private Material matDefault;
    SpriteRenderer sr;

    void Start()
    {
        mustPatrol = true;
        canShoot = true;
        haveToFlip = false;
        //Physics2D.IgnoreLayerCollision(7, 7, true);
        animator = GetComponent<Animator>();
        player = GameObject.Find("Mage").transform;
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matRed = Resources.Load("RedFlash", typeof(Material)) as Material;
        matDefault = sr.material;

        if (portalEnteredText.portalCount == 5 || portalEnteredText.portalCount == 10)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }

        target = new Vector2(0.0f, 0.0f);
    }

    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }

        disToPlayer = Vector2.Distance(transform.position, player.position);
        if (disToPlayer >= distance)
        {
            if (player.position.x > transform.position.x && transform.localScale.x < 0
                   || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }

            if (disToPlayer <= range && haveToFlip == false)
            {
                //mustPatrol = false;
                //rb.velocity = Vector2.zero;

                if (canShoot)
                {
                    animator.SetTrigger("Attack");
                    StartCoroutine(Shot());
                }
            }
        }

        if(disToPlayer <= stop)
        {
            rb.AddForce(Vector2.up * 10f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Staff" && PlayerController.normalAttack)
        {
            damage = GameObject.Find("Mage").GetComponent<PlayerStat>().damageDealt(1);
            gameObject.GetComponent<MiniBossStats>().TakeDamage(damage);
            Debug.Log("BOSS " + damage);
        }

        switch (col.gameObject.name)
        {
            case "Fireball(Clone)":
                damage = GameObject.Find("Mage").GetComponent<PlayerStat>().damageDealt(2);
                gameObject.GetComponent<MiniBossStats>().TakeDamage(damage);
                Debug.Log("BOSS " + damage);

                for (int i = 0; i < 2; i++)
                {
                    sr.material = matRed;
                    Invoke("ResetMaterial", .07f);
                }
                    
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collide)
    {
        if (collide.gameObject.name == "Mage")
        {
            if(transform.localScale.x > 0) //right
            {
                sr.material = matWhite;
                Invoke("ResetMaterial", .05f);
                transform.localScale += new Vector3(0.1f, 0.1f, 0);
                Debug.Log("RIGHT");
            }
            if (transform.localScale.x < 0) //left
            {
                sr.material = matWhite;
                Invoke("ResetMaterial", .05f);
                transform.localScale += new Vector3(-0.1f, 0.1f, 0);
                Debug.Log("LEFT");
            }
                
        }
    }

    void ResetMaterial()
    {
        sr.material = matDefault;
    }

    private void FixedUpdate()
    {
        if (mustPatrol == true)
        {
            //mustTurn = !Physics2D.OverlapCircle(groundcheckPos.position, 0.1f, groundLayer);
            haveToFlip = false;
        }
    }

    //enemy move
    void Patrol()
    {
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
        if (mustTurn == true || bodyCollider.IsTouchingLayers(groundLayer))
        {
            //Flip();
            haveToFlip = true;
            rb.AddForce(Vector2.up * 23f);
        }
    }

    //enemy flip
    void Flip()
    {
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

        if (gameObject.transform.localScale.x > 0)
        {
            newBullet.transform.localScale = new Vector2(newBullet.transform.localScale.x * -1, newBullet.transform.localScale.y);
        }

        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * walkSpeed * Time.fixedDeltaTime, 0f);

        canShoot = true;
       
    }

    //enemy die
    void Die()
    {
        Destroy(gameObject);
        GameObject.Find("Spawn_Enemies").GetComponent<Spawn_Enemies>().enemyMin -= 1;
    }
}