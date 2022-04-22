using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Craft : MonoBehaviour
{
    public void craft_Position()
    {
        Vector3 centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.77f, 0.55f, 10f));
        transform.position = centerPos;

    }
}
