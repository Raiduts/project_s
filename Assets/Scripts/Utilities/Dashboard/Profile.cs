using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class Profile : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private RectTransform profilePanel;
    [SerializeField] private RectTransform closeButton;

    private Vector2 hiddenPos;
    private Vector2 shownPos;

    private void Start()
    {
        backgroundImage.gameObject.SetActive(true);

        // Posisi awal (off screen bawah)
        shownPos = profilePanel.anchoredPosition;
        hiddenPos = shownPos + new Vector2(0, -1000f);

        profilePanel.anchoredPosition = hiddenPos;
        backgroundImage.color = new Color(0, 0, 0, 0);
        closeButton.DOAnchorPosY(128, 0.3f);

        EventListener.ChangeDashboardPage += OnChangeDashboardPage;
    }

    private void OnChangeDashboardPage(DashboardPage page)
    {
        CloseProfile();
    }

    public void OpenProfile()
    {
        // Fade background
        backgroundImage.DOFade(0.5f, 0.3f);
        closeButton.DOAnchorPosY(-128, 0.3f);

        // Muncul dari bawah
        profilePanel.DOAnchorPos(shownPos, 0.4f)
            .SetEase(Ease.OutBack);
    }

    public void CloseProfile()
    {
        // Fade out background
        backgroundImage.DOFade(0f, 0.3f);
        closeButton.DOAnchorPosY(128, 0.3f);

        // Turun lagi
        profilePanel.DOAnchorPos(hiddenPos, 0.3f)
            .SetEase(Ease.InBack);
    }
}