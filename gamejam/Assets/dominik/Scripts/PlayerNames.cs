using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNames : MonoBehaviour
{
    [Header("UI Dependencies")]
    [SerializeField] TMP_InputField _attackerNameIP;
    [SerializeField] TMP_InputField _defenderNameIP;
    [SerializeField] private TMP_Text _attackerName;
    [SerializeField] private TMP_Text _defenderName;
    public static string DefenderUsername;
    public static string AttackerUsername;
    [SerializeField] GameObject _movingMenu;
    [SerializeField] GameObject _namesGO;
    [SerializeField] private float _devScreenResolutionHeight = 1440;

    [Header("Default Usernames")]
    [SerializeField] private string _attackerDefaultUsername = "Attacker";
    [SerializeField] private string _defenderDefaultUsername = "Defender";

    private void Start()
    {
        _attackerNameIP.characterLimit = 13;
        _defenderNameIP.characterLimit = 13;
        SetDefaultUsernames();
    }

    private void SetDefaultUsernames()
    {
        AttackerUsername = _defenderDefaultUsername;
        DefenderUsername = _attackerDefaultUsername;
        UpdateVisuals();
    }

    /// <summary>
    /// Updates UI
    /// </summary>
    private void UpdateVisuals()
    {
        _attackerName.text = AttackerUsername;
        _defenderName.text = DefenderUsername;
    }

    public void SetAndMoveUI()
    {
        if (!string.IsNullOrEmpty(_attackerNameIP.text))
        {
            // Trim removes any white space at the start and the end of the string
            AttackerUsername = _attackerNameIP.text.Trim();
            _attackerName.text = AttackerUsername;
        }

        if (!string.IsNullOrEmpty(_defenderNameIP.text))
        {
            // Trim removes any white space at the start and the end of the string
            DefenderUsername = _defenderNameIP.text.Trim();
            _defenderName.text = DefenderUsername;
        }

        _movingMenu.transform.DOMoveY(-1260, 1.5f);
        _namesGO.transform.DOMoveY(CalculateTextMove(750), 1).OnComplete(StartGame);
    }

    /// <summary>
    /// Calculates the move ammount on a different resolution which isnt developers resolution
    /// </summary>
    /// <param name="movePos">Move position which is recalculated</param>
    /// <returns>Recalculated movePos</returns>
    private float CalculateTextMove(float movePos)
    {
        float desiredMovePos = movePos * (Screen.currentResolution.height / _devScreenResolutionHeight);
        return desiredMovePos;
    }

    private void StartGame()
    {
        StartManager.Instance.StartGameTimer();
    }
}
