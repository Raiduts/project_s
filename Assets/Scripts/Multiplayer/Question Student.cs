using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionStudent : MonoBehaviour
{
    public static QuestionStudent Instance { get; private set; }

    // Test
    public List<QuestionData> localQuestions = new List<QuestionData>();

    [Header("Timer")]
    [SerializeField]
    private Slider timerSlider;
    private Image timerBar;
    private float timerValue;

    [Header("Score")]
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [Header("Question")]
    [SerializeField]
    private TextMeshProUGUI questionText;
    [SerializeField]
    private TextMeshProUGUI questionNumberText;
    private int currentQuestionNumber = 0;

    [Header("Options")]
    [SerializeField]
    private Transform optionGrid;
    private string[] options;
    private string currentAnswerKey;

    //Test
    [SerializeField]
    private TextMeshProUGUI[] optionText;

    // Evnet
    public Action OnFinishQuiz;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timerValue = 30;

        UpdateScoreText(0);

        QuizScoringStudent.Instance.OnChangeScore += UpdateScoreText;
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = $"{score} poin";
        //print(score);
    }

    private void Update()
    {
        timerValue -= Time.deltaTime;

        timerSlider.value = timerValue / 30;

        if (timerValue <= 0)
        {
            NextQuestion();
        }
    }

    private void NextQuestion()
    {
        currentQuestionNumber++;

        if (currentQuestionNumber >= localQuestions.Count)
        {
            OnFinishQuiz?.Invoke();
            return;
        }

        ShowQuestion(localQuestions[currentQuestionNumber]);
    }

    public void ShowQuestion(QuestionData questionData)
    {
        timerValue = 30;

        questionText.text = questionData.questionText;

        questionNumberText.text = $"{currentQuestionNumber + 1}";

        options = questionData.options.Split(';');

        currentAnswerKey = options[questionData.answerKey];

        print(currentAnswerKey);

        InsertOptionsText();
    }

    private void InsertOptionsText()
    {
        for (int i = 0; i < options.Length; i++)
        {
            optionText[i].text = options[i];
        }
    }

    public void SelectOption(int index)
    {
        PlayerPrefs.SetString("SelectedOption", options[index]);
    }

    public void Answer()
    {
        if (!PlayerPrefs.HasKey("SelectedOption"))
            return;

        if (CheckAnswer())
        {
            CalculateScore();
        }
        else
        {
            print("Salah");
        }

        NextQuestion();

        PlayerPrefs.DeleteKey("SelectedOption");
    }

    private void CalculateScore()
    {
        int tempScore = (int) timerValue * 10;

        QuizScoringStudent.Instance.AddScore(tempScore);
    }

    private bool CheckAnswer()
    {
        string selectedOption = PlayerPrefs.GetString("SelectedOption");

        return selectedOption.Equals(currentAnswerKey);
    }
}
