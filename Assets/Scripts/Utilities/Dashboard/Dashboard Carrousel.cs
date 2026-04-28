using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DashboardCarrousel : MonoBehaviour
{
    private void Start()
    {
        int pageOnOpen = PlayerPrefs.GetInt("Dashboard Page");

        DoChangeTab(pageOnOpen, true);
    }

    public void ChangeTab(int page)
    {
        DoChangeTab(page);
    }

    public void DoChangeTab(int page, bool skip = false)
    {
        float targetX = 0;
        DashboardPage pageType = (DashboardPage) page;
     
        switch (pageType)
        {
            case DashboardPage.Leaderboard:
                FindAnyObjectByType<Navigate>().HideNav();
                PlayerPrefs.SetInt("Dashboard Page", 0);
                targetX = 2778;
                break;
            case DashboardPage.Home:
                FindAnyObjectByType<Navigate>().ShowNav();
                PlayerPrefs.SetInt("Dashboard Page", 1);
                targetX = 0;
                break;
            case DashboardPage.Game:
                FindAnyObjectByType<Navigate>().HideNav();
                PlayerPrefs.SetInt("Dashboard Page", 2);
                targetX = -2778;
                break;
        }

        EventListener.ChangeDashboardPage?.Invoke(pageType);

        transform.DOLocalMoveX(targetX, skip ? 0 : 0.5f).SetEase(Ease.OutBack);
    }
}
