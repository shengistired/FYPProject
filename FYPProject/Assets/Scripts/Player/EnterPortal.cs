using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterPortal : MonoBehaviour
{
    [SerializeField]
    private string nextLevel = "Map";
    public PortalEnteredText portalEnteredText;
    public audio_manager music;
    [HideInInspector]
    public static bool sceneLoaded = false;

    private void Awake()
    {
        music = GetComponent<audio_manager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sceneLoaded = true;
            //EnemyStat.lvl += 1;
            //SceneManager.LoadScene("Map", LoadSceneMode.Single);
            music.portal_Play();
            LevelLoader.Instance.LoadLevel("Map");
            
            //LevelLoader.Instance.LoadLevel(nextLevel);

            portalEnteredText.OnPortalEnter();
            

        }

    }
    

}
