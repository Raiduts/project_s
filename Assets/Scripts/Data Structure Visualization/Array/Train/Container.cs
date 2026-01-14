using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Container : MonoBehaviour
{
    public int value;
    public TextMeshProUGUI valueText;

    public void SetValue(int newValue)
    {
        value = newValue;
        valueText.text = newValue.ToString();
    }

    public void RandomizeValue()
    {
        int random = Random.Range(0,100);
        SetValue(random);
    }
}
