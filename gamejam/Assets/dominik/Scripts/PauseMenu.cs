using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random=UnityEngine.Random;


public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused;
    [SerializeField] private GameObject PauseMenuGO;
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button SettingsButton;
    [SerializeField] private Button MenuButton;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _clips = new List<AudioClip>();

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

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game Scene");
    }
    
    public void PlayRandomSound()
    {
        _audioSource.clip = _clips[Random.Range(0, 3)];
        _audioSource.Play(1);
    }
}
