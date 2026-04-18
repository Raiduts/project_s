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
    [SerializeField]
    private QuizOption quizOptionPref;
    private QuizOption[] quizOptions;
    private string[] options;
    private string currentAnswerKey;
    private int selectedOptionKey, answerOptionKey;

    [Header("PopUp")]
    [SerializeField]
    private QuizPopUpReveal popUpReveal;

    private bool isAnswering, isChanging;

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
        timerValue = 15;

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
        if (!isAnswering)
        {
            timerValue -= Time.deltaTime;

            timerSlider.value = timerValue / 15;            
        }

        if (timerValue <= 0 && !isChanging)
        {
            isChanging = true;

            Answer();
        }
    }

    private void NextQuestion()
    {
        isChanging = true;

        ResetOption();

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
        // Reset Answer
        selectedOptionKey = -1;
        isAnswering = false;
        isChanging = false;

        // Reset Timer
        timerValue = 15;

        // Set Question
        questionText.text = questionData.questionText;

        // Set Number
        questionNumberText.text = $"{currentQuestionNumber + 1}/{localQuestions.Count}";

        // Get Options Text
        options = questionData.options.Split(';');
        
        // Set Answer Key String (erasable)
        currentAnswerKey = options[questionData.answerKey];

        // Set Answer Key Int
        answerOptionKey = questionData.answerKey;

        // Set Options Text to Options Prefab
        InsertOptionsText();
    }

    private void InsertOptionsText()
    {
        int index = 0;

        quizOptions = new QuizOption[options.Length];

        foreach(string optionText in options)
        {
            QuizOption quizOptionTemp = Instantiate(quizOptionPref, optionGrid);
            quizOptionTemp.SetText(optionText);
            quizOptionTemp.SetOptionIndex(index);

            // Add To Array
            quizOptions[index] = quizOptionTemp;

            // Dont Forget To UnSub
            quizOptionTemp.ChooseOption += SelectOption;

            // Increment
            index++;
            //optionText[i].text = options[i];
        }
    }

    public void SelectOption(int index)
    {
        if (isAnswering)
        {
            return;
        }

        selectedOptionKey = index;
        
        quizOptions[selectedOptionKey].Bounce();

        Answer();
        //PlayerPrefs.SetString("SelectedOption", options[index]);
    }

    public void Answer()
    {
        //if (selectedOptionKey == -1)
        //    return;


        isAnswering = true;

        HideOtherOption();

        Invoke(nameof(RevealAnswer), 1f);
    }

    private void RevealAnswer()
    {
        if (CheckAnswer())
        {
            CalculateScore();
        }
        else
        {
            popUpReveal.SetText("Kamu Salah\nDASAR BODOH!");
            popUpReveal.ShowPopUp();

            if (selectedOptionKey != -1)
            {   
                quizOptions[selectedOptionKey].TurnRed();
            }
        }
        
        quizOptions[answerOptionKey].ShowOption();

        quizOptions[answerOptionKey].TurnGreen();

        Invoke(nameof(NextQuestion), 2f);
    }

    private void HideOtherOption()
    {
        for (int i = 0; i < quizOptions.Length; i++)
        {
            if (i == selectedOptionKey)
            {
                continue;
            }

            quizOptions[i].HideOption();
        }
    }

    private void ResetOption()
    {
        if (quizOptions.Length == 0)
        {
            return;
        }

        foreach (QuizOption option in quizOptions)
        {
            option.ChooseOption -= SelectOption;
            Destroy(option.gameObject);
        }
    }

    private void CalculateScore()
    {
        int tempScore = (int) (timerValue * 100);

        //QuizScoringStudent.Instance.AddScore(tempScore);

        popUpReveal.SetText("Kamu Benar", tempScore);
        popUpReveal.ShowPopUp();
    }

    private bool CheckAnswer()
    {
        if (selectedOptionKey == -1)
        {
            return false;
        }
        //string selectedOption = PlayerPrefs.GetString("SelectedOption");

        return selectedOptionKey == answerOptionKey;
    }
}
