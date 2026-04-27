using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ErrorPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI errorMessage;

    public Action CloseError;

    private void Start()
    {
        Show();
    }

    private void Show()
    {
        transform.DOScaleY(1.15f, 0.25f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            transform.DOScaleY(1, 0.25f).SetEase(Ease.OutBack);
        });
    }

    public void Hide()
    {
        transform.DOScaleY(0, 0.25f).SetEase(Ease.InBack).OnComplete(() => 
        {
            CloseError?.Invoke();
            Destroy(gameObject);
        });
    }

    public void SetText(string text)
    {
        errorMessage.text = text;
    }
}
