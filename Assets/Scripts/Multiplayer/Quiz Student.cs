using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine;

public class QuizStudent : MonoBehaviour
{
    FirebaseFirestore db;
    FirebaseAuth auth;
    [SerializeField]
    List<QuestionData> localQuestions = new List<QuestionData>();

    private string code;

    private void Awake()
    {
        db = FirebaseFirestore.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;
    }

    void Start()
    {
        code = PlayerPrefs.GetString("Code");

        QuizScoringStudent.Instance.OnChangeScore += UploadScoreToLeaderboard;

        JoinAndFetchQuiz(code);
    }

    // 1. Ambil semua soal berdasarkan kode 
    public async void JoinAndFetchQuiz(string quizCode)
    {
        QuerySnapshot snapshot = await db.Collection("Quizzes").Document(quizCode)
                                        .Collection("Questions").GetSnapshotAsync();

        foreach (DocumentSnapshot doc in snapshot.Documents)
        {
            localQuestions.Add(doc.ConvertTo<QuestionData>());
        }

        ShuffleQuestions(localQuestions);

        Debug.Log($"Berhasil ambil {localQuestions.Count} soal.");

        // Test
        QuestionStudent.Instance.ShowQuestion(localQuestions[0]);
        QuestionStudent.Instance.localQuestions = localQuestions;
    }

    public void ShuffleQuestions(List<QuestionData> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            QuestionData temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }

        //foreach (QuestionData item in list)
        //{
        //    print(item.questionText);
        //}
    }

    public void UploadScoreToLeaderboard(int score)
    {
        SubmitAnswer(code, score);
    }

    // 2. Update skor ke leaderboard (Gunakan Email sebagai ID Dokumen)
    public void SubmitAnswer(string quizCode, int currentScore)
    {
        string userEmail = auth.CurrentUser.Email;
        string userName = auth.CurrentUser.DisplayName;

        if (userEmail == "" || userName == "")
        {
            userEmail = "anonymous@gmail.com";
            userName = "anonymous";
        }

        DocumentReference scoreRef = db.Collection("Quizzes").Document(quizCode)
                                       .Collection("Leaderboard").Document(userEmail);

        Dictionary<string, object> data = new Dictionary<string, object> {
            { "name", userName },
            { "score", currentScore },
            { "lastUpdated", FieldValue.ServerTimestamp }
        };

        // SetAsync dengan Merge agar tidak menimpa data lain jika ada
        scoreRef.SetAsync(data, SetOptions.MergeAll);
    }
}
