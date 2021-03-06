using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHB : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;

    public void setHealth(float health, float maxHealth)
    {
        slider.gameObject.SetActive(health < maxHealth);
        slider.value = health;
        slider.maxValue = maxHealth;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }

    void Update()
    {
        Vector3 offset = new Vector3(0f, 1.4f, 0f);
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}