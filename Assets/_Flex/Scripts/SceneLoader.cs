﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneAsync(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    public void LoadSceneAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}