using System;
using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class AuthManager : MonoBehaviour
{
    public static AuthManager Instance;

    [Header("Firebase")]
    private FirebaseAuth auth;
    private FirebaseUser user;

    [Header("Login")]
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private TMP_InputField gmailTextField;
    [SerializeField] private TMP_InputField passwordTextField;

    [Header("Register")]
    [SerializeField] private GameObject registerPanel;
    [SerializeField] private TMP_InputField newUsernameTextField;
    [SerializeField] private TMP_InputField newGmailTextField;
    [SerializeField] private TMP_InputField newPasswordTextField;
    [SerializeField] private TMP_InputField confirmPasswordTextField;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI usernameText;
    [SerializeField] private Image authPanel;
    [SerializeField] private RectTransform closeButton;

    public Action LoggedIn, TryLogOut;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Firebase Dependency Error: " + task.Result);
            }
        });

        if (user == null)
        {
            print("No User");
            authPanel.gameObject.SetActive(true);
        }
    }

    public FirebaseUser User
    {
        get
        {
            if (user == null)
                Debug.LogWarning("User is NULL, not logged in yet!");

            return user;
        }
    }

    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
    }

    private void AuthStateChanged(object sender, EventArgs e)
    {
        if (auth.CurrentUser == user) return;

        user = auth.CurrentUser;

        bool signedIn = user != null;

        usernameText.gameObject.SetActive(signedIn);

        if (signedIn)
        {
            usernameText.text = $"Hello, {user.DisplayName}";
            Debug.Log($"Signed In: {user.Email}");
            CloseAuth();
            LoggedIn?.Invoke();
        }
        else
        {
            Debug.Log("Signed Out");
            OpenAuth();
        }
    }

    #region LOGIN

    public void Login()
    {
        StartCoroutine(LoginAsync(gmailTextField.text, passwordTextField.text));
    }

    private IEnumerator LoginAsync(string email, string password)
    {
        var task = auth.SignInWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Exception != null)
        {
            HandleAuthError(task.Exception, "Login Failed");
        }
        else
        {
            Debug.Log("Login Success");
        }
    }

    #endregion

    #region REGISTER

    public void Register()
    {
        if (!ValidateRegister()) return;

        StartCoroutine(RegisterAsync(
            newUsernameTextField.text,
            newGmailTextField.text,
            newPasswordTextField.text));
    }

    private bool ValidateRegister()
    {
        if (string.IsNullOrEmpty(newUsernameTextField.text))
        {
            Debug.LogError("Username empty");
            return false;
        }

        if (string.IsNullOrEmpty(newGmailTextField.text))
        {
            Debug.LogError("Email empty");
            return false;
        }

        if (newPasswordTextField.text != confirmPasswordTextField.text)
        {
            Debug.LogError("Password not match");
            return false;
        }

        return true;
    }

    private IEnumerator RegisterAsync(string name, string email, string password)
    {
        var task = auth.CreateUserWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Exception != null)
        {
            HandleAuthError(task.Exception, "Register Failed");
            yield break;
        }

        user = task.Result.User;

        var profile = new UserProfile { DisplayName = name };
        var profileTask = user.UpdateUserProfileAsync(profile);

        yield return new WaitUntil(() => profileTask.IsCompleted);

        if (profileTask.Exception != null)
        {
            HandleAuthError(profileTask.Exception, "Profile Update Failed");
            user.DeleteAsync();
        }
        else
        {
            Debug.Log($"Register Success: {name}");
        }
    }

    #endregion

    #region LOGOUT

    public void Logout()
    {
        if (auth == null) return;

        auth.SignOut();
        user = null;

        usernameText.text = "";
        usernameText.gameObject.SetActive(false);

        TryLogOut?.Invoke();
        OpenAuth();

        Debug.Log("Logout Success");
    }

    #endregion

    #region ERROR HANDLER

    private void HandleAuthError(AggregateException exception, string prefix)
    {
        FirebaseException firebaseEx = exception.GetBaseException() as FirebaseException;

        if (firebaseEx == null)
        {
            Debug.LogError(prefix);
            return;
        }

        AuthError error = (AuthError)firebaseEx.ErrorCode;

        string message = prefix + ": ";

        switch (error)
        {
            case AuthError.InvalidEmail: message += "Invalid Email"; break;
            case AuthError.WrongPassword: message += "Wrong Password"; break;
            case AuthError.MissingEmail: message += "Missing Email"; break;
            case AuthError.MissingPassword: message += "Missing Password"; break;
            default: message += "Unknown Error"; break;
        }

        Debug.LogError(message);
    }

    #endregion

    #region UI

    public void ChangePage(bool isLogin)
    {
        loginPanel.SetActive(isLogin);
        registerPanel.SetActive(!isLogin);
    }

    public void OpenAuth()
    {
        print("Opening");

        authPanel.gameObject.SetActive(true);

        authPanel.DOFade(1, 0.5f).From(0);

        loginPanel.transform.DOLocalMoveY(0, 0.5f).From(1500).SetEase(Ease.OutBack);
        registerPanel.transform.DOLocalMoveY(0, 0.5f).From(1500).SetEase(Ease.OutBack);
        closeButton.DOAnchorPosX(-384, 0.5f).SetEase(Ease.OutBack);
    }

    public void CloseAuth()
    {
        print("Closing");

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