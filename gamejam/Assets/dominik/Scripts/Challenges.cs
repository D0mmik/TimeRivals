using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Challenges : MonoBehaviour
{
    public Transform[] ChallengesArray;
    [SerializeField] private GameObject _questsPanel;
    [SerializeField] private TMP_Text StatusText;

    private void Start()
    {
        ChallengesArray = Array.FindAll(GetComponentsInChildren<Transform>(), child => child != this.transform);
        foreach (Transform challenge in ChallengesArray)
        {
            challenge.gameObject.SetActive(false);
        }
        _questsPanel.SetActive(false);
    }

    public void StartRandomChallenge()
    {
        if (PowerUpSpawner.Instance.TimeExpired) return;
        Open();
        foreach (var challenge in ChallengesArray)
            challenge.gameObject.SetActive(false);

        
        ChallengesArray[Random.Range(0, ChallengesArray.Length)].gameObject.SetActive(true);
    }

    private void Open() => _questsPanel.SetActive(true);

    public void Close()
    {
        Status(Color.white, "Time Expired!");
    }

    public void CorrectAndClose()
    {
        Status(Color.green, "Correct!");
    }
    private void CloseText() => StatusText.gameObject.SetActive(false);

    private void Status(Color color, string text)
    {
        _questsPanel.SetActive(false);
        StatusText.text = text;
        StatusText.color = color;
        StatusText.gameObject.SetActive(true);
        StatusText.DOFade(0, 1f).onComplete = CloseText;
    }
}
