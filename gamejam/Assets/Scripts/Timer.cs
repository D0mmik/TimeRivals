using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private Vector3 endTextScale;
    [SerializeField] private float scaleTweenDuration;

    public void StartGameTimer(int startCount, Action onCompleteCallback = null)
    {
        StartCoroutine(StartGameTimerCoroutine(startCount, prefab, spawnParent, onCompleteCallback));
    }

    private IEnumerator StartGameTimerCoroutine(int startGameDelay, GameObject timer, Transform timerParent, Action onCompleteCallback = null)
    {
        int time = startGameDelay;
        GameObject spawnedTimer = Instantiate(timer, timerParent);
        TMP_Text spawnedTimerText = spawnedTimer.GetComponentInChildren<TMP_Text>();
        if (spawnedTimerText == null) yield break;

        while (time >= 1)
        {
            SetTimerText(spawnedTimerText, time.ToString());
            yield return new WaitForSeconds(1f);
            time -= 1;
        }

        SetTimerText(spawnedTimerText, "Start!", () => Destroy(spawnedTimer));
        
        onCompleteCallback?.Invoke();
    }

    private void SetTimerText(TMP_Text timerText, string text, Action onCompleteCallback = null)
    {
        timerText.text = text;
        timerText.transform.localScale = Vector3.one;
        timerText.transform.DOScale(endTextScale, scaleTweenDuration)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => onCompleteCallback?.Invoke());
    }
}
