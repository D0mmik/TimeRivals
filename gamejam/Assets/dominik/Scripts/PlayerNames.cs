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
    [SerializeField] TMP_Text AttackerName;
    [SerializeField] TMP_Text DefenderName;
    [SerializeField] GameObject MovingMenu;
    [SerializeField] GameObject NamesGO;

    private void Start()
    {
        AttackerNameIP.characterLimit = 13;
        DefenderNameIP.characterLimit = 13;
    }

    public void StartGame()
    {
        if (!string.IsNullOrEmpty(AttackerNameIP.text)) AttackerName.text = AttackerNameIP.text;
        if (!string.IsNullOrEmpty(DefenderNameIP.text)) DefenderName.text = DefenderNameIP.text;
        
        MovingMenu.transform.DOMoveY(-1260, 1.5f);
        NamesGO.transform.DOMoveY(750, 1);
    }
}
