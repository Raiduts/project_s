using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOnCreateList : QuestBase
{
    public override void OnCreateList()
    {
        if (QuestManager.instance.CheckQuest(this))
        {
            QuestCompleted();
        }
    }
}
