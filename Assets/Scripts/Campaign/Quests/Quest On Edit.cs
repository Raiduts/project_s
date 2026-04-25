using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOnEdit : QuestBase
{
    [SerializeField] private indeks[] indexY;
    [SerializeField] private QuestEditType editType = QuestEditType.Both;

    public override void OnEdit(int[,] data)
    {
        if (IsSameData(data) && QuestManager.instance.CheckQuest(this))
        {
            print("Berhasil");
            QuestCompleted();
        }
    }

    private bool IsSameData(int[,] data)
    {
        int reqLength = 0;

        try
        {
            for (int y = 0; y < indexY.Length; y++)
            {
                for (int x = 0; x < indexY[y].horizontalIndex.Length; x++)
                {
                    if (editType == QuestEditType.Both || editType == QuestEditType.Values)
                    {
                        if (data[x,y] != indexY[y].horizontalIndex[x])
                        {
                            return false;
                        }
                    }
                    reqLength++;
                }
            }
        }
        catch
        {
            return false;
        }

        if (editType == QuestEditType.Both || editType == QuestEditType.Length)
        {
            if (reqLength != data.Length)
            {
                return false;
            }
        }

        return true;
    }
}

public enum QuestEditType
{
    Both,
    Values,
    Length
}