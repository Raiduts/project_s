using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using System;

public class UserData : MonoBehaviour
{
    public static UserData Instance;

    private FirebaseAuth auth;
    private FirebaseFirestore db;

    //private bool hasLoaded = false;

    [Header("Data")]
    // Profile Data
    public int iconIndex, score, level = 1;

    // Campaign Data
    public int arrayLevel, linkedlistLevel, stackLevel, queueLevel;
    public bool completedArray, completedLinkedlist, completedStack, completedQueue;
    public DSType campaignDSType;

    // Event
    public Action DataUpdated, DataSaved, FinishLoading, LoadingData;

    public bool isNewAccount, isSaving, isLoading;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        db = FirebaseFirestore.DefaultInstance;

        auth.StateChanged += OnStateChanged;
    }

    private void OnStateChanged(object sender, EventArgs e)
    {
        print("user data");

        if (auth.CurrentUser != null)
        {
            //hasLoaded = true;
            LoadProgress();
        }
    }

    // 🔥 SAVE DATA
    public void SaveProgress()
    {
        if (auth.CurrentUser == null || isSaving) return;

        isSaving = true;

        string email = auth.CurrentUser.Email;

        DocumentReference docRef = db.Collection("Users").Document(email);

        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            // Profile
            { "iconIndex", iconIndex },
            //{ "level", level},

            // Level
            { "arrayLevel", arrayLevel },
            { "linkedlistLevel", linkedlistLevel },
            { "stackLevel", stackLevel },
            { "queueLevel", queueLevel },

            // Completed
            { "completedArray", completedArray },
            { "completedLinkedlist", completedLinkedlist },
            { "completedStack", completedStack },
            { "completedQueue", completedQueue },
        };

        docRef.SetAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Progress berhasil disimpan");

                DataSaved?.Invoke();

                if (isNewAccount)
                {
                    isNewAccount = false;

                    DataUpdated?.Invoke();
                }
            }
            else
            {
                Debug.LogError("Gagal save: " + task.Exception);
            }
        });

        SaveFinalScore(score);

        CalculateLevel();
    }

    // 🔥 LOAD DATA
    public void LoadProgress()
    {
        if (auth.CurrentUser == null || isLoading) return;

        isLoading = true;

        LoadingData?.Invoke();

        //print("Load Data 1");

        string email = auth.CurrentUser.Email;
        DocumentReference docRef = db.Collection("Users").Document(email);

        // Load Personal Data
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DocumentSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    //print("Progress loading");

                    // Profile
                    iconIndex = snapshot.ContainsField("iconIndex") ? snapshot.GetValue<int>("iconIndex") : 0;
                    //level = snapshot.ContainsField("level") ? snapshot.GetValue<int>("level") : 1;

                    // Level
                    arrayLevel = snapshot.ContainsField("arrayLevel") ? snapshot.GetValue<int>("arrayLevel") : 0;
                    linkedlistLevel = snapshot.ContainsField("linkedlistLevel") ? snapshot.GetValue<int>("linkedlistLevel") : 0;
                    stackLevel = snapshot.ContainsField("stackLevel") ? snapshot.GetValue<int>("stackLevel") : 0;
                    queueLevel = snapshot.ContainsField("queueLevel") ? snapshot.GetValue<int>("queueLevel") : 0;

                    // Completed
                    completedArray = snapshot.ContainsField("completedArray") ? snapshot.GetValue<bool>("completedArray") : false;
                    completedLinkedlist = snapshot.ContainsField("completedLinkedlist") ? snapshot.GetValue<bool>("completedLinkedlist") : false;
                    completedStack = snapshot.ContainsField("completedStack") ? snapshot.GetValue<bool>("completedStack") : false;
                    completedQueue = snapshot.ContainsField("completedQueue") ? snapshot.GetValue<bool>("completedQueue") : false;

                    // Enum (string → enum)
                    //if (snapshot.ContainsField("campaignDSType"))
                    //{
                    //    string typeString = snapshot.GetValue<string>("campaignDSType");
                    //    Enum.TryParse(typeString, out campaignDSType);
                    //}
                    //else
                    //{
                    //    campaignDSType = DSType.Array;
                    //}

                    //print("Progress berhasil diload");

                    DataUpdated?.Invoke();

                    isNewAccount = false;
                }
                else
                {
                    print("Data belum ada, bikin baru");

                    //ResetProgress();
                    //SaveProgress();

                    isNewAccount = true;
                }
            }
            else
            {
                print("Gagal load: " + task.Exception);
            }
        });

        // Load personal score
        DocumentReference leaderboardRef = db.Collection("Leaderboard").Document(email);

        leaderboardRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DocumentSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    score = snapshot.GetValue<int>("score");
                }

                // Calculate Level
                CalculateLevel();

                isLoading = true;
            }
        });
    }

    public void ResetProgress()
    {
        iconIndex = 0;
        score = 0;
        //level = snapshot.ContainsField("level") ? snapshot.GetValue<int>("level") : 1;

        // Level
        arrayLevel = 0;
        linkedlistLevel = 0;
        stackLevel = 0;
        queueLevel = 0;

        // Completed
        completedArray = false;
        completedLinkedlist = false;
        completedStack = false;
        completedQueue = false;

        SaveProgress();
    }

    public void AddScore(int score)
    {
        this.score += score;

        SaveProgress();
    }

    private void CalculateLevel()
    {
        //print(score);

        level = (score / 1000) + 1;

        FinishLoading?.Invoke();
    }

    public void SaveFinalScore(int score)
    {
        string email = auth.CurrentUser.Email;

        string playerName = auth.CurrentUser.DisplayName;

        DocumentReference docRef = db.Collection("Leaderboard").Document(email);

        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result.Exists)
            {
                int oldScore = task.Result.GetValue<int>("score");

                if (score >= oldScore)
                {
                    docRef.SetAsync(new Dictionary<string, object>()
                    {
                        { "name", playerName },
                        { "score", score },
                        { "iconIndex", iconIndex }
                    });
                    AuthManager.Instance.LoggedIn?.Invoke();
                    isSaving = false;
                }
            }
            else
            {
                docRef.SetAsync(new Dictionary<string, object>()
                {
                    { "name", playerName },
                    { "score", score },
                    { "iconIndex", iconIndex }
                });
                isSaving = false;
            }
        });
    }
}

public enum DSType
{
    Array,
    Linkedlist,
    Stack,
    Queue
}