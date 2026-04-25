using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public int level;
    [SerializeField]
    private List<QuestBase> questsSerialized;
    
    [TextArea]
    [SerializeField] public string duduText;

    public List<QuestBase> GetQuest()
    {
        return questsSerialized;
    }
}
