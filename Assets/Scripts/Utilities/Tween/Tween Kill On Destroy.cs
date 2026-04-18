using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TweenKillOnDestroy : MonoBehaviour
{
    private DOTweenAnimation[] tweens;

    private void Start()
    {
        tweens = GetComponents<DOTweenAnimation>();
    }

    private void OnDestroy()
    {
        foreach (DOTweenAnimation item in tweens)
        {
            item.DOKill(transform);
        }
    }
}
