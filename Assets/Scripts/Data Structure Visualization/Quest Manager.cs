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

        questLevel = FindAnyObjectByType<LevelData>();

        if (questLevel == null)
        {

            return;
        }

        if (AuthManager.Instance && AuthManager.Instance.User() != null)
            Dudu.Instance.ShowDudu($"Haloo {AuthManager.Instance.User().DisplayName}, {questLevel.duduText}. Let's Go!!", () =>
            {
                print("Ceking");
                CheckForQuest();
            });
        else 
            Dudu.Instance.ShowDudu("Haloo user!", () =>
            {
                print("Ceking");
                CheckForQuest();
            });

        //Dudu.Instance.OnHidingEvent += OnCompleteQuest;

        //Invoke(nameof(CheckForQuest), 5f);

        QuestEvent.CompletedQuest += OnCompleteQuest;
    }

    private void CheckForQuest()
    {

        //print(questLevel.name);


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
        questInfo.HideQuestInfo(() =>
        {
            print(quests.Count);

            if (quests.Count > 0)
            {
                Invoke(nameof(StartNewQuest), 1.5f);
                //StartNewQuest();
            }
            else
            {
                Invoke(nameof(EndQuest), 1.5f);
            }
        });

    }

    private void EndQuest()
    {
        QuestEvent.CompletedLevel?.Invoke();

        DSType campaignDSType = UserData.Instance.campaignDSType;

        int newLevel = questLevel.level + 1;

        switch (campaignDSType)
        {
            case DSType.Array:
                if (newLevel == 4)
                {
                    UserData.Instance.completedArray = true;
                }
                if (newLevel > UserData.Instance.arrayLevel)
                {
                    UserData.Instance.arrayLevel = newLevel;                
                    UserData.Instance.AddScore(500);
                }
                break;
            case DSType.Linkedlist:
                if (newLevel == 3)
                {
                    UserData.Instance.completedLinkedlist = true;
                }
                if (newLevel > UserData.Instance.linkedlistLevel)
                {
                    UserData.Instance.linkedlistLevel = newLevel;
                    UserData.Instance.AddScore(500);
                }
                break;
            case DSType.Stack:
                if (newLevel == 3)
                {
                    UserData.Instance.completedStack = true;
                }
                if (newLevel > UserData.Instance.stackLevel)
                {
                    UserData.Instance.stackLevel = newLevel;
                    UserData.Instance.AddScore(500);
                }
                break;
            case DSType.Queue:
                if (newLevel == 3)
                {
                    UserData.Instance.completedQueue = true;
                }
                if (newLevel > UserData.Instance.queueLevel)
                {
                    UserData.Instance.queueLevel = newLevel;
                    UserData.Instance.AddScore(500);
                }
                break;
        }
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
