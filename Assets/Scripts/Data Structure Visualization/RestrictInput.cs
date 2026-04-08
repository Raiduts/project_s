using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RestrictInput : MonoBehaviour
{
    [Header("Const")]
    [SerializeField]
    private int min, max;
    [SerializeField]
    private bool isPositive;

    private TMP_InputField inputField;

    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    public void OnChangeInput()
    {
        print("Changed");

        int input = int.Parse(inputField.text);

        if (isPositive && input < 0)
        {
            input = -input;
        }

        input = Mathf.Clamp(input, min, max);

        inputField.text = $"{input}";
    }
}
