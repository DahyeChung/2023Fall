using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlaneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        PlayerMovementScript playerMovement = collision.gameObject.GetComponent<PlayerMovementScript>();
        if(playerMovement != null)
        {
            playerMovement.ResetToStartingPosition();
        }
    }
}
