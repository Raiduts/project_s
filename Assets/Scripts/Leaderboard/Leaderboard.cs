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

    public void GetTop10()
    {
        foreach (Transform item in container)
        {
            Destroy(item.gameObject);
        }

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        db.Collection("Leaderboard")
        .OrderByDescending("score")
        .GetSnapshotAsync()
        .ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                int rank = 1;

                foreach (var doc in task.Result.Documents)
                {
                    //doc.Reference.UpdateAsync("iconIndex", Random.Range(0,3));

                    string name = doc.GetValue<string>("name");

                    int score = doc.GetValue<int>("score");

                    int icon = doc.GetValue<int>("iconIndex");

                    UserScore tempUserScore = Instantiate(userScorePref);

                    tempUserScore.SetUserScore(rank, name, score, icon);

                    tempUserScore.transform.SetParent(container);

                    tempUserScore.transform.localScale = Vector3.one;

                    rank++;
                }
            }
        });


    }

    private void OnDestroy()
    {
        AuthManager.Instance.LoggedIn -= GetTop10;
    }
}
