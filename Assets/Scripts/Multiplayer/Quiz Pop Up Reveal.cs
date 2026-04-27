using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizPopUpReveal : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI mainText, scoreText;
    [SerializeField]
    private float duration;
    [SerializeField] private Color correct, incorrect;

    private RectTransform rectTransform;
    private Image popUpBackground;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        popUpBackground = GetComponent<Image>();
    }

    public void SetText(string text, int score = 0)
    {
        mainText.text = text;

        if (score == 0)
        {
            popUpBackground.color = incorrect;
            scoreText.gameObject.SetActive(false);
        }
        else
        {
            popUpBackground.color = correct;
            scoreText.gameObject.SetActive(true);
            scoreText.text = $"+{score}";   
        }
    }

    public void ShowPopUp()
    {
        rectTransform.DOAnchorPosY(-76, 0.25f);

        Invoke(nameof(HidePopUp), duration);
    }

    public void HidePopUp()
    {
        rectTransform.DOAnchorPosY(76, 0.25f);
    }
}
