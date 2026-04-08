using UnityEngine;

public class QuizSceneManager : MonoBehaviour
{
    [Header("UI Panels")]
    public QuizTeacher teacherPanel;
    public QuizStudent studentPanel;

    void Awake()
    {
        // Ambil data "isTeacher" dari PlayerPrefs
        // Default value 0 (Student) kalau datanya ngga ketemu
        int isTeacher = PlayerPrefs.GetInt("IsTeacher", 0);

        if (isTeacher == 1)
        {
            ActivateTeacherMode();
        }
        else
        {
            ActivateStudentMode();
        }
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