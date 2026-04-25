using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashboardLoadingDataFade : MonoBehaviour
{
    [SerializeField] private GameObject loadingObject;
    [SerializeField] private Image loadingBackground;
    [SerializeField] private TextMeshProUGUI loadingText;

    // Start is called before the first frame update
    void Start()
    {
        UserData.Instance.LoadingData += OnLoadingData;
        UserData.Instance.FinishLoading += OnFinishLoading;   
    }

    private void OnLoadingData()
    {
        loadingObject.SetActive(true);
    }

    private void OnFinishLoading()
    {
        Sequence seq = DOTween.Sequence();

        seq.Join(loadingText.DOFade(0, 0.5f));
        seq.Append(loadingBackground.DOFade(0, 1));

        //seq.Play();
        //throw new NotImplementedException();
    }
}
