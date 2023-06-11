using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _clips = new List<AudioClip>();
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private Slider _sliderSFX;
    [SerializeField] private AudioMixer _audioMixerGroup;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject AttackerWinMenu;
    [SerializeField] private GameObject DefenderWinMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pauseMenu != null)
            {
                _pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) return;

        Orb.Instance.OnWin += OnWin;
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
    }

    public void LoadMainScene()
    {
        ResumeGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadMainGame()
    {
        SceneManager.LoadScene("marekScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene("Choose Char Scene");
    }

    public void PlayRandomSound()
    {
        _audioSource.clip = _clips[Random.Range(0, 3)];
        _audioSource.Play(1);
    }

    public void ChangeVolumeMusic()
    {
        _audioMixerGroup.SetFloat("Music", _sliderMusic.value);
    }

    public void ChangeVolumeSFX()
    {
        _audioMixerGroup.SetFloat("SFX", _sliderSFX.value);
    }
    
    private void PauseGame()
    {
        Time.timeScale = 0;
    }
    
    private void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private void OnWin(Player player)
    {
        PauseGame();
        if (player == TurnManager.Instance.Attacker)
        {
            AttackerWinMenu.SetActive(true);
            return;
        }
        DefenderWinMenu.SetActive(true);
    }

}
