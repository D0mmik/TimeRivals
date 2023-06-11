using Unity.VisualScripting;
using UnityEngine;

public class PowerUpHandler : MonoBehaviour
{
    [SerializeField] private float _minDamage = 5f;
    [SerializeField] private float _maxDamage = 5f;

    [SerializeField] private float _minTime = 3f;
    [SerializeField] private float _maxTime = 7f;

    [SerializeField] private float _instantTimeIncrease = 10f;

    #region Singleton
    public static PowerUpHandler Instance;

    private void SetInstance()
    {
        if (Instance == null) Instance = this;
        else Debug.LogError($"[PowerUpHandler] There can be only one instance of this object (object: {gameObject.name})");
    }
    #endregion

    private void Awake()
    {
        SetInstance();
    }

    public void IncreaseDamage()
    {
        float damage = GetRandomDamage();
        TurnManager.Instance.CurrentPlayer.IncreaseDamage(damage);
    }

    public void AddTime()
    {
        TurnManager.Instance.CurrentPlayer.IncreaseTime(_instantTimeIncrease);
    }

    public void AddTimeToNextRound()
    {
        float time = GetRandomTime();
        TurnManager.Instance.CurrentPlayer.IncreaseNextRoundTime(time);
    }

    /*public void ReduceEnemyTime()
    {
        float time = -GetRandomTime();
        Debug.Log($"Time: {time}");

        TurnManager.Instance.CurrentPlayer.DecreaseNextRoundTimeValue(time);
    }*/

    private float GetRandomDamage() => GetRandomNumber(_minDamage, _maxDamage);

    private float GetRandomTime() => GetRandomNumber(_minTime, _maxTime);

    private float GetRandomNumber(float min, float max)
    {
        float number = Random.Range(min, max);
        return number;
    }
}
