using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private Sprite _image;
    [SerializeField] private float _minFallingSpeed;
    [SerializeField] private float _maxFallingSpeed;
    [SerializeField] private float _sidePushSpeed;
    [SerializeField] private Char[] _chars = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p','q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
    [SerializeField] public static List<GameObject> SpawnedPowerUps = new List<GameObject>();
    private string _char;
    public static PowerUp SelectedPowerUp;

    enum TypeOfAction
    {
        Advantage,
        Attack,
        SabotageNextRound,
        AdvantageNextRound
    }

    [SerializeField] private TypeOfAction _typeOfAction;
    
    private void Awake()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1,2) * _sidePushSpeed ,Random.Range(_minFallingSpeed, _maxFallingSpeed)));
        _char = _chars[Random.Range(0, _chars.Length)].ToString().ToUpper();
        gameObject.GetComponentInChildren<TextMeshPro>().text = _char;
        SpawnedPowerUps.Add(gameObject);
    }

    private void OnEnable()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1,2) * _sidePushSpeed,Random.Range(_minFallingSpeed, _maxFallingSpeed)));
    }

    private void Update()
    {
        if (Input.GetKeyDown(_char.ToLower()))
        {
           PowerUpSpawner.Instance.StartQuest?.Invoke();
           SelectedPowerUp = this;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Border"))
        {
            SpawnedPowerUps.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public void ClaimPowerUp()
    {
        switch (_typeOfAction)
        {
            case TypeOfAction.Advantage:
                // Advantage();
                PowerUpHandler.Instance.AddTime();
                SpawnedPowerUps.Remove(gameObject);
                Destroy(gameObject);
                break;
            case TypeOfAction.Attack:
                TakeDamage();
                SpawnedPowerUps.Remove(gameObject);
                Destroy(gameObject);
                break;
            case TypeOfAction.AdvantageNextRound:
                // AdvantageNextRound();
                PowerUpHandler.Instance.AddTimeToNextRound();
                SpawnedPowerUps.Remove(gameObject);
                Destroy(gameObject);
                break;
            case TypeOfAction.SabotageNextRound:
                // SabotageNextRound();
                // PowerUpHandler.Instance.ReduceEnemyTime();
                SpawnedPowerUps.Remove(gameObject);
                Destroy(gameObject);
                break;
        }
    }

    void TakeDamage()
    {
        PowerUpHandler.Instance.IncreaseDamage();
    }
}
