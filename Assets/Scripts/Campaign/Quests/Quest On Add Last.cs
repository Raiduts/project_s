using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOnAddLast : QuestBase
{
    [Header("Quest Req")]
    [SerializeField]
    private Vector2Int reqVector;
    [SerializeField]
    private int requiredNumber = 25;

    public override void OnAddLast(int obj)
    {
        if (QuestManager.instance.CheckQuest(this))
        {
            Dudu.Instance.ShowDudu(duduComment);

            QuestEvent.CompletedQuest?.Invoke();
        }
    }
}
