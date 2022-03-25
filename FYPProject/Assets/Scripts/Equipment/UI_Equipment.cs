using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Equipment : MonoBehaviour
{
    // [SerializeField] private Transform equipmentSlotContainer;
    // [SerializeField] private Transform equipmentSlotTemplate;


    // Start is called before the first frame update
    public void equipment_Position()
    {
        
        Vector3 leftBottomPos = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 10f));
        transform.position = leftBottomPos;

    }

}
