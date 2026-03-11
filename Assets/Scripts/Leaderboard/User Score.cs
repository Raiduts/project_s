using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserScore : MonoBehaviour
{
    public Image backgroundImage;
    public TextMeshProUGUI numberText, nameText, scoreText;

    public Color[] colors;

    public void SetUserScore(int number, string name, int score)
    {
        numberText.text = number.ToString();
        nameText.text = name;
        scoreText.text = score.ToString();

        SetColor(number);
    }

    public void SetColor(int number)
    {
        switch(number)
        {
            case 1:
                backgroundImage.color = colors[0];
                break;
            case 2:
                backgroundImage.color = colors[1];
                break;
            case 3:
                backgroundImage.color = colors[2];
                break;
            default:
                backgroundImage.color = colors[3];
                break;
        }   
    }
}
