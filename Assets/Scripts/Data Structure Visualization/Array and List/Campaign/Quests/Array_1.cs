using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Array_1 : ArrayListQuestBase
{
    public override void OnAddValue(Vector2Int vector, int arg2)
    {
        if (vector == new Vector2(1,2) && arg2 == 100)
        {
            Dudu.Instance.ShowDudu("Woww keren! Selamat yaa!");

            QuestEvent.CompletedQuest?.Invoke();
        }
    }
}
