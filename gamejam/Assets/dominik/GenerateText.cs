using System.Text;
using TMPro;
using UnityEngine;

public class GenerateText : MonoBehaviour
{
    private string sentence;
    [SerializeField] int SentenceLength = 3;
    [SerializeField] TMP_Text SentenceText;
    private string[] words;
    private int arrayLength;
    StringBuilder stringBuilder = new StringBuilder();
    private void Start()
    {
        words = GetComponent<Words>().wordsArray;
        arrayLength = words.Length;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateSentence();
        }
    }

    private void GenerateSentence()
    {
        stringBuilder.Clear();
        for (int i = 0; i < SentenceLength; i++)
        {
            int randomIndex = Random.Range(0, arrayLength);
            stringBuilder.Append(words[randomIndex]).Append(" ");
        }
        sentence = stringBuilder.ToString().Trim();
        SentenceText.text = sentence;
    }
}
