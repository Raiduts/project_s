using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOnGetSize : QuestBase
{
    public override void OnGetSize(int obj)
    {
        if (QuestManager.instance.CheckQuest(this))
        {
            QuestCompleted();
        }
    }
}
