using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 _minSpawnPos;
    [SerializeField] private Vector3 _maxSpawnPos;
    [SerializeField] private float timeLeft;
    [SerializeField] private TextMeshProUGUI _timerUI;
    [SerializeField] private List<GameObject> _powerUps = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(spawnPowerUp());
    }

    IEnumerator spawnPowerUp()
    {
        while (timeLeft >= 0)
        {
            yield return new WaitForSeconds(1.5f);
            Instantiate(randomPowerUp() ,generateSpawnPos(), transform.rotation);
        }
    }

    IEnumerator Timer()
    {
        while (timeLeft >= 0)
        {
            yield return new WaitForSeconds(1f);
            timeLeft--;
            _timerUI.text = timeLeft.ToString();
        }
    }

    GameObject randomPowerUp()
    {
        return _powerUps[Random.Range(0, _powerUps.Count - 1)];
    }
    Vector3 generateSpawnPos()
    {
        return new Vector3(Random.Range(_minSpawnPos.x, _maxSpawnPos.x), _minSpawnPos.y);
    }
}
