using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPHUD : MonoBehaviour {

    public PlayerActionScript playerActionScript;
    Text hudText;

	// Use this for initialization
	void Start () {
        hudText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        hudText.text = "MP: " + playerActionScript.GetCurrentMP() + "/" + playerActionScript.MaxMP;
    }
}
