using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GenerateText : MonoBehaviour
{
    private string sentence;
    [SerializeField] int SentenceWords = 3;
    [SerializeField] TMP_Text SentenceText;
    [SerializeField] TMP_InputField UserIP;
    private string[] words;
    private int arrayLength;
    StringBuilder stringBuilder = new StringBuilder();
    private bool textIsRight;
    
    private void Start()
    {
        words = GetComponent<Words>().wordsArray;
        arrayLength = words.Length;
        GenerateSentence();
        UserIP.onValueChanged.AddListener(CheckText);
        UserIP.ActivateInputField();
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        UserIP.caretPosition = UserIP.text.Length;
    }

    private void CheckText(string userText)
    {
        textIsRight = true;
        UserIP.textComponent.color = Color.black;
        for (int i = 0; i < userText.Length; i++)
        {
            if (i < sentence.Length && userText[i] != sentence[i])
            {
                Debug.Log("chyba  " + (i + 1));
                textIsRight = false;
                UserIP.textComponent.color = Color.red;
                break;
            }
        }

        if (userText.Length == sentence.Length  && textIsRight)
        {
            GenerateSentence();
        }
    }
    
    private void GenerateSentence()
    {
        UserIP.text = string.Empty;
        stringBuilder.Clear();
        for (int i = 0; i < SentenceWords; i++)
        {
            int randomIndex = Random.Range(0, arrayLength);
            stringBuilder.Append(words[randomIndex]).Append(" ");
        }
        sentence = stringBuilder.ToString().Trim();
        SentenceText.text = sentence;
    }
}
