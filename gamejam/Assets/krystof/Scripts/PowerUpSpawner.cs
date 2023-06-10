using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 _minSpawnPos;
    [SerializeField] private Vector3 _maxSpawnPos;
    [SerializeField] private float timeLeft;
    [SerializeField] private TextMeshProUGUI _timerUI;
    [SerializeField] private List<GameObject> _powerUps = new List<GameObject>();
    [SerializeField] public bool _canSpawn = true;

    [SerializeField] private GenerateText _generateText;

    public static PowerUpSpawner Instance = null;

    public UnityEvent StartWriting;
    public UnityEvent StartSpawning;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        StartWriting.AddListener(() =>
        {
            StopSpawning(); 
            ChangeBoolValue();
            _generateText.GenerateSentence();
        });

        StartSpawning.AddListener(() =>
        {
            ChangeBoolValue();
            StartCoroutine(SpawnPowerUp());
        });
    }

    private void Start()
    {
        StartCoroutine(SpawnPowerUp());
        StartCoroutine(Timer());
    }

    public IEnumerator SpawnPowerUp()
    {
        if (PowerUp._spawnedPowerUps.Count != 0)
        {
            foreach (var VARIABLE in PowerUp._spawnedPowerUps)
            { 
                VARIABLE.SetActive(true);
            }
        }

        while (timeLeft > 0 && _canSpawn)
        {
            yield return new WaitForSeconds(1.5f);
            if (_canSpawn) Instantiate(randomPowerUp() ,generateSpawnPos(), transform.rotation);
        }
    }

    IEnumerator Timer()
    {
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(1f);
            timeLeft--;
            _timerUI.text = timeLeft.ToString();
        }
    }

    public void StopSpawning()
    {
        foreach (var VARIABLE in PowerUp._spawnedPowerUps)
        {
            VARIABLE.SetActive(false);
        }
    }

    public void ChangeBoolValue()
    {
        _canSpawn = !_canSpawn;
    }

    GameObject randomPowerUp()
    {
        return _powerUps[Random.Range(0, _powerUps.Count)];
    }
    Vector3 generateSpawnPos()
    {
        return new Vector3(Random.Range(_minSpawnPos.x, _maxSpawnPos.x), _minSpawnPos.y);
    }
}
