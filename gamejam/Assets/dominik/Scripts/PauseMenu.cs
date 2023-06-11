using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused;
    [SerializeField] private GameObject PauseMenuGO;
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button SettingsButton;
    [SerializeField] private Button MenuButton;

    private void Start()
    {
        ResumeButton.onClick.AddListener(Resume);
        SettingsButton.onClick.AddListener(OpenSettings);
        MenuButton.onClick.AddListener(OpenMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) Resume();
            else Pause();
        }
    }

    private void Pause()
    {
        GameIsPaused = true;
        Time.timeScale = 0;
        PauseMenuGO.SetActive(true);
    }

    private void Resume()
    {
        GameIsPaused = false;
        Time.timeScale = 1;
        PauseMenuGO.SetActive(false);
    }
    private void OpenSettings()
    {
        Debug.Log("Settings");
    }
    private void OpenMenu()
    {
        SceneManager.LoadScene(0);
    }
}
