using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{

    void Update()
    {
        Vector3 positioning = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0, 10f));
        transform.position = positioning;
    }
}
