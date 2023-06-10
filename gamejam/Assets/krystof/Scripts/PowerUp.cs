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
    [SerializeField] private float _fallingSpeed;
    [SerializeField] private Char[] _chars = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p','q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
    [SerializeField] public static List<GameObject> _spawnedPowerUps = new List<GameObject>();
    private static List<GameObject> actionToExecute = new List<GameObject>();
    private string _char;

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
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,-1) * _fallingSpeed;
        _char = _chars[Random.Range(0, _chars.Length)].ToString().ToUpper();
        
        
        foreach (var VARIABLE in _spawnedPowerUps)
        {
            if (VARIABLE.GetComponent<PowerUp>()._char == _char)
            {
                
            }
        }
        gameObject.GetComponentInChildren<TextMeshPro>().text = _char;
        _spawnedPowerUps.Add(gameObject);
    }

    private void OnEnable()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,-1) * _fallingSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_char.ToLower()))
        {
           PowerUpSpawner.Instance.StartWriting.Invoke();
           Action();
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

    public void Action()
    {
        switch (_typeOfAction)
        {
            case TypeOfAction.Advantage:
                Debug.Log("Advantage");
                _spawnedPowerUps.Remove(gameObject);
                Destroy(gameObject);
                break;
            case TypeOfAction.Attack:
                Debug.Log("Atack");
                _spawnedPowerUps.Remove(gameObject);
                Destroy(gameObject);
                break;
            case TypeOfAction.AdvantageNextRound:
                Debug.Log("AdvantageNextRound");
                _spawnedPowerUps.Remove(gameObject);
                Destroy(gameObject);
                break;
            case TypeOfAction.SabotageNextRound:
                Debug.Log("SabotageNextRound");
                _spawnedPowerUps.Remove(gameObject);
                Destroy(gameObject);
                break;
        }
    }

    class ActionType
    {
        
    }
    
}
