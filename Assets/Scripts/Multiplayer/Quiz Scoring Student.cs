using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizScoringStudent : MonoBehaviour
{
    public static QuizScoringStudent Instance;

    [SerializeField] private PlayerItem playerItem;
    private int quizScore;

    public GameObject scorePanel;
    public TextMeshProUGUI scoreText, correctText, wrongText;

    public Action<int> OnChangeScore;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        scorePanel.SetActive(false);
        QuestionStudent.Instance.OnFinishQuiz += FinishedQuiz;
    }

    private void FinishedQuiz()
    {
        scorePanel.SetActive(true);

        scoreText.text = $"{quizScore}";

        UserData.Instance.AddScore(quizScore);

        playerItem.SetData(AuthManager.Instance.User().DisplayName, UserData.Instance.iconIndex);

        correctText.text = QuestionStudent.Instance.GetCorrectAnswer().ToString();

        wrongText.text = QuestionStudent.Instance.GetWrongAnswer().ToString();  
    }

    public int QuizScore()
    {
        return quizScore;
    }

    public void AddScore(int adder)
    {
        quizScore += adder;

        OnChangeScore?.Invoke(quizScore);
    }

    public void BackToMenu()
    {
        MySceneManager.instance.ChangeScene("Dashboard");
    }
}
