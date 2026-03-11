using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Navigate : MonoBehaviour
{
    [Header("UI")]
    private Canvas canvas;
    [SerializeField]
    private Transform homePanel;

    [Header("Prefabs")]
    [SerializeField]
    private Setting settingPref;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
    }

    public void HideNav()
    {
        homePanel.DOLocalMoveY(500, 0.5f);
    }

    public void ShowNav()
    {
        homePanel.DOLocalMoveY(0, 0.5f);
    }

    public void CreateSetting()
    {
        Instantiate(settingPref, canvas.transform).OnCloseSetting += ShowNav;
        HideNav();
    }

    public void BackHome()
    {
        MySceneManager.instance.ChangeScene("Dashboard");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
