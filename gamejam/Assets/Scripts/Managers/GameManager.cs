using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void SetInstance()
    {
        if (Instance == null) Instance = this;
        else Debug.LogError($"[GameManager] There can be only one instance of this object (object: {gameObject.name})");
    }
    #endregion

    private void Awake()
    {
        SetInstance();
    }

    public void LoadNextScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(sceneIndex);
    }

    public bool IsInFirstScene()
    {
        return SceneManager.GetActiveScene().buildIndex == 0;
    }
}
