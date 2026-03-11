using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase;
using TMPro;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class AuthManager : MonoBehaviour
{
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public static FirebaseAuth auth;
    public static FirebaseUser user;

    [Space]
    [Header("login")]
    public GameObject loginPanel;
    public TMP_InputField gmailTextField;
    public TMP_InputField passwordTextField;

    [Space]
    [Header("Register")]
    public GameObject registerPanel;
    public TMP_InputField newUsernameTextField;
    public TMP_InputField newGmailTextField;
    public TMP_InputField newPasswordTextField;
    public TMP_InputField confirmPasswordTextField;

    [Space]
    [Header("Others")]
    public TextMeshProUGUI usernameText;
    public Image authPanel;

    //private void Awake()
    //{
    //    FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
    //    {
    //        dependencyStatus = task.Result;

    //        if (dependencyStatus == DependencyStatus.Available)
    //        {
    //            InitializeFirebase();
    //            print("Bisa");
    //        }
    //        else
    //        {
    //            Debug.LogError("Could not resolve :" + dependencyStatus.ToString());
    //        }
    //    });
    //}

    public void ChangePage(bool isLogin)
    {
        loginPanel.SetActive(isLogin);
        registerPanel.SetActive(!isLogin);
    }

    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    private void AuthStateChanged(object sender, EventArgs e)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            usernameText.gameObject.SetActive(signedIn);

            if (signedIn && user != null)
            {
                print("Signed Out " + user.UserId);
            }

            user = auth.CurrentUser;

            if (signedIn)
            {
                print("Signed In " + user.DisplayName + " " + user.Email);
                usernameText.text = $"Hello, {user.DisplayName}!";
            }
        }
    }

    public void Login()
    {
        StartCoroutine(LoginAsync(gmailTextField.text, passwordTextField.text));
    }

    private IEnumerator LoginAsync(string email, string password)
    {
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            Debug.LogError(loginTask.Exception);

            FirebaseException firebaseException = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError authError = (AuthError)firebaseException.ErrorCode;


            string failedMessage = "Login Failed! Because ";

            switch (authError)
            {
                case AuthError.InvalidEmail:
                    failedMessage += "Email is invalid";
                    break;
                case AuthError.WrongPassword:
                    failedMessage += "Wrong Password";
                    break;
                case AuthError.MissingEmail:
                    failedMessage += "Email is missing";
                    break;
                case AuthError.MissingPassword:
                    failedMessage += "Password is missing";
                    break;
                default:
                    failedMessage = "Login Failed";
                    break;
            }

            Debug.Log(failedMessage);
        }
        else
        {
            user = loginTask.Result.User;

            Debug.LogFormat("{0} You Are Successfully Logged In", user.DisplayName);

            //References.userName = user.DisplayName;
            //UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }
    }

    public void Register()
    {
        StartCoroutine(RegisterAsync(newUsernameTextField.text, newGmailTextField.text, newPasswordTextField.text, confirmPasswordTextField.text));
    }

    private IEnumerator RegisterAsync(string name, string email, string password, string confirmPassword)
    {
        if (name == "")
        {
            Debug.LogError("User Name is empty");
        }
        else if (email == "")
        {
            Debug.LogError("email field is empty");
        }
        else if (newPasswordTextField.text != confirmPasswordTextField.text)
        {
            Debug.LogError("Password does not match");
        }
        else
        {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(() => registerTask.IsCompleted);

            if (registerTask.Exception != null)
            {
                Debug.LogError(registerTask.Exception);

                FirebaseException firebaseException = registerTask.Exception.GetBaseException() as FirebaseException;
                AuthError authError = (AuthError)firebaseException.ErrorCode;

                string failedMessage = "Registration Failed! Becuase ";
                switch (authError)
                {
                    case AuthError.InvalidEmail:
                        failedMessage += "Email is invalid";
                        break;
                    case AuthError.WrongPassword:
                        failedMessage += "Wrong Password";
                        break;
                    case AuthError.MissingEmail:
                        failedMessage += "Email is missing";
                        break;
                    case AuthError.MissingPassword:
                        failedMessage += "Password is missing";
                        break;
                    default:
                        failedMessage = "Registration Failed";
                        break;
                }

                Debug.Log(failedMessage);
            }
            else
            {
                // Get The User After Registration Success
                user = registerTask.Result.User;

                UserProfile userProfile = new UserProfile { DisplayName = name };

                var updateProfileTask = user.UpdateUserProfileAsync(userProfile);

                yield return new WaitUntil(() => updateProfileTask.IsCompleted);

                if (updateProfileTask.Exception != null)
                {
                    // Delete the user if user update failed
                    user.DeleteAsync();

                    Debug.LogError(updateProfileTask.Exception);

                    FirebaseException firebaseException = updateProfileTask.Exception.GetBaseException() as FirebaseException;
                    AuthError authError = (AuthError)firebaseException.ErrorCode;


                    string failedMessage = "Profile update Failed! Becuase ";
                    switch (authError)
                    {
                        case AuthError.InvalidEmail:
                            failedMessage += "Email is invalid";
                            break;
                        case AuthError.WrongPassword:
                            failedMessage += "Wrong Password";
                            break;
                        case AuthError.MissingEmail:
                            failedMessage += "Email is missing";
                            break;
                        case AuthError.MissingPassword:
                            failedMessage += "Password is missing";
                            break;
                        default:
                            failedMessage = "Profile update Failed";
                            break;
                    }

                    Debug.Log(failedMessage);
                }
                else
                {
                    Debug.Log("Registration Sucessful Welcome " + user.DisplayName);
                    //UIManager.Instance.OpenLoginPanel();
                }
            }
        }
    }

    public void OpenAuth()
    {
        authPanel.gameObject.SetActive(true);

        authPanel.DOFade(1, 0.5f).From(0);

        loginPanel.transform.DOLocalMoveY(0, 0.5f).From(1500).SetEase(Ease.OutBack);
        registerPanel.transform.DOLocalMoveY(0, 0.5f).From(1500).SetEase(Ease.OutBack);
    }

    public void CloseAuth()
    {
        loginPanel.transform.DOLocalMoveY(1500, 0.5f);
        registerPanel.transform.DOLocalMoveY(1500, 0.5f);

        authPanel.DOFade(0, 0.5f).From(1).OnComplete(() => 
        { 
            authPanel.gameObject.SetActive(false);
        });
    }
}
