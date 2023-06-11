using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Challenges : MonoBehaviour
{
    public Transform[] ChallengesArray;
    [SerializeField] private GameObject _questsPanel;

    private void Start()
    {
        ChallengesArray = Array.FindAll(GetComponentsInChildren<Transform>(), child => child != this.transform);
        foreach (Transform challenge in ChallengesArray)
        {
            challenge.gameObject.SetActive(false);
        }
        Close();
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
    public void Close() => _questsPanel.SetActive(false);

}
