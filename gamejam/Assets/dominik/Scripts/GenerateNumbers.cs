using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateNumbers : MonoBehaviour
{
    [SerializeField] private TMP_Text ChallengeName;
    [SerializeField] GenerateText generateText;
    [SerializeField] TMP_Text EquationText;
    [SerializeField] TMP_InputField UserIP;
    [SerializeField] int minNumber;
    [SerializeField] int maxNumber;
    public string[] operators = { "+", "-", "*"};
    private int result;

    private void Start()
    {
        UserIP.characterValidation = TMP_InputField.CharacterValidation.Integer;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (result.ToString() == UserIP.text)
            {
                //UserIP.textComponent.color = Color.green;
                PowerUpSpawner.Instance.StartSpawning?.Invoke();
            }
        }
    }

    private void OnEnable()
    {
        ChallengeName.text = "Count the expression";
        UserIP.gameObject.SetActive(true);
        UserIP.textComponent.color = Color.white;
        UserIP.Select();
        GenerateEquation();
    }
    private void OnDisable()
    {
        if (UserIP != null) UserIP.gameObject.SetActive(false);
    }


    public void GenerateEquation()
    {
        UserIP.ActivateInputField();
        UserIP.text = string.Empty;
        int number1 = Random.Range(minNumber, maxNumber + 1);
        int number2 = Random.Range(minNumber, maxNumber + 1);
        string selectedOperator = operators[Random.Range(0, operators.Length)];

        string equation = number1 + " " + selectedOperator + " " + number2;
        
        result = CalculateResult(number1, number2, selectedOperator);

        EquationText.text = equation;
        Debug.Log("Result: " + result);
    }

    private int CalculateResult(int number1, int number2, string selectedOperator)
    {
        var result = selectedOperator switch
        {
            "+" => number1 + number2,
            "-" => number1 - number2,
            "*" => number1 * number2,
            _ => 0
        };

        return result;
    }
}
