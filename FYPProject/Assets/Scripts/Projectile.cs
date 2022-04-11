using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private bool hit;
    private float direction;
    private float lifeTime;

    private BoxCollider2D boxCollider;
    private Animator ani;

    private void Start()
    {


        ani = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }

       private void Update()
       {
           if (hit) return;

           lifeTime += Time.deltaTime;
           if (lifeTime > 5)
           {
               Destroy(gameObject);
           }
       }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.GetComponent<EnemyHealth>() != null)
        {
            collisionGameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        }*/

        hit = true;
        Debug.Log("BOOM");
        ani.SetTrigger("explode");
        Destroy(gameObject);
    }


    private void Deactivate()
    {
        Destroy(gameObject);
    }

}
