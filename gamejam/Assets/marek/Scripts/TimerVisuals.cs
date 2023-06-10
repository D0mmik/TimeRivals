using System;
using TMPro;
using UnityEngine;

public class TimerVisuals : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private float _showDecimals = 10;

    public void SetTimerText(float timer)
    {
        if (timer <= _showDecimals) timer = (float)Math.Round(timer, 2);
        else timer = (int)timer;

        _timerText.text = $"{timer}";
    }
}
