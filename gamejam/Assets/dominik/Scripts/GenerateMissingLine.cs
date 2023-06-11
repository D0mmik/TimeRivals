using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateMissingLine : MonoBehaviour
{
    [SerializeField] TMP_Text LineText;
    [SerializeField] TMP_InputField UserIP;
    [SerializeField] int LineLength = 5;
    [SerializeField] string[] Line;
    [SerializeField] string MissingNum;
    [SerializeField] int Multiplier;

    private void Start()
    {
        UserIP.characterValidation = TMP_InputField.CharacterValidation.Integer;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (MissingNum == UserIP.text)
            {
                //UserIP.textComponent.color = Color.green;
                PowerUpSpawner.Instance.StartSpawning?.Invoke();
            }
        }
    }

    private void OnEnable()
    {
        UserIP.gameObject.SetActive(true);
        UserIP.Select();
        GenerateMissingNum(LineLength);
    }
    private void OnDisable()
    {
        if (UserIP != null) UserIP.gameObject.SetActive(false);
    }

    private void GenerateMissingNum(int lenght)
    {
        UserIP.characterValidation = TMP_InputField.CharacterValidation.Integer;
        UserIP.ActivateInputField();
        Multiplier = Random.Range(1, 20);
        UserIP.text = string.Empty;
        Line = new string[LineLength];
        for (int i = 0; i < LineLength; i++)
        {
            Line[i] = ((i + 1) * Multiplier).ToString();
        }

        var missingIndex = Random.Range(0, LineLength);
        MissingNum = Line[missingIndex];
        Line[missingIndex] = "X";

        Debug.Log(string.Join(" ",Line));
        LineText.text = string.Join(" ", Line);


    }
}
