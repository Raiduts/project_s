using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOnAddLast : QuestBase
{
    [Header("Quest Req")]
    [SerializeField]
    private int requiredNumber;

    public override void OnAddLast(int req)
    {
        if (req == requiredNumber && QuestManager.instance.CheckQuest(this))
        {
            QuestCompleted();
        }
    }
}
