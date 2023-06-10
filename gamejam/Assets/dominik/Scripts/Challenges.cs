using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Challenges : MonoBehaviour
{
    public Transform[] ChallengesArray;

    void Start()
    {
        ChallengesArray = Array.FindAll(GetComponentsInChildren<Transform>(), child => child != this.transform);
        StartRandomChallenge();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            StartRandomChallenge();
        }
    }

    void StartRandomChallenge()
    {
        foreach (var challenge in ChallengesArray)
            challenge.gameObject.SetActive(false);

        ChallengesArray[Random.Range(0, ChallengesArray.Length)].gameObject.SetActive(true);
    }

}
