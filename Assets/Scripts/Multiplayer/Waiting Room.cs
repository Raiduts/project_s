using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.UI;
using TMPro;
using System;

public class WaitingRoom : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI codeText;
    public Button startButton;
    public Transform playerContainer;
    public GameObject playerItemPrefab;

    private FirebaseFirestore db;
    [SerializeField]
    private QuizSceneManager quizSceneManager;

    private string roomCode;
    private string roomCreatorEmail;
    private string currentUserEmail;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;

        roomCode = PlayerPrefs.GetString("Code");
        currentUserEmail = AuthManager.Instance.User().Email; 

        codeText.text = roomCode;
        startButton.gameObject.SetActive(false);
        startButton.onClick.AddListener(OnClickStart);

        CheckRoomOwner();
        LoadPlayers();
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

    void LoadPlayers()
    {
        CollectionReference leaderboardRef = db
            .Collection("Quizzes")
            .Document(roomCode)
            .Collection("Leaderboard");

        leaderboardRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                QuerySnapshot snapshot = task.Result;

                foreach (DocumentSnapshot doc in snapshot.Documents)
                {
                    string playerId = doc.Id;
                    string playerName = doc.GetValue<string>("name");

                    print(playerName);

                    // skip creator
                    if (playerId == currentUserEmail)
                        continue;

                    SpawnPlayer(playerName);
                }
            }
        });
    }

    void SpawnPlayer(string playerName)
    {
        GameObject obj = Instantiate(playerItemPrefab, playerContainer);

        // asumsi prefab punya script sendiri
        PlayerItem item = obj.GetComponent<PlayerItem>();
        if (item != null)
        {
            item.SetData(playerName);
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