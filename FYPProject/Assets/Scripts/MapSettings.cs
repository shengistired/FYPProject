using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MapSettings : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider soundSlider;
    [SerializeField] TMP_Dropdown graphic;
    public AudioSource[] allBGM;
    public AudioSource[] allsoundEffect;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1/3);
            Load();
        }
        else
        {
            Load();
        }
        if (!PlayerPrefs.HasKey("soundEffect"))
        {
            PlayerPrefs.SetFloat("soundEffect", 1 / 4);
            Load();
        }
        else
        {
            Load();
        }

        if (!PlayerPrefs.HasKey("QualityLevel"))
        {
            graphic.value = 4;
        }
        else
        {
            graphic.value = PlayerPrefs.GetInt("QualityLevel");
        }
    }

    public void changeVolume()
    {
        for(int i = 0; i < allBGM.Length; i++)
        {
            allBGM[i].volume = volumeSlider.value / 3;
        }
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value / 3);
    }
    public void changeSoundEffect()
    {
        for(int i = 0; i < allsoundEffect.Length; i++)
        {
            allsoundEffect[i].volume = soundSlider.value/4;
        }
        PlayerPrefs.SetFloat("soundEffect", soundSlider.value / 4);
    }

    public void Load()
    {
        
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume") * 3;
        soundSlider.value = PlayerPrefs.GetFloat("soundEffect") * 4;
        for (int i = 0; i < allBGM.Length; i++)
        {
            allBGM[i].volume = volumeSlider.value / 3;
        }
        for (int i = 0; i < allsoundEffect.Length; i++)
        {
            allsoundEffect[i].volume = soundSlider.value / 4;
        }

    }

    public void SetQuality(int i)
    {
        QualitySettings.SetQualityLevel(i);
        PlayerPrefs.SetInt("QualityLevel", i);
    }

}
