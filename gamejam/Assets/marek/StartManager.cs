using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [SerializeField] private Transform _timer;
    [SerializeField] private Transform _timerParent;
    [SerializeField] private int _startGameDelay = 3;

    [SerializeField] private Vector3 _timerEndScale = new Vector3(0.3f, 0.3f, 0.3f);
    [SerializeField] private float _timerScaleDuration = 1f;

    [Header("Transition Options")]
    [SerializeField] private Transform _transition;
    [SerializeField] private Vector3 _closeScale;
    [SerializeField] private Vector3 _openScale;
    [SerializeField] private float _transitionDuration = 2f;

    #region Singleton
    public static StartManager Instance;

    private void SetInstance()
    {
        if (Instance == null) Instance = this;
        else Debug.LogError($"[StartManager] There can be only one instance of this object (object: {gameObject.name})");
    }
    #endregion

    private void Awake()
    {
        SetInstance();
    }

    private void Start()
    {
        if (!GameManager.Instance.IsInFirstScene()) DoOpenTransition(() => _transition.gameObject.SetActive(false));
            
    }

    public void StartGameTimer()
    {
        Debug.Log("Starting Game");
        StartCoroutine(StartGameTimerCoroutine());
    }

    private IEnumerator StartGameTimerCoroutine()
    {
        int time = _startGameDelay;
        GameObject spawnedTimer = Instantiate(_timer.gameObject, _timerParent);
        TMP_Text spawnedTimerText = spawnedTimer.GetComponentInChildren<TMP_Text>();
        if (spawnedTimerText == null) yield break;

        while (time >= 1)
        {
            SetTimerText(spawnedTimerText, time.ToString());
            yield return new WaitForSeconds(1f);
            time -= 1;
        }
        SetTimerText(spawnedTimerText, "Start!", () =>
        {
            DoCloseTransition(GameManager.Instance.LoadNextScene);
        });
    }

    private void SetTimerText(TMP_Text timerText, string text, Action onCompleteCallback = null)
    {
        timerText.text = text;
        timerText.transform.localScale = Vector3.one;
        timerText.transform.DOScale(_timerEndScale, _timerScaleDuration)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => onCompleteCallback?.Invoke());
    }

    private void DoCloseTransition(Action onCompleteCallback = null) => DoTransition(_closeScale, _transitionDuration, Ease.OutCubic, onCompleteCallback);

    private void DoOpenTransition(Action onCompleteCallback = null) => DoTransition(_openScale, _transitionDuration, Ease.OutCubic, onCompleteCallback);

    private void DoTransition(Vector3 scale, float scaleDuration, Ease ease, Action onCompleteCallback = null)
    {
        _transition.gameObject.SetActive(true);
        _transition.DOScale(scale, scaleDuration)
            .SetEase(ease)
            .OnComplete(() => onCompleteCallback?.Invoke());
    }
}
