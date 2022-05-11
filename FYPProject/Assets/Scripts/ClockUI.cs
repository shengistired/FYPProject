using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{
    [SerializeField] private Camera camera;
    private Color newColor;
    private const float READ_SECONS_PER_INGAME_DAY = 500f;
    private Transform clockHandTransform;
    Color nightColor = new Color32(24, 44, 77, 0);
    Color backgroundColor = new Color32(120, 166, 238, 0);
    private float day;
    private void Awake()
    {
        clockHandTransform = transform.Find("clockHand");

    }
    private void Update()
    {
        day += Time.deltaTime / READ_SECONS_PER_INGAME_DAY;
        float dayNormalized = day % 1f;

        float rotationDegreesPerDay = 360f;
        clockHandTransform.eulerAngles = new Vector3(0, 0, - dayNormalized * rotationDegreesPerDay);
        Debug.Log(dayNormalized);
        newColor = camera.backgroundColor;
        newColor.r -= (dayNormalized/500f);
        newColor.g -= (dayNormalized / 500f);
        newColor.b -= (dayNormalized / 500f);
        camera.backgroundColor = newColor;
        
    }
}
