    using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class QuestOnAddValue : QuestBase
{
    [Header("Quest Req")]
    [SerializeField]
    private Vector2Int reqVector;
    [SerializeField]
    private int requiredNumber = 0;

    public override void OnAddValue(Vector2Int vector, int value)
    {
        if (vector == reqVector && value == requiredNumber && QuestManager.instance.CheckQuest(this))
        {
            QuestCompleted();
        }
    }
}
