using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingVolume : MonoBehaviour {

    public string targetSceneName;
    public bool useAsyncLoad = true;
    public bool stayLoaded = false;
    bool playerIsInVolume;

    private void Update()
    {
        if(playerIsInVolume)
        {
            ActiveSceneManager.LoadScene(targetSceneName, useAsyncLoad);
        }
        else
        {
            if(!stayLoaded)
            {
                ActiveSceneManager.UnloadScene(targetSceneName);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCharacter>() != null)
        {
            playerIsInVolume = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCharacter>() != null)
        {
            playerIsInVolume = false;
        }
    }
}
