using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour, IDataPersistence
{
    [SerializeField] private Camera camera;
    [SerializeField] private Transform clockHandTransform;
    private Color newColor;
    private const float READ_SECONS_PER_INGAME_DAY = 100f;
    Color nightColor = new Color32(15, 61, 133, 0);
    Color backgroundColor = new Color32(120, 166, 238, 0);
    Color dayColor = new Color32(140, 205, 247, 0);
    private float day;
    float time;
    private void Update()
    {
        day += Time.deltaTime / READ_SECONS_PER_INGAME_DAY;
        float dayNormalized = day % 1f;

        float rotationDegreesPerDay = 360f;
//        clockHandTransform.eulerAngles = new Vector3(0, 0, - dayNormalized * rotationDegreesPerDay);
        time = (dayNormalized * rotationDegreesPerDay)/90f * 3;

        newColor = camera.backgroundColor;
        if(time < 6 && time > 0)
        {
            if(camera.backgroundColor.g < dayColor.g)
            {
                newColor.g += (dayNormalized / 2000f);
                if (camera.backgroundColor.r < dayColor.r)
                {
                    newColor.r += (dayNormalized / 2000f);

                }
                if (camera.backgroundColor.b < dayColor.b)
                {
                    newColor.b += (dayNormalized / 2000f);

                }
                camera.backgroundColor = newColor;

            }
            else 
            {
                camera.backgroundColor = dayColor;

            }

        }
        else if (time < 12 && time > 6)
        {
            if (camera.backgroundColor.g > nightColor.g)
            {
                newColor.r -= (dayNormalized / 2000f);
                newColor.g -= (dayNormalized / 2000f);
                newColor.b -= (dayNormalized / 2000f);
                camera.backgroundColor = newColor;

            }

        }

        
    }

    public void LoadData(GameData data)
    {
        time = data.time;
    }

    public void SaveData(ref GameData data)
    {
        data.time = time;
    }
}
