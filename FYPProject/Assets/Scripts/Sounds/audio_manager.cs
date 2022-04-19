using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_manager : MonoBehaviour
{
    [Header("Background Music")]
    public AudioSource forest_music;
    public AudioSource desert_music;
    public AudioSource snow_music;
    public AudioSource miniBoss_music;
    public AudioClip forest_BGM_clip;
    public AudioClip desert_BGM_clip;
    public AudioClip snow_BGM_clip;
    public AudioClip miniBoss_BGM_clip;

    [Header("Sound Effects")]
    public AudioSource walk;
    public AudioSource hit;
    public AudioSource chop;
    public AudioSource mining;
    public AudioSource swordHit;
    public AudioSource fireBall;
    public AudioSource hurt;
    public AudioSource portal;
    public AudioClip walk_effect_clip;
    public AudioClip hit_effect_clip;
    public AudioClip chop_effect_clip;
    public AudioClip mining_effect_clip;
    public AudioClip swordHit_effect_clip;
    public AudioClip fireBall_effect_clip;
    public AudioClip hurt_effect_clip;
    public AudioClip portal_effect_clip;


    //public string biome;


    // Start is called before the first frame update
    void Start()
    {
        //bgm
        forest_music = gameObject.AddComponent<AudioSource>();
        desert_music = gameObject.AddComponent<AudioSource>();
        snow_music = gameObject.AddComponent<AudioSource>();
        miniBoss_music = gameObject.AddComponent<AudioSource>();

        forest_music.playOnAwake = false;
        desert_music.playOnAwake = false;
        snow_music.playOnAwake = false;
        miniBoss_music.playOnAwake = false;

        //sound effects
        walk = gameObject.AddComponent<AudioSource>(); ;
        hit = gameObject.AddComponent<AudioSource>(); ;
        chop = gameObject.AddComponent<AudioSource>(); ;
        mining = gameObject.AddComponent<AudioSource>(); ;
        swordHit = gameObject.AddComponent<AudioSource>(); ;
        fireBall = gameObject.AddComponent<AudioSource>(); ;
        hurt = gameObject.AddComponent<AudioSource>(); ;
        portal = gameObject.AddComponent<AudioSource>(); ;

        walk.playOnAwake = false;
        hit.playOnAwake = false;
        chop.playOnAwake = false;
        mining.playOnAwake = false;
        swordHit.playOnAwake = false;
        fireBall.playOnAwake = false;
        hurt.playOnAwake = false;
        portal.playOnAwake = false;



    }

    // Update is called once per frame
    void Update()
    {


    }
    //sound effects
    public void hitTag()
    {
        if (hit_effect_clip.length == 0)
        {
            print("no clip found."); return;
        }

        hit.clip = hit_effect_clip;
        hit.Play();

    }

    public void fireBall_play()
    {
        // if (fireBall_effect_clip.length == 0)
        // {
        //     print("no clip found."); return;
        // }

        fireBall.clip = fireBall_effect_clip;
        fireBall.Play();
        

    }







    //BGMs
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

    public void miniBoss_music_play()
    {
        forest_music.Stop();
        desert_music.Stop();
        snow_music.Stop();

        miniBoss_music.clip = miniBoss_BGM_clip;
        miniBoss_music.Play();
        miniBoss_music.loop = true;

    }
}
