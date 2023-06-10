using System;
using TMPro;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] protected TMP_Text _usernameText;
    [HideInInspector] public string Username;
    [HideInInspector] public PlayerType PlayerRole;

    [Header("Timer Settings")]
    public float StartTimerValue = 30f;
    [SerializeField] protected float _minTimerValue = 0f;
    [SerializeField] protected float _timerSpeedMultiplyer = 1f;
    [Header("Timer visuals")]
    [SerializeField] protected TMP_Text _timerText;
    [SerializeField] protected float _showDecimalsWhen = 10;
    protected float _time = 0;

    public enum PlayerType
    {
        Defender,
        Attacker
    }

    public float Time
    {
        get
        {
            return _time;
        }
        protected set {}
    }

    protected void SetPlayerText() => _usernameText.text = Username;

    public void SetTimerText(float time)
    {
        // Bouncing effect (Polishing phase)
        if (time <= _showDecimalsWhen) time = (float)Math.Round(time, 2);
        else time = (int)time;

        _timerText.text = $"{time}";
    }

    protected void SetTimeValue(float value)
    {
        _time = Mathf.Clamp(value, _minTimerValue, StartTimerValue);
        if (_time == 0)
        {
            TurnManager.Instance.StartPlayerTimer();
        }
    }

    public void RestartTimer(float startTime)
    {
        _time = startTime;
    }

    protected void FixedUpdate()
    {
        if (_time <= 0) return;
        SetTimeValue(_time - UnityEngine.Time.fixedDeltaTime * _timerSpeedMultiplyer);
        SetTimerText(_time);
    }
}
