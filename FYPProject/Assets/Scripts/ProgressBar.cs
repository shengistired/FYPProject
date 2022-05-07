using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public int maximum;
    public int minumum;
    public int current;
    public Image mask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float currentOffSet = current - minumum;
        float maximumOffSet = maximum - minumum;
        float fillAmount = currentOffSet / maximumOffSet;
        mask.fillAmount = fillAmount;
    }
}
