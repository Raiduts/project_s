using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.UI;
using TMPro;
using System;
using Firebase.Auth;

public class WaitingRoom : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI codeText;
    public Button startButton;
    public Transform playerContainer;
    public GameObject playerItemPrefab;

    private FirebaseFirestore db;
    private FirebaseAuth auth;

    [SerializeField]
    private QuizSceneManager quizSceneManager;

    private string roomCode;
    private string roomCreatorEmail;
    private string currentUserEmail;

    ListenerRegistration playerListener;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;

        roomCode = PlayerPrefs.GetString("Code");
        currentUserEmail = AuthManager.Instance.User().Email; 

        codeText.text = roomCode;
        startButton.gameObject.SetActive(false);
        startButton.onClick.AddListener(OnClickStart);

        JoinWaitingRoom();
        CheckRoomOwner();
        ListenPlayers();
    }

    public void JoinWaitingRoom()
    {
        string userEmail = auth.CurrentUser.Email;
        string userName = auth.CurrentUser.DisplayName;
        int iconIndex = PlayerPrefs.GetInt("IconIndex");

        if (userEmail == "" || userName == "")
        {
            userEmail = "anonymous@gmail.com";
            userName = "anonymous";
        }

        DocumentReference scoreRef = db.Collection("Quizzes").Document(roomCode)
                                       .Collection("Leaderboard").Document(userEmail);

        Dictionary<string, object> data = new Dictionary<string, object> {
            { "name", userName },
            { "iconIndex", iconIndex },
            { "lastUpdated", FieldValue.ServerTimestamp }
        };

        // SetAsync dengan Merge agar tidak menimpa data lain jika ada
        scoreRef.SetAsync(data, SetOptions.MergeAll);
    }

    void CheckRoomOwner()
    {
        DocumentReference roomRef = db.Collection("Quizzes").Document(roomCode);

        roomRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DocumentSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    string createdBy = snapshot.GetValue<string>("createdBy");

                    roomCreatorEmail = createdBy;
                    //print(createdBy + " and " + currentUserEmail);

                    if (IsCreator())
                    {
                        PlayerPrefs.SetInt("IsTeacher", 1);
                        startButton.gameObject.SetActive(true);
                        startButton.interactable = true;
                    }
                    else
                    {
                        PlayerPrefs.SetInt("IsTeacher", 0);
                        startButton.gameObject.SetActive(false);
                    }

                    quizSceneManager.ListenRoomStatus();
                }
            }
        });
    }

    public bool IsCreator()
    {
        return roomCreatorEmail == currentUserEmail;
    }

    void ListenPlayers()
    {
        CollectionReference leaderboardRef = db
            .Collection("Quizzes")
            .Document(roomCode)
            .Collection("Leaderboard");

        playerListener = leaderboardRef.Listen(snapshot =>
        {
            // clear dulu biar ga duplicate
            foreach (Transform child in playerContainer)
            {
                Destroy(child.gameObject);
            }

            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                string playerId = doc.Id;
                string playerName = doc.GetValue<string>("name");
                int playerIcon = doc.ContainsField("iconIndex") ? doc.GetValue<int>("iconIndex") : 0;

                // Debug.Log(playerName);

                // skip creator
                if (playerId == roomCreatorEmail)
                    continue;

                SpawnPlayer(playerName, playerIcon);
            }
        });
    }

    void SpawnPlayer(string playerName, int iconIndex)
    {
        GameObject obj = Instantiate(playerItemPrefab, playerContainer);

        // asumsi prefab punya script sendiri
        PlayerItem item = obj.GetComponent<PlayerItem>();

        if (item != null)
        {
            item.SetData(playerName, iconIndex);
        }
    }

    // dipanggil dari button
    public void OnClickStart()
    {
        db.Collection("Quizzes")
          .Document(roomCode)
          .UpdateAsync("status", "running")
          .ContinueWithOnMainThread(task =>
          {
              if (task.IsCompleted)
              {
                  Debug.Log("Status berhasil diubah ke RUNNING");
              }
              else
              {
                  Debug.LogError("Gagal update status: " + task.Exception);
              }
          });
    }
}