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

    private bool isStarting;

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

        //print(questLevel.name);

        if (questLevel == null)
        {
            return;
        }

        foreach (QuestBase quest in questLevel.GetQuest())
        {
            QuestBase tempQuest = Instantiate(quest, transform);
            quests.Enqueue(tempQuest);
        }
        
        isStarting = true;

        StartNewQuest();
        //currentQuest = quests.Dequeue();
    }

    public void OnCompleteQuest()
    {
        questInfo.HideQuestInfo();
        print(quests.Count);

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

        DSType campaignDSType = UserData.Instance.campaignDSType;

        int newLevel = questLevel.level + 1;

        switch (campaignDSType)
        {
            case DSType.Array:
                UserData.Instance.arrayLevel = newLevel;
                break;
            case DSType.Linkedlist:
                UserData.Instance.linkedlistLevel = newLevel;
                break;
            case DSType.Stack:
                UserData.Instance.stackLevel = newLevel;
                break;
            case DSType.Queue:
                UserData.Instance.queueLevel = newLevel;
                break;
        }

        UserData.Instance.SaveProgress();

        print("LEVEL COMPLETED");
    }

    public void StartNewQuest()
    {
        currentQuest = quests.Dequeue();

        currentQuest.TryShowTips();

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
        return quest == currentQuest && isStarting;
    }

    private void OnDestroy()
    {
        QuestEvent.CompletedQuest -= OnCompleteQuest;

        if (questLevel)
        {        
            Destroy(questLevel.gameObject);
        }
    }
}
