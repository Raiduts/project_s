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

    private bool hasLoaded = false;

    [Header("Data")]
    // Profile Data
    public int iconIndex;
    
    // Campaign Data
    public int arrayLevel, linkedlistLevel, stackLevel, queueLevel;
    public bool completedArray, completedLinkedlist, completedStack, completedQueue;
    public DSType campaignDSType;

    // Event
    public Action DataUpdated, DataSaved;

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
        if (auth.CurrentUser != null && !hasLoaded)
        {
            hasLoaded = true;
            LoadProgress();
        }
    }

    // 🔥 SAVE DATA
    public void SaveProgress()
    {
        if (auth.CurrentUser == null) return;

        string email = auth.CurrentUser.Email;
        DocumentReference docRef = db.Collection("Users").Document(email);

        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            // Profile
            { "iconIndex", iconIndex },

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

            // Enum (convert ke string biar aman)
            //{ "campaignDSType", campaignDSType.ToString() }
        };

        docRef.SetAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Progress berhasil disimpan");

                DataSaved?.Invoke();
            }
            else
            {
                Debug.LogError("Gagal save: " + task.Exception);
            }
        });
    }

    // 🔥 LOAD DATA
    public void LoadProgress()
    {
        if (auth.CurrentUser == null) return;

        print("Load Data 1");

        string email = auth.CurrentUser.Email;
        DocumentReference docRef = db.Collection("Users").Document(email);

        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DocumentSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    print("Progress loading");

                    // Profile
                    iconIndex = snapshot.ContainsField("iconIndex") ? snapshot.GetValue<int>("iconIndex") : 0;

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

                    print("Progress berhasil diload");

                    DataUpdated?.Invoke();
                }
                else
                {
                    print("Data belum ada, bikin baru");
                    SaveProgress();
                }
            }
            else
            {
                print("Gagal load: " + task.Exception);
            }
        });

        print("Load Data 3");
    }
}

public enum DSType
{
    Array,
    Linkedlist,
    Stack,
    Queue
}