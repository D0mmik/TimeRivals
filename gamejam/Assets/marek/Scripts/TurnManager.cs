using System;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    #region Singleton
    public static TurnManager Instance;

    private void SetInstance()
    {
        if (Instance == null) Instance = this;
        else Debug.LogError($"[TurnManager] There can be only one instance of this object (object: {gameObject.name})");
    }
    #endregion

    private void Awake()
    {
        SetInstance();
    }
}