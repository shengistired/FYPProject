using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MapSettings : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider soundSlider;
    [SerializeField] TMP_Dropdown graphic;
    public AudioSource soundEffect;
    public AudioSource clickEffect;
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

        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value / 3);
    }
    public void changeSoundEffect()
    {

        soundEffect.volume = soundSlider.value / 3;
        clickEffect.volume = soundSlider.value / 4;

        PlayerPrefs.SetFloat("soundEffect", soundSlider.value / 4);
    }

    public void Load()
    {
        
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume") * 3;
        soundSlider.value = PlayerPrefs.GetFloat("soundEffect") * 4;
        soundEffect.volume = soundSlider.value / 4;
        clickEffect.volume = soundSlider.value / 4;

    }
    private void Save()
    {
    }

    public void SetQuality(int i)
    {
        QualitySettings.SetQualityLevel(i);
        PlayerPrefs.SetInt("QualityLevel", i);
    }

}
