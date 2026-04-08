using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOnRemoveFirst : QuestBase
{
    public override void OnRemoveFirst()
    {
        if (QuestManager.instance.CheckQuest(this))
        {
            Dudu.Instance.ShowDudu(duduComment);
            QuestEvent.CompletedQuest?.Invoke();
        }
    }
}
