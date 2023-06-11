using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _clips = new List<AudioClip>();
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private Slider _sliderSFX;
    [SerializeField] private AudioMixer _audioMixerGroup;
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
        _audioMixerGroup.SetFloat("Music" ,_sliderMusic.value);
    }
    public void ChangeVolumeSFX()
    {
        _audioMixerGroup.SetFloat("SFX" ,_sliderSFX.value);
    }
}
