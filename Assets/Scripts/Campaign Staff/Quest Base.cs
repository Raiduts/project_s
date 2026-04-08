using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestBase : MonoBehaviour
{
    public string questTitle;
    [TextArea]
    public string questObjective;

    [Header("Quest Req")]
    [TextArea]
    public string duduComment;

    void Start()
    {
        EventListener.AddValue += OnAddValue;
        EventListener.CreateArray += OnCreate;
        EventListener.ChangeMode += OnChangeMode;
        EventListener.ChangeDimension += OnChangeDimension;
        EventListener.AddFirst += OnAddFirst;
        EventListener.AddLast += OnAddLast;
        EventListener.RemoveFirst += OnRemoveFirst;
        EventListener.RemoveLast += OnRemoveLast;
        EventListener.GetSize += OnGetSize;
        EventListener.IsEmpty += IsEmpty;
    }

    public virtual void IsEmpty()
    {
        throw new NotImplementedException();
    }

    public virtual void OnGetSize(int obj)
    {
        throw new NotImplementedException();
    }

    public virtual void OnRemoveLast()
    {
        throw new NotImplementedException();
    }

    public virtual void OnRemoveFirst()
    {
        throw new NotImplementedException();
    }

    public virtual void OnStartQuest()
    {

    }

    public virtual void OnAddLast(int obj)
    {
        throw new NotImplementedException();
    }

    public virtual void OnAddFirst(int obj)
    {
        throw new NotImplementedException();
    }

    public virtual void OnChangeDimension(ArrayDimension dimension)
    {
        //throw new NotImplementedException();
    }

    public virtual void OnChangeMode(OperatorMode mode)
    {
        throw new NotImplementedException();
    }

    public virtual void OnCreate(Vector2Int vector)
    {
        // Created Data Structure
    }

    public virtual void OnAddValue(Vector2Int vector, int value) 
    { 
        // Added Value to Data Structure
    }
}

