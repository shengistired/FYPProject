using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    [HideInInspector]
    public int worldSize;

    private float orthoSize;

    public void Spawn(Vector3 pos)
    {
        GetComponent<Transform>().position = pos;
    }

    private void FixedUpdate()
    {


        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothPosition.x = Mathf.Clamp(desiredPosition.x, 12 + (orthoSize * 2), (worldSize - 11) - (orthoSize * 2.5f));
        transform.position = smoothPosition;


        // transform.LookAt(target);
    }
}
