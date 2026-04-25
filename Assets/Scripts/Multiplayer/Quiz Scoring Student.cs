using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizScoringStudent : MonoBehaviour
{
    public static QuizScoringStudent Instance;

    private int quizScore;

    public GameObject scorePanel;
    public TextMeshProUGUI scoreText;

    public Action<int> OnChangeScore;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        QuestionStudent.Instance.OnFinishQuiz += FinishedQuiz;
    }

    private void FinishedQuiz()
    {
        scorePanel.SetActive(true);
        scoreText.text = $"{quizScore} poin";

        UserData.Instance.AddScore(quizScore);
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
}
