using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float walkSpeed, range, timeBTWshots, shootSpeed, damage;
    private float disToPlayer;
    
    //public EnterPortal EnterPortal;

    [HideInInspector]
    public bool mustPatrol, haveToFlip;
    private bool mustTurn, canShoot;
    //public int lvl;

    public Rigidbody2D rb;
    public Transform groundcheckPos;
    public Transform player, shootPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public GameObject bullet;
    //public GameObject boom;

    public PortalEnteredText portalEnteredText;


    void Start()
    {
        mustPatrol = true;
        canShoot = true;
        haveToFlip = false;
        Physics2D.IgnoreLayerCollision(7, 7, true);
        //EnemyHealthBar.SetActive(false);

        player = GameObject.Find("Mage").transform;

        if (portalEnteredText.portalCount == 5 || portalEnteredText.portalCount == 10)
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
        //lvl += EnterPortal.portalsEntered;
        //Debug.Log(EnterPortal.portalsEntered);

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

            mustPatrol = false;
            rb.velocity = Vector2.zero;

            if (canShoot)
            {
                StartCoroutine(Shot());
            }
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
            GameObject.Find("Spawn_Enemies").GetComponent<Spawn_Enemies>().enemyMin -= 1;
            Debug.Log("Invisible");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Staff" && PlayerController.normalAttack)
        {
            //Instantiate(boom, col.gameObject.transform.position, Quaternion.identity);
            gameObject.GetComponent<EnemyStat>().TakeDamage(damage);
            Debug.Log("Killed collide");
        }
        switch (col.gameObject.name)
        {
            case "Fireball(Clone)":
                //Instantiate(boom, col.gameObject.transform.position, Quaternion.identity);
                gameObject.GetComponent<EnemyStat>().TakeDamage(damage);
                Debug.Log("Killed shoot");

            break;

        
    }
    }

    private void FixedUpdate()
    {
        if (mustPatrol == true)
        {
            mustTurn = !Physics2D.OverlapCircle(groundcheckPos.position, 0.1f, groundLayer);
            //Patrol();
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

    //enemy die
    void Die()
    {
        Destroy(gameObject);
        GameObject.Find("Spawn_Enemies").GetComponent<Spawn_Enemies>().enemyMin -= 1;
        Debug.Log("Die");
    }
}