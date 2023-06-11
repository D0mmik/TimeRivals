using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameManager _settingsPrefab;
    [SerializeField] private Transform _settingsSpawn;
    public void Quit()
    {
        Application.Quit();
    }

    public void Settings()
    {
        Instantiate(_settingsPrefab, _settingsSpawn);
    }
}
