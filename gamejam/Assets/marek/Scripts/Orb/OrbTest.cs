using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbTest : MonoBehaviour
{
    public static OrbTest Instance;

    public event Action<float> OnHealthChange;

    public void SetInstance()
    {
        if (Instance == null) Instance = this;
        else Debug.LogError($"[OrbTest] There can be only one instance of this object (object: {gameObject.name})");
    }

    [SerializeField] private float _startHealth = 50f;
    private float _health = 0;

    private void Awake()
    {
        SetInstance();
        _health = _startHealth;
    }

    private void Start()
    {
        OnHealthChange?.Invoke(_health);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        OnHealthChange?.Invoke(_health);
    }

    public void Heal(float health)
    {
        _health += health;
        OnHealthChange?.Invoke(_health);
    }

    public void DealDamage()
    {
        if (TurnManager.Instance.CurrentPlayer.PlayerRole == Player.PlayerType.Attacker) TakeDamage(7f);
        else Debug.Log("Cant deal damage to orb!");
    }

    public void HealOrb()
    {
        if (TurnManager.Instance.CurrentPlayer.PlayerRole == Player.PlayerType.Defender) Heal(5f);
        else Debug.Log("Cant heal orb!");
    }
}
