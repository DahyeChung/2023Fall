﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveSceneManager : MonoBehaviour {

    static List<string> LoadedSceneNames = new List<string>();

    public static List<Scene> GetAllScenes()
    {
        List<Scene> retScenes = new List<Scene>();

        foreach(string sceneName in LoadedSceneNames)
        {
            Scene currScene = SceneManager.GetSceneByName(sceneName);
            retScenes.Add(currScene);
        }

        return retScenes;
    }

    public static bool IsSceneLoaded(string sceneName)
    {
        Scene currScene = SceneManager.GetSceneByName(sceneName);
        if(currScene.IsValid())
        {
            return currScene.isLoaded;
        }

        return false;
    }

	public static void LoadScene(string sceneName, bool useAsyncLoad)
    {
        if(!LoadedSceneNames.Contains(sceneName))
        {
            if (useAsyncLoad)
            {
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
            LoadedSceneNames.Add(sceneName);
        }
    }

    public static void UnloadScene(string sceneName)
    {
        if (LoadedSceneNames.Contains(sceneName))
        {
            SceneManager.UnloadSceneAsync(sceneName);
            
            LoadedSceneNames.Remove(sceneName);
        }
    }
}
