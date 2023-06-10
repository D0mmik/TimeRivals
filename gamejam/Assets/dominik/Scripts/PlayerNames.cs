using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNames : MonoBehaviour
{
    [SerializeField] TMP_InputField AttackerNameIP;
    [SerializeField] TMP_InputField DefenderNameIP;
    [SerializeField] private TMP_Text AttackerName;
    [SerializeField] private TMP_Text DefenderName;
    public static string DefenderUsername = "Player1";
    public static string AttackerUsername = "Player2";
    [SerializeField] GameObject MovingMenu;
    [SerializeField] GameObject NamesGO;
    [SerializeField] private float _devScreenResolutionHeight = 1440;

    private void Start()
    {
        AttackerNameIP.characterLimit = 13;
        DefenderNameIP.characterLimit = 13;
    }

    public void MoveUI()
    {
        if (!string.IsNullOrEmpty(AttackerNameIP.text))
        {
            AttackerUsername = AttackerNameIP.text;
            AttackerName.text = AttackerUsername;
        }

        if (!string.IsNullOrEmpty(DefenderNameIP.text))
        {
            DefenderUsername = DefenderNameIP.text;
            DefenderName.text = DefenderUsername;
        }
        
        MovingMenu.transform.DOMoveY(-1260, 1.5f);
        NamesGO.transform.DOMoveY(CalculateTextMove(750), 1).OnComplete(() => StartGame());
    }

    private float CalculateTextMove(float movePos)
    {
        Debug.Log(_devScreenResolutionHeight + " " + Screen.currentResolution.height);
        float desiredMovePos = movePos * (Screen.currentResolution.height / _devScreenResolutionHeight);
        Debug.Log(desiredMovePos);
        return desiredMovePos;
    }

    private void StartGame()
    {
        StartManager.Instance.StartGameTimer();
    }
}
