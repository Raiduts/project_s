using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class AuthUIBinder : MonoBehaviour
{
    [Header("Login")]
    public GameObject loginPanel;
    public TMP_InputField gmailTextField;
    public TMP_InputField passwordTextField;

    [Header("Register")]
    public GameObject registerPanel;
    public TMP_InputField newUsernameTextField;
    public TMP_InputField newGmailTextField;
    public TMP_InputField newPasswordTextField;
    public TMP_InputField confirmPasswordTextField;

    [Header("UI")]
    //public TextMeshProUGUI usernameText;
    public Image authPanel;
    public RectTransform closeButton;

    void Start()
    {
        AuthManager.Instance.BindUI(this);
    }

    #region BUTTON

    public void OnClickLogin()
    {
        AuthManager.Instance.Login(
            gmailTextField.text,
            passwordTextField.text
        );
    }

    public void OnClickRegister()
    {
        if (newPasswordTextField.text != confirmPasswordTextField.text)
        {
            Debug.LogError("Password not match");
            return;
        }

        AuthManager.Instance.Register(
            newUsernameTextField.text,
            newGmailTextField.text,
            newPasswordTextField.text
        );
    }

    public void OnClickLogout()
    {
        AuthManager.Instance.Logout();
    }

    public void ChangePage(bool isLogin)
    {
        loginPanel.SetActive(isLogin);
        registerPanel.SetActive(!isLogin);
    }

    #endregion

    #region UI ANIMATION

    public void OpenAuth()
    {
        authPanel.gameObject.SetActive(true);

        authPanel.DOFade(1, 0.5f).From(0);

        loginPanel.transform.DOLocalMoveY(0, 0.5f).From(1500).SetEase(Ease.OutBack);
        registerPanel.transform.DOLocalMoveY(0, 0.5f).From(1500).SetEase(Ease.OutBack);
        closeButton.DOAnchorPosX(-384, 0.5f).SetEase(Ease.OutBack);
    }

    public void CloseAuth()
    {
        loginPanel.transform.DOLocalMoveY(1500, 0.5f);
        registerPanel.transform.DOLocalMoveY(1500, 0.5f);
        closeButton.DOAnchorPosX(384, 0.5f).SetEase(Ease.OutBack);

        authPanel.DOFade(0, 0.5f).OnComplete(() =>
        {
            authPanel.gameObject.SetActive(false);
        });
    }

    #endregion
}