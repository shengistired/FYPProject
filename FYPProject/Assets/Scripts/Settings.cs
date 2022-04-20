using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] TMP_Dropdown graphic;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1/4);
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
        AudioListener.volume = volumeSlider.value/4;
        Save();
    }

    private void Load()
    {
        
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume") * 4;
        AudioListener.volume = volumeSlider.value/4;

    }
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value/4);
    }

    public void SetQuality(int i)
    {
        QualitySettings.SetQualityLevel(i);
        PlayerPrefs.SetInt("QualityLevel", i);
    }

}
