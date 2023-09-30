using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateTextScript : MonoBehaviour {

    Text myText;
    public PlayerMovementScript targetPlayer;

	// Use this for initialization
	void Start () {
        myText = GetComponent<Text>();	
	}
	
	// Update is called once per frame
	void Update () {
        myText.text = "Player State: " + targetPlayer.GetStateName() + "\n" +
            "Distance to Ground: " + targetPlayer.GetDistanceToGround().ToString();
	}
}
