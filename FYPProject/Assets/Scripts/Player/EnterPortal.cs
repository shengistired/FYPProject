using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterPortal : MonoBehaviour
{
    [SerializeField]
    private string nextLevel ="Map";
    public PortalEnteredText portalEnteredText;

    [HideInInspector]
    public static bool sceneLoaded = false;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sceneLoaded = true;
            //EnemyStat.lvl += 1;
            SceneManager.LoadScene(nextLevel);
            portalEnteredText.OnPortalEnter();

        }

    }


}
