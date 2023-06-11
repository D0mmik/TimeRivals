using System;
using TMPro;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [Header("Player Settings")] [SerializeField]
    protected TMP_Text _usernameText;

    [HideInInspector] public string Username;
    [HideInInspector] public PlayerType PlayerRole;

    [Header("Timer Settings")] public float StartTimerValue = 30f;
    [SerializeField] protected float _minTimerValue = 0f;
    [SerializeField] protected float _timerSpeedMultiplyer = 1f;
    [SerializeField] protected float _minStartTimerValue = 15f;
    [SerializeField] protected float _maxStartTimerValue = 60f;

    private float _enemyDecreaseTime;

    [Header("Timer visuals")] [SerializeField]
    protected TMP_Text _timerText;

    [SerializeField] protected float _showDecimalsWhen = 10;
    protected float _time = 0;

    [Header("PowerUp OnRoundEnd")] private float _damage = 0f;
    private float _nextRoundTime = 0f;
    private event Action _onTimerEnd;

    public enum PlayerType
    {
        Defender,
        Attacker
    }

    public float Time
    {
        get { return _time; }
        protected set { }
    }

    protected void SetUp()
    {
        _onTimerEnd += ApplyAfterRoundPowerUps;
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
        if (_time == 0 && !PowerUpSpawner.Instance.TimeExpired)
        {
            _onTimerEnd.Invoke();
            PowerUpSpawner.Instance.OnTimerExpire?.Invoke();
            TurnManager.Instance.StartPlayerTimer();
        }
    }

    public void RestartTimer(float startTime)
    {
        _time = startTime;
    }

    public void IncreaseTime(float time)
    {
        _time += time;
    }

    public void IncreaseNextRoundTime(float time)
    {
        _nextRoundTime += time;
        _nextRoundTime = Mathf.Clamp(_nextRoundTime, _minStartTimerValue, _maxStartTimerValue);
    }

    public void DecreaseNextRoundTime(float time)
    {
        if (time > 0) time = -time;
        Debug.Log($"TImespeci {time}");
        IncreaseNextRoundTime(time);
    }

    public void DecreaseNextRoundTimeValue(float time) => _enemyDecreaseTime -= time;

    public void IncreaseDamage(float damage) => _damage += damage;

    public void DecreaseEnemyTime(float time)
    {
        Player enemy = TurnManager.Instance.Attacker;
        if (TurnManager.Instance.CurrentPlayer == TurnManager.Instance.Attacker) enemy = TurnManager.Instance.Defender;
        
        enemy.DecreaseNextRoundTime(time);
    }

    protected void FixedUpdate()
    {
        if (_time <= 0) return;
        SetTimeValue(_time - UnityEngine.Time.fixedDeltaTime * _timerSpeedMultiplyer);
        SetTimerText(_time);
    }

    protected void ApplyAfterRoundPowerUps()
    {
        if (PlayerRole == PlayerType.Attacker) Orb.Instance.Damage(_damage);
        else Orb.Instance.Heal(_damage);
        StartTimerValue += _nextRoundTime;
        StartTimerValue = Mathf.Clamp(StartTimerValue, _minStartTimerValue, _maxStartTimerValue);
        DecreaseEnemyTime(_nextRoundTime);
        // Reset variables
        ResetAllAfterRoundVariables();
    }

    protected void ResetAllAfterRoundVariables()
    {
        _damage = 0f;
        _nextRoundTime = 0f;
        _enemyDecreaseTime = 0f;
    }
}
