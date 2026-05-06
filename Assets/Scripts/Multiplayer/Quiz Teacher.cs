using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine;

public class QuizTeacher : MonoBehaviour
{
    private string code;

    FirebaseFirestore db;
    FirebaseAuth auth;

    public QuizLeaderboard leaderboard;
    public int questionsQuantity;

    [Header("Test")]
    public List<QuestionData> questionGenerator;
    private QuestionManager questionManager;

    private void Awake()
    {
        db = FirebaseFirestore.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;
    }

    void Start()
    {
        code = PlayerPrefs.GetString("Code");
        questionManager = GetComponent<QuestionManager>();

        questionGenerator = questionManager.GenerateRandomQuestions(questionsQuantity);

        CreateQuiz(code);

        leaderboard.ListenToQuizLeaderboard(code);
    }

    public async void CreateQuiz(string quizCode)
    {
        DocumentReference quizRef = db.Collection("Quizzes").Document(quizCode);
        //DocumentSnapshot checkSnapshot = await quizRef.GetSnapshotAsync();

        //// CEK APAKAH DOKUMENNYA EKSIS DI DATABASE
        //if (checkSnapshot.Exists)
        //{
        //    print("Gagal: Kode Quiz sudah digunakan! Silakan pakai kode lain.");
        //    // Munculkan Pop-up ke Guru di sini
        //    return;
        //}

        WriteBatch batch = db.StartBatch();

        //// Ini akan langsung membuat dokumen 129529 jadi "Nyata" (Hitam tebal)
        //Dictionary<string, object> quizMeta = new Dictionary<string, object>
        //{
        //    { "createdAt", FieldValue.ServerTimestamp },
        //    { "status", "active" },
        //    { "createdBy", auth.CurrentUser.Email } // Opsional: catat siapa pembuatnya
        //};

        //// Kirim data meta dulu ke Firebase
        //await quizRef.SetAsync(quizMeta);
        //Debug.Log("Meta Data Terbuat!");

        // Contoh Generate 20 soal secara loop
        for (int i = 0; i < questionsQuantity; i++)
        {
            DocumentReference qRef = quizRef.Collection("Questions").Document($"Q{i+1}");

            QuestionData newQuestion = questionGenerator[i];

            batch.Set(qRef, newQuestion);
        }

        //CollectionReference lRef = quizRef.Collection("Leaderboard");0

        await batch.CommitAsync();
        Debug.Log("Quiz Berhasil Dibuat!");
    }

    //// Sementara
    //public void WatchLeaderboard(string quizCode)
    //{
    //    Query query = db.Collection("Quizzes").Document(quizCode)
    //                    .Collection("Leaderboard").OrderByDescending("score");

    //    // Listener ini akan terus aktif selama aplikasi jalan
    //    query.Listen(snapshot => {
    //        Debug.Log("Leaderboard Update!");
    //        foreach (DocumentSnapshot doc in snapshot.Documents)
    //        {
    //            LeaderboardData data = doc.ConvertTo<LeaderboardData>();
    //            // Di sini kamu tinggal update UI Text atau Prefab baris leaderboard
    //            Debug.Log($"{data.name}: {data.score}");
    //        }
    //    });
    //}
}

[FirestoreData] // Tambahkan ini agar Firebase mengenali class ini
[Serializable]
public class QuestionData
{
    [FirestoreProperty] // Tambahkan ini pada setiap property
    public string questionText { get; set; }

    [FirestoreProperty]
    public string options { get; set; } // Format: "A;B;C;D"

    [FirestoreProperty]
    public int answerKey { get; set; }
}

[FirestoreData]
public class LeaderboardData
{
    [FirestoreProperty]
    public string name { get; set; }

    [FirestoreProperty]
    public int score { get; set; }
}
