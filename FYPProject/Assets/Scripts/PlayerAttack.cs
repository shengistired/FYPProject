using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int directionNum;
    [SerializeField] private GameObject staff;


    public int rotationOffset = 0;
    [SerializeField] private GameObject fireball;


    public void attack()
    {

        staff.SetActive(true);

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10f) - transform.position);
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Quaternion quan = Quaternion.Euler(0f, 0f, 180);

        if(PlayerMovement.directionNum == 1)
        {
            Instantiate(fireball, transform.position, Quaternion.identity);

        }
        else
        {
            Instantiate(fireball, transform.position, quan);

        }
    }


}
