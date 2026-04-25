using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ErrorPopper : MonoBehaviour
{
    public static ErrorPopper Instance;

    [SerializeField] private ErrorPopUp errorPopUpPref;
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

    public void ShowError(string message)
    {
        ErrorPopUp errorPopUpTemp = Instantiate(errorPopUpPref, container);
        errorPopUpTemp.SetText(message);
        errorPopUpTemp.CloseError += FadeOutBackground;

        FadeInBackground();
    }

    public void FadeInBackground()
    {
        containerBackground.raycastTarget = true;
        
        containerBackground.DOFade(0.9f, 0.25f).OnComplete(()=> 
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
