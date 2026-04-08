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
    [SerializeField]
    private CreateSessionTeacher createSessionTeacherPref;
    [SerializeField]
    private JoinSessionStudent joinSessionStudentPref;

    public void OpenGameSection()
    {
        print($"Open {gameObject.name}");
        freemodeCanvas.gameObject.SetActive(true);
        backgroundImage.DOFade(0.75f, 0.5f).From(0);
        panelTransform.DOLocalMoveY(0, 0.5f).From(1500).SetEase(Ease.OutBack);
    }

    public void CloseGameSection()
    {
        backgroundImage.DOFade(0, 0.25f);
        panelTransform.DOLocalMoveY(1500, 0.25f).SetEase(Ease.InQuad).OnComplete(() => 
        {
            freemodeCanvas.gameObject.SetActive(false);
        });
    }

    public void OpenCreateTab()
    {
        Instantiate(createSessionTeacherPref);
    }

    public void OpenJoinTab()
    {
        Instantiate(joinSessionStudentPref);
    }

    public void EnterFreemode(string sceneName)
    {
        CloseGameSection();
        MySceneManager.instance.ChangeScene(sceneName);
    }

    public void EnterCampaign(int campaignType)
    {
        CloseGameSection();

        PlayerPrefs.SetInt("campaignType", campaignType);
        MySceneManager.instance.ChangeScene("Campaign");
    }
}
