using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;
using Random = UnityEngine.Random;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _powerUps = new List<GameObject>();
    [SerializeField] public bool _canSpawn = true;

    [SerializeField] private Challenges _questSystem;

    public static PowerUpSpawner Instance;

    private void SetInstance()
    {
        if (Instance == null) Instance = this;
        else Debug.LogError($"[PowerUpSpawner] There can be only one instance of this object (object: {gameObject.name})");
    }

    public UnityEvent StartQuest;
    public UnityEvent StartSpawning;
    public UnityEvent OnTimerExpire;

    [SerializeField] private Transform _powerUpParent;

    private void Awake()
    {
        SetInstance();

        StartQuest.AddListener(() =>
        {
            StopSpawning(); 
            EnableSpawning(false);
            _questSystem.StartRandomChallenge();
        });

        StartSpawning.AddListener(() =>
        {
            EnableSpawning(true);
            StartCoroutine(SpawnPowerUp());
            PowerUp.SelectedPowerUp.ClaimPowerUp();
            _questSystem.Close(); 
        });

        OnTimerExpire.AddListener(() =>
        {
            EnableSpawning(true);
            _questSystem.Close();
        });
    }

    public void StartSpawningPowerUps()
    {
        StartCoroutine(SpawnPowerUp());
    }

    public IEnumerator SpawnPowerUp()
    {
        if (PowerUp._spawnedPowerUps.Count != 0)
        {
            foreach (GameObject powerUp in PowerUp._spawnedPowerUps)
            { 
                powerUp.SetActive(true);
            }
        }

        Debug.Log(TurnManager.Instance.CurrentPlayer.Time + " " + _canSpawn);
        while (TurnManager.Instance.CurrentPlayer.Time > 0 && _canSpawn)
        {
            yield return new WaitForSeconds(1.5f);
            Debug.Log(_canSpawn);
            if (_canSpawn) Instantiate(GetRandomPowerUp(), _powerUpParent.position, Quaternion.identity);
        }
    }

    public void StopSpawning()
    {
        foreach (GameObject powerUp in PowerUp._spawnedPowerUps)
        {
            powerUp.SetActive(false);
        }
    }

    private void EnableSpawning(bool enable) => _canSpawn = enable;

    GameObject GetRandomPowerUp()
    {
        return _powerUps[Random.Range(0, _powerUps.Count)];
    }
}
