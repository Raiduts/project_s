using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public int level;
    [SerializeField]
    private List<QuestBase> questsSerialized;

    public List<QuestBase> GetQuest()
    {
        return questsSerialized;
    }
}
