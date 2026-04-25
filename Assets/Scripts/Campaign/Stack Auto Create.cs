using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackAutoCreate : MonoBehaviour, I_OnStartQuest
{
    [SerializeField] private int[] boxId;

    public void OnStartQuest()
    {
        AddBoxes();
    }

    private void AddBoxes()
    {
        foreach (int id in boxId)
        {
            StackOperator.Instance.BypassPush(id);
        }
    }
}
