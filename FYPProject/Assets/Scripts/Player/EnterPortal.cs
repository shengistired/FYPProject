using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterPortal : MonoBehaviour
{
    [SerializeField]
    private string nextLevel ="Map";
    public int portalsEntered;
    private void Start()
    {
        try
        {
            portalsEntered = SaveData.current.portalEntered;

        }
        catch
        {
            portalsEntered = 0;
        }



    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextLevel);
            portalsEntered++;
            SaveData.current.portalEntered = portalsEntered;
        }

    }

}
