using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private Sprite _image;
    [SerializeField] private float _fallingSpeed;

    private void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = _image;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,-1) * _fallingSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}
