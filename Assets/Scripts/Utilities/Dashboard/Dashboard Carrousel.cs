using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DashboardCarrousel : MonoBehaviour
{
    public void ChangeTab(int page)
    {
        float targetX = 0;
        DashboardPage pageType = (DashboardPage) page;
     
        switch (pageType)
        {
            case DashboardPage.Leaderboard:
                targetX = 2778;
                break;
            case DashboardPage.Home:
                targetX = 0;
                break;
            case DashboardPage.Game:
                targetX = -2778;
                break;
        }

        EventListener.ChangeDashboardPage?.Invoke(pageType);

        transform.DOLocalMoveX(targetX, 0.5f).SetEase(Ease.OutBack);
    }
}
