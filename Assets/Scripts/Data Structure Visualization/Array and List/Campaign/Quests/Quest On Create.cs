using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOnCreate : QuestBase
{
    [Header("Quest Req")]
    [SerializeField]
    private Vector2Int requiredSize;
    [TextArea]
    [SerializeField]
    private string duduComment;

    public override void OnCreate(Vector2Int vector)
    {
        if (vector == requiredSize && QuestManager.instance.CheckQuest(this))
        {
            Dudu.Instance.ShowDudu(duduComment);

            QuestEvent.CompletedQuest?.Invoke();
        }
    }
}
