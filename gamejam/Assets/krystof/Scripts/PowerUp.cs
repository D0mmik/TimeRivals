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
    [SerializeField] private static List<GameObject> _spawnedPowerUps = new List<GameObject>();
    private Char _char;

    private void Awake()
    {
        //gameObject.GetComponent<SpriteRenderer>().sprite = _image;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,-1) * _fallingSpeed;
        _char = _chars[Random.Range(0, _chars.Length)];
        gameObject.GetComponentInChildren<TextMeshPro>().text = _char.ToString();
        _spawnedPowerUps.Add(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(_char.ToString()))
        {
           PowerUpSpawner.Instance.StartWriting.Invoke();
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
    
}
