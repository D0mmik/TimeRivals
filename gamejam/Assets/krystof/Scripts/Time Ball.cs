using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBall : MonoBehaviour
{
    [SerializeField] private float _lives = 100f;
    public static TimeBall Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void DamageHeal(float damage)
    {
        _lives += damage;
    }
}
