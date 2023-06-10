using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private Defender _defender;
    public Player CurrentPlayer;

    [Header("Timer Settings")]
    [SerializeField] private int _startCount = 5;

    #region Singleton
    public static TurnManager Instance;

    private void SetInstance()
    {
        if (Instance == null) Instance = this;
        else Debug.LogError($"[TurnManager] There can be only one instance of this object (object: {gameObject.name})");
    }
    #endregion

    private void Awake()
    {
        CurrentPlayer = _attacker;
        SetInstance();
    }

    public void StartPlayerTimer()
    {
        CurrentPlayer = CurrentPlayer == _attacker ? _defender : _attacker;
        _timer.StartGameTimer(_startCount, () =>
        {
            StartTimer();
            // This has to be called after the StartTimer function, it depends on it!
            PowerUpSpawner.Instance.StartSpawningPowerUps();
        });
    }

    private void StartTimer()
    {
        CurrentPlayer.RestartTimer(CurrentPlayer.StartTimerValue);
    }

    // Workflow: Let the player attack for a certain ammount of time
    // When the time is over, start a timer to let players switch on their pc
    // After the time is over (5 seconds), let the second player do their thing
    // Keep track of the current player and display it on the screen :)
}