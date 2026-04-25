using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOnAccessData : QuestBase
{
    public override void OnAccessData()
    {
        if (QuestManager.instance.CheckQuest(this))
        {
            QuestCompleted();
        }
    }
}
