using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOnCreate : QuestBase
{
    [Header("Quest Req")]
    [SerializeField]
    private Vector2Int requiredSize;

    public override void OnCreate(Vector2Int vector)
    {
        if (vector == requiredSize && QuestManager.instance.CheckQuest(this))
        {
            QuestCompleted();
        }
    }
}
