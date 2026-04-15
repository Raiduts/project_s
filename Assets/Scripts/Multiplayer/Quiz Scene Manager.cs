using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine;

public class QuizSceneManager : MonoBehaviour
{
    private FirebaseFirestore db;
    private FirebaseAuth auth;

    [Header("UI Panels")]
    public QuizTeacher teacherPanel;
    public QuizStudent studentPanel;

    private ListenerRegistration listener;
    private string roomCode;

    void Awake()
    {
        // Ambil data "isTeacher" dari PlayerPrefs
        // Default value 0 (Student) kalau datanya ngga ketemu
    }

    private void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;

        roomCode = PlayerPrefs.GetString("Code");

        //ListenRoomStatus();
    }

    public void ListenRoomStatus()
    {
        DocumentReference roomRef = db.Collection("Quizzes").Document(roomCode);

        listener = roomRef.Listen(snapshot =>
        {
            if (snapshot.Exists)
            {
                string status = snapshot.GetValue<string>("status");

                Debug.Log("Status sekarang: " + status);

                switch (status)
                {
                    case "running":
                        OnGameStarted();
                        break;

                    case "finished":
                        OnGameFinished();
                        break;
                }
            }
        });
    }

    void OnGameStarted()
    {
        Debug.Log("Game mulai!");
        
        // Cara 1
        int isTeacher = PlayerPrefs.GetInt("IsTeacher", 0);

        if (isTeacher == 1)
        {
            ActivateTeacherMode();
        }
        else
        {
            ActivateStudentMode();
        }
        // contoh:
        // SceneManager.LoadScene("GameScene");
    }

    void OnGameFinished()
    {
        Debug.Log("Game selesai!");

        // contoh:
        // SceneManager.LoadScene("ResultScene");
    }
    void ActivateTeacherMode()
    {
        Debug.Log("Masuk sebagai Guru");
        if (teacherPanel != null) teacherPanel.gameObject.SetActive(true);

        // Di sini kamu bisa otomatis panggil Leaderboard-nya
        string quizCode = PlayerPrefs.GetString("Code");
        // Contoh: FindObjectOfType<Leaderboard>().ListenToQuizLeaderboard(quizCode);
    }

    void ActivateStudentMode()
    {
        Debug.Log("Masuk sebagai Siswa");
        if (studentPanel != null) studentPanel.gameObject.SetActive(true);

        // Di sini kamu bisa otomatis panggil Fetch Soal-nya
        string quizCode = PlayerPrefs.GetString("Code");
        // Contoh: FindObjectOfType<StudentManager>().JoinAndFetchQuiz(quizCode);
    }
}