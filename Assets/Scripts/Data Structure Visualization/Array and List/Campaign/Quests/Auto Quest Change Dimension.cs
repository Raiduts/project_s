using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoQuestChangeDimension : QuestBase
{
    public override void OnStartQuest()
    {
        ArrayListOperator.instance.SetArrayDimension(true);

        Dudu.Instance.ShowDudu("Okay sekarang kita beralih ke mode 2D");

        QuestEvent.CompletedQuest?.Invoke();
    }
}
