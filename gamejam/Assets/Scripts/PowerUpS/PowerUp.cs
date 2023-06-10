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
    [SerializeField] public static List<GameObject> _spawnedPowerUps = new List<GameObject>();
    private static List<PowerUpDelegate> actionToExecute = new List<PowerUpDelegate>();
    private string _char;
    public static PowerUp SelectedPowerUp;
    delegate void PowerUpDelegate();

    enum OnMove
    {
        defender,
        attacker
    }

    private OnMove whoIsOnMove = OnMove.defender;

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
        _spawnedPowerUps.Add(gameObject);
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
            _spawnedPowerUps.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public void ClaimPowerUp()
    {
        switch (_typeOfAction)
        {
            case TypeOfAction.Advantage:
                Advantage();
                _spawnedPowerUps.Remove(gameObject);
                Destroy(gameObject);
                break;
            case TypeOfAction.Attack:
                TakeDamage();
                _spawnedPowerUps.Remove(gameObject);
                Destroy(gameObject);
                break;
            case TypeOfAction.AdvantageNextRound:
                AdvantageNextRound();
                _spawnedPowerUps.Remove(gameObject);
                Destroy(gameObject);
                break;
            case TypeOfAction.SabotageNextRound:
                SabotageNextRound();
                _spawnedPowerUps.Remove(gameObject);
                Destroy(gameObject);
                break;
        }
    }

    void TakeDamage()
    {
        Debug.Log("Damage");
        if (whoIsOnMove == OnMove.attacker)
        {
            TimeBall.Instance.DamageHeal(Random.Range(-5, -11));
        }
        else if (whoIsOnMove == OnMove.attacker)
        {
            TimeBall.Instance.DamageHeal(Random.Range(5, 11));
        }
        
        actionToExecute.Add(TakeDamage);
    }

    void Advantage()
    {
        Debug.Log("Advantage");
        int random = Random.Range(0, 4);
        
        switch (random)
        {
            case 0:
                // More time
                void MoreTime()
                {
                    //PowerUpSpawner.TimeLeft += Random.Range(5, 11);   
                }
                actionToExecute.Add(MoreTime);
                break;
            case 1:
                // Slower falling
                void SlowerFalling()
                {
                    //_fallingSpeed = _fallingSpeed / 2;
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1,2) * _sidePushSpeed ,Random.Range(_minFallingSpeed, _maxFallingSpeed))); 
                }
                actionToExecute.Add(SlowerFalling);
                break;
        }
    }

    void AdvantageNextRound()
    {
        Debug.Log("Advantage Next Round");
        int random = Random.Range(0, 4);
        
        switch (random)
        {
            case 0:
                // Slower falling next round
                void SlowerFallingNextround()
                {
                    Debug.Log("Slower falling next round");
                }
                actionToExecute.Add(SlowerFallingNextround);
                break;
            case 1:
                // More time next round
                void MoreTimeNextRound()
                {
                    Debug.Log("More time next round");
                }
                actionToExecute.Add(MoreTimeNextRound);
                break;
        }
    }

    void SabotageNextRound()
    {
        Debug.Log("Sabotage Next Round");
        int random = Random.Range(0, 4);
        
        switch (random)
        {
            case 0:
                // Less time for opponent next round
                void LessTimeForOpponentNextRound()
                {
                    Debug.Log("Less time for opponent next round");
                }
                break;
            case 1:
                // Faster falling next round
                void FasterFallingNextRound()
                {
                    Debug.Log("Faster falling next round");
                }
                break;
        }
    }
    
}
