using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOnAddFirst : QuestBase
{
    [Header("Quest Req")]
    [SerializeField]
    private int requiredNumber;

    public override void OnAddFirst(int req)
    {
        if (req == requiredNumber && QuestManager.instance.CheckQuest(this))
        {
            QuestCompleted();
        }
    }
}
