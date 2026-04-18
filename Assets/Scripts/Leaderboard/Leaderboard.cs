using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public static bool IsReady = false;

    public Transform container;
    public UserScore userScorePref;

    public void Start()
    {
        AuthManager.Instance.LoggedIn += GetTop10;
    }

    public void AddLeaderboard()
    {
        int point = Random.Range(0, 100) * 10;

        SaveFinalScore(point);
    }

    public void GetTop10()
    {
        foreach (Transform item in container)
        {
            Destroy(item.gameObject);
        }

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        db.Collection("Leaderboard")
        .OrderByDescending("score")
        .Limit(10)
        .GetSnapshotAsync()
        .ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                int rank = 1;

                foreach (var doc in task.Result.Documents)
                {
                    string name = doc.GetValue<string>("name");

                    int score = doc.GetValue<int>("score");

                    UserScore tempUserScore = Instantiate(userScorePref);

                    tempUserScore.SetUserScore(rank, name, score);

                    tempUserScore.transform.SetParent(container);

                    tempUserScore.transform.localScale = Vector3.one;

                    rank++;
                }
            }
        });
    }

    public void SaveFinalScore(int score)
    {
        string email = $"user{Random.Range(0, 100)}{Random.Range(0, 100)}{Random.Range(0, 100)}@gmail.com";

        string playerName = AuthManager.Instance.User().DisplayName;

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        DocumentReference docRef =
        db.Collection("Leaderboard").Document(email);

        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result.Exists)
            {
                int oldScore = task.Result.GetValue<int>("score");

                if (score > oldScore)
                {
                    docRef.SetAsync(new Dictionary<string, object>()
                    {
                        {"name", email },
                        { "score", score }
                    });
                }
            }
            else
            {
                docRef.SetAsync(new Dictionary<string, object>()
                {
                    {"name", email },
                    { "score", score }
                });
            }
        });
    }

    private void OnDestroy()
    {
        AuthManager.Instance.LoggedIn -= GetTop10;
    }
}
