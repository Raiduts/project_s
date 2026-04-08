using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private Image settingPanel;
    [SerializeField]
    private Button backButton;

    public Action OnCloseSetting;

    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(CloseSetting);

        OpenSetting();
    }

    private void OpenSetting()
    {
        backgroundImage.DOFade(0, 0.5f).From();
        settingPanel.transform.DOMoveY(2000, 0.5f).From();
        backButton.transform.DOMoveX(-1000, 0.5f).From();
    }

    private void CloseSetting()
    {
        OnCloseSetting?.Invoke();
        backgroundImage.DOFade(0, 0.5f);
        settingPanel.transform.DOMoveY(2000, 0.5f);
        backButton.transform.DOMoveX(-1000, 0.5f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    public void Logout()
    {
        AuthManager.Instance.Logout();
        CloseSetting();
    }
}
