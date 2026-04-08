using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine;

public class QuizLeaderboard : MonoBehaviour
{
    public Transform container;
    public UserScore userScorePref;

    private ListenerRegistration leaderboardListener; // Untuk mematikan listener saat pindah scene
    private FirebaseFirestore db;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        // Contoh pemanggilan: Ganti "KODE_DUMMY" dengan variabel kode quiz yang sedang aktif
        // ListenToQuizLeaderboard("MATH123"); 
    }

    // FUNGSI UNTUK GURU: Pantau Leaderboard Real-time sesuai Kode Quiz
    public void ListenToQuizLeaderboard(string quizCode)
    {
        if (leaderboardListener != null) leaderboardListener.Stop();

        // Pastikan database sudah terinisialisasi
        if (db == null) db = FirebaseFirestore.DefaultInstance;

        Debug.Log($"Mencoba mendengarkan Leaderboard untuk Kode: {quizCode}");

        // Referensi query
        Query query = db.Collection("Quizzes").Document(quizCode)
                        .Collection("Leaderboard").OrderByDescending("score").Limit(10);

        leaderboardListener = query.Listen(snapshot =>
        {
            // 1. CEK SNAPSHOT: Jika snapshot null atau kosong, bersihkan UI dan berhenti di sini
            if (snapshot == null || snapshot.Count == 0)
            {
                Debug.Log("Leaderboard masih kosong, menunggu siswa submit...");
                ClearLeaderboardUI();
                return;
            }

            // 2. Jika ada data, baru bersihkan UI lama untuk update
            ClearLeaderboardUI();

            int rank = 1;
            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                // Tambahan: Cek apakah field 'name' dan 'score' benar-benar ada di dokumen
                if (doc.Exists && doc.ContainsField("name") && doc.ContainsField("score"))
                {
                    string name = doc.GetValue<string>("name");
                    int score = doc.GetValue<int>("score");

                    UserScore tempUserScore = Instantiate(userScorePref, container);
                    tempUserScore.SetUserScore(rank, name, score);
                    tempUserScore.transform.localScale = Vector3.one;

                    rank++;
                }
            }
        });
    }

    // Fungsi pembantu agar kode lebih rapi
    private void ClearLeaderboardUI()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }
}
