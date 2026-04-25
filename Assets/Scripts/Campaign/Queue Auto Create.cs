using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueAutoCreate : MonoBehaviour, I_OnStartQuest
{
    [SerializeField] private int[] fishesId;

    public void OnStartQuest()
    {
        AddFishes();
    }

    private void AddFishes()
    {
        foreach (int id in fishesId)
        {
            QueueOperator.Instance.BypassEnqueue(id);
        }
    }
}
