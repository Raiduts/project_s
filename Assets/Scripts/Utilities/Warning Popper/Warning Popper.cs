using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WarningPopper : MonoBehaviour
{
    public static WarningPopper Instance;

    [SerializeField] private WarningPopUp warningPopUpPref;
    [SerializeField] private Transform container;
    private Image containerBackground;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        containerBackground = container.GetComponent<Image>();
    }

    public void ShowWarning(string message, Action onYesAction)
    {
        WarningPopUp warnPopUpTemp = Instantiate(warningPopUpPref, container);
        warnPopUpTemp.SetText(message);

        warnPopUpTemp.closeWarning += FadeOutBackground;

        FadeInBackground();

        warnPopUpTemp.confirmButton.onClick.AddListener(() =>
        {
            onYesAction?.Invoke();
            warnPopUpTemp.Hide();
        });
    }

    public void FadeInBackground()
    {
        containerBackground.raycastTarget = true;

        containerBackground.DOFade(0.9f, 0.25f).OnComplete(() =>
        {

        });
    }

    public void FadeOutBackground()
    {
        containerBackground.DOFade(0, 0.25f).OnComplete(() =>
        {
            containerBackground.raycastTarget = false;
        });
    }
}
