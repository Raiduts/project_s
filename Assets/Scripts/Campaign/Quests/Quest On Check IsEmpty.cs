using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOnCheckIsEmpty : QuestBase
{
    public override void IsEmpty()
    {
        if (QuestManager.instance.CheckQuest(this))
        {
            QuestCompleted();
        }
    }
}
