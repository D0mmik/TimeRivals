using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public static Orb Instance;

    public event Action<float> OnHealthChange;

    public void SetInstance()
    {
        if (Instance == null) Instance = this;
        else Debug.LogError($"[OrbTest] There can be only one instance of this object (object: {gameObject.name})");
    }

    [SerializeField] private float _startHealth = 50f;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _minHealth = 0f;
    private float _health = 0;

    public float Health
    {
        get
        {
            return _health;
        }
        private set
        {
            _health = Mathf.Clamp(value, _minHealth, _maxHealth);
        }
    }

    private void Awake()
    {
        SetInstance();
        _health = _startHealth;
    }

    private void Start()
    {
        OnHealthChange?.Invoke(_health);
        
        // Temporary
        TurnManager.Instance.CurrentPlayer.DecreaseEnemyTime(20);
    }

    public void Damage(float damage)
    {
        Health -= damage;
        OnHealthChange?.Invoke(_health);
    }

    public void Heal(float health)
    {
        Health += health;
        OnHealthChange?.Invoke(_health);
    }
}
