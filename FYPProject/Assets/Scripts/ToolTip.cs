using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolTip: MonoBehaviour
{
    private static ToolTip instance;
    private Text tooltipText;
    private RectTransform backgroundRectTransform;
    private void Awake()
    {
        instance = this;
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        tooltipText = backgroundRectTransform.Find("text").GetComponent<Text>();

        gameObject.SetActive(false);
    }
    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
        transform.localPosition = localPoint;
        //backgroundRectTransform.position =  Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, 0f, 10f));

    }
    private void ShowToolTip(string tooltipString)
    {
        gameObject.SetActive(true);

        tooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f, tooltipText.preferredHeight + textPaddingSize * 2f);
        backgroundRectTransform.sizeDelta = backgroundSize;
    }

    private void HideToolTip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowToolTip_Static(string tooltipString)
    {
        instance.ShowToolTip(tooltipString);
    }
    public static void HideToolTip_Static()
    {
        instance.HideToolTip();
    }
}
