using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WarningPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI warnMessage;
    public Button confirmButton, closeButton;

    public Action closeWarning;

    private void Start()
    {
        closeButton.onClick.AddListener(Hide);

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
            closeWarning?.Invoke();
            Destroy(gameObject);
        });
    }

    public void SetText(string text)
    {
        warnMessage.text = text;
    }
}
