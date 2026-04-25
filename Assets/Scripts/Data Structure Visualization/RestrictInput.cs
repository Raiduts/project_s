using System;
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

        inputField.onEndEdit.AddListener(OnChangeInput);
    }

    private void OnChangeInput(string textInput)
    {
        int input = 0;

        try
        {
            input = int.Parse(textInput);
        }
        catch 
        {

        }

        if (isPositive && input < 0)
        {
            input = -input;
        }

        input = Mathf.Clamp(input, min, max);

        inputField.text = $"{input}";
    }
}
