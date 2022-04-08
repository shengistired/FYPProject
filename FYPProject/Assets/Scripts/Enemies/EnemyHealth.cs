using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float startHealth;

    [HideInInspector]
    public static float hp;

    public GameObject diePEffect;

    // Start is called before the first frame update
    void Start()
    {
        hp = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage (float damage)
    {
        hp -= damage;

        if(hp <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if(diePEffect != null)
        {
            Instantiate(diePEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
        GameObject.Find("Spawn_Shoot").GetComponent<Spawn_Shoot>().enemyMin -= 1;
    }
}
