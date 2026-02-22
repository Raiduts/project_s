using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserScore : MonoBehaviour
{
    public TextMeshProUGUI numberText, nameText, scoreText;

    public void SetUserScore(int number, string name, int score)
    {
        numberText.text = number.ToString();
        nameText.text = name;
        scoreText.text = score.ToString();
    }
}
