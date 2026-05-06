using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DashboardMarker : MonoBehaviour
{
    public static DashboardMarker Instance;

    private RectTransform markerTransform;

    //public RectTransform targetLocationTest;

    private Sequence markerSeq;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        markerTransform = GetComponent<RectTransform>();

        //MoveToTarget(targetLocationTest);
    }

    public void MoveToTarget(RectTransform parent)
    {
        markerTransform.localScale = Vector3.one;

        markerTransform.SetParent(parent);

        markerSeq?.Kill();

        markerSeq = DOTween.Sequence();

        markerSeq.Join(markerTransform.DOAnchorPos(new Vector3(0, 64, 0), 0.25f).SetEase(Ease.OutBack));

        markerSeq.Append(markerTransform.DOAnchorPosY(80, 1).SetLoops(int.MaxValue, LoopType.Yoyo));
    }

    private void OnDestroy()
    {
        markerSeq.Kill();
    }
}   
