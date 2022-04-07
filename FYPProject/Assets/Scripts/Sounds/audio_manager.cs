using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_manager : MonoBehaviour
{
    public AudioSource forest_music;
    public AudioSource desert_music;
    public AudioSource snow_music;

    public AudioClip forest_BGM_clip;
    public AudioClip desert_BGM_clip;
    public AudioClip snow_BGM_clip;


    //public string biome;


    // Start is called before the first frame update
    void Start()
    {
        forest_music = gameObject.AddComponent<AudioSource>();
        desert_music = gameObject.AddComponent<AudioSource>();
        snow_music = gameObject.AddComponent<AudioSource>();

        forest_music.playOnAwake = false;
        desert_music.playOnAwake = false;
        snow_music.playOnAwake = false;

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void forest_music_play()
    {
        desert_music.Stop();
        snow_music.Stop();

        forest_music.clip = forest_BGM_clip;
        forest_music.Play();
        forest_music.loop = true;




    }

    public void desert_music_play()
    {

        forest_music.Stop();
        snow_music.Stop();

        desert_music.clip = desert_BGM_clip;
        desert_music.Play();
        desert_music.loop = true;

    }

    public void snow_music_play()
    {
        forest_music.Stop();
        desert_music.Stop();

        snow_music.clip = snow_BGM_clip;
        snow_music.Play();
        snow_music.loop = true;

    }
}
