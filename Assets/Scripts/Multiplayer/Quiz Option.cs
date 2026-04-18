using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizOption : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI optionText;
    private Image optionImage;
    private int optionIndex;

    public Action<int> ChooseOption;

    private void Start()
    {
        optionImage = GetComponent<Image>();
    }

    public void ChooseThisOption()
    {
        ChooseOption?.Invoke(optionIndex);
    }

    public void SetText(string text)
    {
        optionText.text = text;
    }

    public void SetOptionIndex(int index)
    {
        optionIndex = index;
    }

    public void TurnGreen()
    {
        optionImage.DOColor(Color.green, 0.1f);
    }

    public void TurnRed()
    {
        optionImage.DOColor(Color.red, 0.1f);
    }

    public void HideOption()
    {
        optionImage.DOFade(0, 0.1f);
        optionText.DOFade(0, 0.1f);
    }

    public void ShowOption()
    {
        optionImage.DOFade(1, 0.1f);
        optionText.DOFade(1, 0.1f);
    }

    public void Bounce()
    {
        transform.DOScale(1.15f, 0.25f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack);
        });
    }

    private void OnDestroy()
    {
        optionImage.DOKill();
        optionText.DOKill();
    }
}
