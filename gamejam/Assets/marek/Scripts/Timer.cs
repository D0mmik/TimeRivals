using UnityEngine;

public class Timer : MonoBehaviour
{
    private TimerVisuals _timerVisuals;
    [SerializeField] private float _startTimerValue = 0f;
    [SerializeField] private float _minTimerValue = 0f;
    [SerializeField] private float _timerSpeedMultiplyer = 1f;
    private float _time = 0;

    public float Time
    {
        get
        {
            return _time;
        }
        private set {}
    }

    #region Singleton
    public static Timer Instance;

    private void SetInstance()
    {
        if (Instance == null) Instance = this;
        else Debug.LogError($"[TurnManager] There can be only one instance of this object (object: {gameObject.name})");
    }
    #endregion

    private void SetTimeValue(float value)
    {
        _time = Mathf.Clamp(value, _minTimerValue, _startTimerValue);
    }

    private void Awake()
    {
        GetDependencies();
        SetInstance();
        _time = _startTimerValue;
    }

    private void GetDependencies()
    {
        _timerVisuals = GetComponent<TimerVisuals>();
    }

    private void FixedUpdate()
    {
        if (UnityEngine.Time.time >= _startTimerValue) return;
        SetTimeValue(_time - UnityEngine.Time.fixedDeltaTime * _timerSpeedMultiplyer);
        _timerVisuals.SetTimerText(_time);
    }
}
