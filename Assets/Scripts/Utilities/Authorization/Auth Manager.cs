using System;
using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using System.Collections.Generic;

public class AuthManager : MonoBehaviour
{
    public static AuthManager Instance;

    private bool isInitialized = false;

    private FirebaseAuth auth;
    private FirebaseUser user;

    // UI reference (dinamis, dari scene)
    private AuthUIBinder ui;

    public Action LoggedIn, TryLogOut;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (!isInitialized)
        {
            InitFirebase();
        }
    }

    public FirebaseUser User()
    {
        return user;
    }

    void InitFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                InitializeFirebase();
                isInitialized = true;
                Debug.Log("Firebase Initialized");
            }
            else
            {
                Debug.LogError("Firebase Dependency Error: " + task.Result);
            }
        });
    }

    private void InitializeFirebase()
    {
        if (auth != null) return;

        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
    }

    // 🔥 DIPANGGIL DARI UI
    public void BindUI(AuthUIBinder binder)
    {
        ui = binder;

        Debug.Log("UI Bound");

        // refresh state biar UI langsung update
        AuthStateChanged(this, EventArgs.Empty);
    }

    private void AuthStateChanged(object sender, EventArgs e)
    {
        Debug.Log(auth);

        if (auth == null) return;

        //if (auth.CurrentUser == user) return;

        user = auth.CurrentUser;

        bool signedIn = user != null;

        if (ui == null)
        {
            Debug.LogWarning("UI belum di-bind");
            return;
        }

        //ui.usernameText.gameObject.SetActive(signedIn);

        if (signedIn)
        {
            ui.CloseAuth();
            LoggedIn?.Invoke();
        }
        else
        {
            ui.OpenAuth();
        }
    }

    #region LOGIN

    public void Login(string email, string password)
    {
        StartCoroutine(LoginAsync(email, password));
    }

    private IEnumerator LoginAsync(string email, string password)
    {
        var task = auth.SignInWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Exception != null)
        {
            ErrorPopper.Instance.ShowError("Gagal melakukan Login!");
        }
        else
        {
            Debug.Log("Login Success");

        }
    }

    #endregion

    #region REGISTER

    public void Register(string name, string email, string password)
    {
        StartCoroutine(RegisterAsync(name, email, password));
    }

    private IEnumerator RegisterAsync(string name, string email, string password)
    {
        var task = auth.CreateUserWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Exception != null)
        {
            ErrorPopper.Instance.ShowError("Gagal Melakukan Registrasi!");
            //Debug.LogError("Register Failed");
            yield break;
        }

        user = task.Result.User;

        var profile = new UserProfile { DisplayName = name };
        var profileTask = user.UpdateUserProfileAsync(profile);

        yield return new WaitUntil(() => profileTask.IsCompleted);

        if (profileTask.Exception != null)
        {
            Debug.LogError("Profile Update Failed");
            user.DeleteAsync();
        }
        else
        {
            Debug.Log($"Register Success: {name}");

            UserData.Instance.LoadProgress();
            //MySceneManager.instance.ChangeScene("Dashboard");
            //Login(email, password);
        }
    }

    #endregion

    #region LOGOUT

    public void Logout()
    {
        if (auth == null) return;

        auth.SignOut();
        user = null;

        if (ui != null)
        {
            //ui.usernameText.text = "";
            //ui.usernameText.gameObject.SetActive(false);
            ui.OpenAuth();
        }

        TryLogOut?.Invoke();

        Debug.Log("Logout Success");
    }

    #endregion
}