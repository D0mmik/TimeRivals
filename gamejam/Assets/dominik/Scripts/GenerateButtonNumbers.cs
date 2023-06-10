using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GenerateButtonNumbers : MonoBehaviour
{
    [SerializeField] TMP_Text[] Texts;
    [SerializeField] int[] Numbers;
    [SerializeField] int Length = 9;
    [SerializeField] GameObject ButtonsGO;
    [SerializeField] Button[] Buttons;
    [SerializeField] int CurrentIndex;

    private void Start()
    {
        Numbers = new int[Length];
        foreach (var button in Buttons)
        {
            button.onClick.AddListener(() => GetNumberOnButton(button));
        }
        //GenerateButtons();
    }

    private void GetNumberOnButton(Button button)
    {
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        if (buttonText.text == Numbers[CurrentIndex].ToString())
        {
            CurrentIndex++;
            buttonText.color = Color.green;
        }
        if (CurrentIndex == 9)
        {
            Debug.Log("hotovo");
            //PowerUpSpawner.Instance.StartSpawning.Invoke();
        }
    }

    private void OnEnable()
    {
        foreach (var text in Texts)
        {
            text.color = Color.black;
        }
        ButtonsGO.gameObject.SetActive(true);
        StartCoroutine(GenerateButtonsCourotine());
    }

    IEnumerator GenerateButtonsCourotine()
    {
        yield return new WaitForSeconds(0.001f);
        GenerateButtons();
    }
    private void OnDisable()
    {
        if (ButtonsGO != null) ButtonsGO.gameObject.SetActive(false);
    }

    private void GenerateButtons()
    {
        RandomNumbers(Length);
        for (int i = 0; i < Texts.Length; i++)
        {
            Texts[i].text = Numbers[i].ToString();
        }
        Array.Sort(Numbers);
    }

    private void RandomNumbers(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Numbers[i] = Random.Range(1, 999);
        }
    }
}
