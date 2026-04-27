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
        freemodeCanvas.gameObject.SetActive(true);

        Sequence seq = DOTween.Sequence();

        // Background fade (lebih smooth masuk)
        seq.Join(
            backgroundImage.DOFade(0.97f, 0.4f)
            .From(0)
            .SetEase(Ease.OutQuad)
        );

        // Panel masuk dari bawah + sedikit overshoot
        seq.Join(
            panelTransform.DOLocalMoveY(0, 0.6f)
            .From(1200)
            .SetEase(Ease.OutBack, 1.2f)
        );

        // Scale dikit biar kerasa "pop"
        panelTransform.localScale = Vector3.one * 0.9f;
        seq.Join(
            panelTransform.DOScale(1f, 0.5f)
            .SetEase(Ease.OutBack)
        );
    }

    public void CloseGameSection()
    {
        Sequence seq = DOTween.Sequence();

        // Background fade out (cepet tapi halus)
        seq.Join(
            backgroundImage.DOFade(0, 0.25f)
            .SetEase(Ease.InQuad)
        );

        // Panel turun + sedikit shrink
        seq.Join(
            panelTransform.DOLocalMoveY(1200, 0.35f)
            .SetEase(Ease.InBack)
        );

        seq.Join(
            panelTransform.DOScale(0.9f, 0.25f)
            .SetEase(Ease.InQuad)
        );

        seq.OnComplete(() =>
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

        //PlayerPrefs.SetInt("campaignType", campaignType);

        UserData.Instance.campaignDSType = (DSType) campaignType;

        MySceneManager.instance.ChangeScene("Campaign");
    }
}
