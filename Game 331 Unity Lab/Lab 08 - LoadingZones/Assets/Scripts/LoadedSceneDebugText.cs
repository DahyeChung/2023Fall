using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Profiling;

public class LoadedSceneDebugText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        string outputString = "STATS:\n";

        //Object Count
        outputString += "Object Count: ";
        outputString += GameObject.FindObjectsOfType(typeof(GameObject)).Length.ToString();
        outputString += "\n";

        //List Scenes
        outputString += "Current Scenes:\n";
        List<Scene> sceneList = ActiveSceneManager.GetAllScenes();
        foreach(Scene scene in sceneList)
        {
            outputString += scene.name;
            outputString += ": ";
            outputString += scene.isLoaded ? "Loaded!" : "Loading...";
            outputString += "\n";
        }

        GetComponent<Text>().text = outputString;
	}
}
