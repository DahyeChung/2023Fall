using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingDoor : MonoBehaviour {

    bool playerWithinRange = false;
    bool openRequested = false;

    public GameObject doorMesh;
    public float doorMovementRate = 3.0f;

    public string mapToLoad = "";
    public string mapToUnload = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (playerWithinRange) 
        {
            if (Input.GetButton("Fire1"))
            {
                openRequested = true;
                if(mapToLoad != "")
                {
                    ActiveSceneManager.LoadScene(mapToLoad, true);
                }
                if(mapToUnload != "")
                {
                    ActiveSceneManager.UnloadScene(mapToUnload);
                }
            }
        }

        bool shouldBeOpen = false;
        if(openRequested && (mapToLoad == "" || ActiveSceneManager.IsSceneLoaded(mapToLoad)))
        {
            shouldBeOpen = true;
        }

        if(doorMesh != null)
        {
            float targetLocalY = shouldBeOpen ? -6.0f : 1.5f;
            Vector3 targetLocalPos = new Vector3(doorMesh.transform.localPosition.x, targetLocalY, doorMesh.transform.localPosition.z);

            doorMesh.transform.localPosition += (targetLocalPos - doorMesh.transform.localPosition) * Time.deltaTime * doorMovementRate;
        }
        
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCharacter>() != null)
        {
            playerWithinRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerCharacter>() != null)
        {
            playerWithinRange = false;
            openRequested = false;
        }
    }
}
