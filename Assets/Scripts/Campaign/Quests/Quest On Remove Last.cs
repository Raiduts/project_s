using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOnRemoveLast : QuestBase
{
    public override void OnRemoveLast()
    {
        if (QuestManager.instance.CheckQuest(this))
        {
            QuestCompleted();
        }
    }
}
