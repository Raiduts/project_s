using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Navigate : MonoBehaviour
{
    [Header("UI")]
    private Canvas canvas;
    [SerializeField]
    private RectTransform homePanel;

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
        homePanel.DOAnchorPosY(500, 0.5f);
    }

    public void ShowNav()
    {
        homePanel.DOAnchorPosY(-540, 0.5f);
    }

    public void CreateSetting()
    {
        Instantiate(settingPref, canvas.transform).OnCloseSetting += ShowNav;
        HideNav();
    }

    public void BackHome()
    {
        WarningPopper.Instance.ShowWarning("Yakin ingin kembali?", () =>
        {
            MySceneManager.instance.ChangeScene("Dashboard");
        });
    }

    public void ExitGame()
    {
        WarningPopper.Instance.ShowWarning("Kamu yakin ingin keluar?", () =>
        {
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
