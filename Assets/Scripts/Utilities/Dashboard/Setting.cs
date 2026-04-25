using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image settingPanel;
    [SerializeField] private Button backButton;
    [SerializeField] private Slider musicSlider, sfxSlider;

    public Action OnCloseSetting;

    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(CloseSetting);

        musicSlider.value = AudioManager.Instance.musicVolume;
        sfxSlider.value = AudioManager.Instance.sfxVolume;

        musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSfxVolume);

        OpenSetting();
    }

    private void OpenSetting()
    {
        backgroundImage.DOFade(0, 0.5f).From();
        settingPanel.rectTransform.DOAnchorPosY(1000, 0.5f).SetEase(Ease.OutBack).From();
        //backButton.transform.DOMoveX(-1000, 0.5f).From();
    }

    private void CloseSetting()
    {
        backgroundImage.DOFade(0, 0.5f);
        settingPanel.rectTransform.DOAnchorPosY(1000, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            OnCloseSetting?.Invoke();
            //backgroundImage.DOKill();
            Destroy(gameObject);
        });
        //backButton.transform.DOMoveX(-1000, 0.5f)
    }

    public void Logout()
    {
        AuthManager.Instance.Logout();
        CloseSetting();
    }
}
