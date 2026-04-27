using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class QuestBase : MonoBehaviour
{
    public string questTitle;
    [TextArea]
    public string questObjective;

    [Header("Quest Req")]
    [TextArea]
    public string duduComment;

    [Header("Tips")]
    [SerializeField] private Tips tips;

    private bool isCompleted;

    void Start()
    {
        EventListener.CreateArray += OnCreate;
        EventListener.CreateList += OnCreateList;
        EventListener.AddValue += OnAddValue;
        EventListener.ChangeMode += OnChangeMode;
        EventListener.ChangeDimension += OnChangeDimension;
        EventListener.AddFirst += OnAddFirst;
        EventListener.AddLast += OnAddLast;
        EventListener.RemoveFirst += OnRemoveFirst;
        EventListener.RemoveLast += OnRemoveLast;
        EventListener.GetSize += OnGetSize;
        EventListener.IsEmpty += IsEmpty;
        EventListener.Edited += OnEdit;
        EventListener.AccessData += OnAccessData;
    }

    public virtual void OnStartQuest()
    {
        //TryShowTips();
     
        I_OnStartQuest onStartScript = GetComponent<I_OnStartQuest>();

        onStartScript?.OnStartQuest();
    }

    public virtual void TryShowTips()
    {
        if (tips != null)
        {
            Instantiate(tips).OpenTips();
        }
    }

    public virtual void OnCreateList() { }

    public virtual void IsEmpty() { }

    public virtual void OnGetSize(int obj) { }

    public virtual void OnRemoveLast() { }

    public virtual void OnRemoveFirst() { }

    public virtual void OnAccessData() { }

    public virtual void OnAddLast(int obj) { }

    public virtual void OnAddFirst(int obj) { }

    public virtual void OnChangeDimension(ArrayDimension dimension) { }

    public virtual void OnChangeMode(OperatorMode mode) { }

    public virtual void OnCreate(Vector2Int vector) { }

    public virtual void OnAddValue(Vector2Int vector, int value) { }

    public virtual void OnEdit(int[,] data) { }

    public void QuestCompleted()
    {
        if (isCompleted)
        {
            return;
        }

        isCompleted = true;

        Dudu.Instance.ShowDudu(duduComment);

        QuestEvent.CompletedQuest?.Invoke();
    }
}

