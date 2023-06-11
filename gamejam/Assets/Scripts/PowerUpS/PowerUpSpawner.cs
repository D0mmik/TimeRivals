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
    [SerializeField] public bool CanSpawn = true;
    [SerializeField] public bool TimeExpired;

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
            TimeExpired = true;
            StopSpawning();
            EnableSpawning(true);
            _questSystem.Close();
            DestroyPowerUps();
        });
    }

    public void StartSpawningPowerUps()
    {
        TimeExpired = false;
        StartCoroutine(SpawnPowerUp());
    }

    public IEnumerator SpawnPowerUp()
    {
        Debug.Log(PowerUp.SpawnedPowerUps.Count);
        if (PowerUp.SpawnedPowerUps.Count != 0)
        {
            foreach (GameObject powerUp in PowerUp.SpawnedPowerUps)
            { 
                powerUp.SetActive(true);
            }
        }

        while (TurnManager.Instance.CurrentPlayer.Time > 0 && CanSpawn)
        {
            yield return new WaitForSeconds(1.5f);
            if (CanSpawn && !TimeExpired) Instantiate(GetRandomPowerUp(), _powerUpParent.position, Quaternion.identity);
        }
    }

    private void DestroyPowerUps()
    {
        List<GameObject> spawnedPowerUps = PowerUp.SpawnedPowerUps;
        while (spawnedPowerUps.Count > 0)
        {
            GameObject powerUp = spawnedPowerUps[0];
            spawnedPowerUps.Remove(powerUp);
            Destroy(powerUp);
        }
    }

    public void StopSpawning()
    {
        foreach (GameObject powerUp in PowerUp.SpawnedPowerUps)
        {
            powerUp.SetActive(false);
        }
    }

    private void EnableSpawning(bool enable) => CanSpawn = enable;

    GameObject GetRandomPowerUp()
    {
        return _powerUps[Random.Range(0, _powerUps.Count)];
    }
}
