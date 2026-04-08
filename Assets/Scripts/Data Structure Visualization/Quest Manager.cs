using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [Header("Quests")]
    [SerializeField]
    private QuestInfo questInfo;
    private Vector3 questInfoPanelFirstPos;
    [SerializeField]
    private TextMeshProUGUI questTitle;
    [SerializeField]
    private TextMeshProUGUI questObjective;
    private LevelData questLevel;
    private Queue<QuestBase> quests;
    private QuestBase currentQuest;
    private int currentQuestId;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        quests = new Queue<QuestBase>();

        Invoke(nameof(CheckForQuest), 5f);
        //CheckForQuest();

        QuestEvent.CompletedQuest += OnCompleteQuest;
    }

    private void CheckForQuest()
    {
        questLevel = FindAnyObjectByType<LevelData>();

        if (questLevel == null)
        {
            return;
        }

        foreach (QuestBase quest in questLevel.GetQuest())
        {
            QuestBase tempQuest = Instantiate(quest, transform);
            quests.Enqueue(tempQuest);
        }
        
        StartNewQuest();
        //currentQuest = quests.Dequeue();
    }

    public void OnCompleteQuest()
    {
        questInfo.HideQuestInfo();

        if (quests.Count > 0)
        {
            Invoke(nameof(StartNewQuest), 5f);
        }
        else
        {
            Invoke(nameof(EndQuest), 5f);
        }
    }

    private void EndQuest()
    {
            QuestEvent.CompletedLevel?.Invoke();
            print("LEVEL COMPLETED");
    }

    public void StartNewQuest()
    {
        currentQuest = quests.Dequeue();

        questInfo.ShowQuestInfo(currentQuest);
        
        currentQuest.OnStartQuest();
    }

    //private void ShowQuestInfo()
    //{
    //    questInfoPanel.DOLocalMoveX(600, 0.5f).SetRelative(); 

    //    questTitle.text = currentQuest.questTitle;
    //    questObjective.text = currentQuest.questObjective;
    //}

    //private void HideQuestInfo()
    //{
    //    questInfoPanel.DOLocalMoveX(-600, 0.5f).SetRelative();

    //    questTitle.text = "";
    //    questObjective.text = "";
    //}

    public int GetQuestId()
    {
        return currentQuestId;
    }

    public void AddQuestId()
    {
        currentQuestId++;
    }

    public bool CheckQuest(QuestBase quest)
    {
        return quest == currentQuest;
    }
}
