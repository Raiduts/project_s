using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [Header("Quests")]
    [SerializeField]
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

        CheckForQuest();

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
        
        currentQuest = quests.Dequeue();
    }

    public void OnCompleteQuest()
    {
        if (quests.Count > 0)
        {
            Invoke(nameof(StartNewQuest), 5f);
        }
        else
        {
            QuestEvent.CompletedLevel?.Invoke();
            print("LEVEL COMPLETED");
        }
    }

    public void StartNewQuest()
    {
        currentQuest = quests.Dequeue();
        
        currentQuest.OnStartQuest();
    }

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
