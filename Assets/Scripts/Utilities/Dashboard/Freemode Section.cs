using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FreemodeSection : MonoBehaviour
{
    [SerializeField]
    private Canvas freemodeCanvas;
    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private Transform panelTransform;

    private void Start()
    {
        
    }

    public void OpenFreemodeSection()
    {
        freemodeCanvas.gameObject.SetActive(true);
        backgroundImage.DOFade(0.75f, 0.5f).From(0);
        panelTransform.DOLocalMoveY(0, 0.5f).From(1500).SetEase(Ease.OutBack);
    }

    public void CloseFreemodeSection()
    {
        backgroundImage.DOFade(0, 0.5f);
        panelTransform.DOLocalMoveY(1500, 0.5f).SetEase(Ease.InQuad).OnComplete(() => 
        {
            freemodeCanvas.gameObject.SetActive(false);
        });
    }

    public void EnterFreemode(string sceneName)
    {
        CloseFreemodeSection();
        MySceneManager.instance.ChangeScene(sceneName);
    }
}
