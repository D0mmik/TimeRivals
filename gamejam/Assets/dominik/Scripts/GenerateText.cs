using System.Text;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateText : MonoBehaviour
{
    private string sentence;
    [SerializeField] int SentenceWords = 3;
    [SerializeField] TMP_Text SentenceText;
    [SerializeField] TMP_InputField UserIP;
    private string[] words;
    StringBuilder stringBuilder;
    private bool textIsRight;

    private void Start()
    {
        UserIP.onValueChanged.AddListener(CheckText);
    }

    private void OnEnable()
    {
        UserIP.gameObject.SetActive(true);
        UserIP.Select();
        GenerateSentence();
    }
    private void OnDisable()
    {
        if (UserIP != null) UserIP.gameObject.SetActive(false);
    }

    private void CheckText(string userText)
    {
        textIsRight = true;
        UserIP.textComponent.color = Color.white;
        for (int i = 0; i < userText.Length; i++)
        {
            if (i < sentence.Length && userText[i] != sentence[i])
            {
                textIsRight = false;
                UserIP.textComponent.color = Color.red;
                break;
            }
        }

        if (userText.Length == sentence.Length  && textIsRight)
        {
            //UserIP.textComponent.color = Color.green;
            PowerUpSpawner.Instance.StartSpawning?.Invoke();
        }
    }
    
    public void GenerateSentence()
    {
        UserIP.characterValidation = TMP_InputField.CharacterValidation.None;
        UserIP.ActivateInputField();
        stringBuilder = new StringBuilder();
        words = GetComponent<Words>().wordsArray;
        
        UserIP.text = string.Empty;
        stringBuilder.Clear();
        for (int i = 0; i < SentenceWords; i++)
        {
            int randomIndex = Random.Range(0, words.Length);
            stringBuilder.Append(words[randomIndex]).Append(" ");
        }
        sentence = stringBuilder.ToString().Trim();
        SentenceText.text = sentence;
    }
}
