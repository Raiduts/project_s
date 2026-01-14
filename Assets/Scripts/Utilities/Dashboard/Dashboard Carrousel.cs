using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DashboardCarrousel : MonoBehaviour
{
    public void ChangeTab(float targetX)
    {
        transform.DOLocalMoveX(-targetX,0.5f).SetEase(Ease.OutBack);
    }
}
