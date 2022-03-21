using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private GameObject staff;


    private Animator ani;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    public int rotationOffset = 0;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }


    public void attack()
    {
        ani.SetTrigger("Attack");
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                staff.SetActive(true);
            }
            else
            {
                staff.SetActive(false);
            }
        

        cooldownTimer += Time.deltaTime;


        cooldownTimer = 0;

        //object pooling
        fireballs[findFireball()].transform.position = firePoint.position;
        fireballs[findFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int findFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
