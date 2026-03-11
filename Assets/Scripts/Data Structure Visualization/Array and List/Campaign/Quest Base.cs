using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestBase : MonoBehaviour
{
    void Start()
    {
        ArrayListEventListener.AddValue += OnAddValue;
        ArrayListEventListener.CreateArray += OnCreate;
        ArrayListEventListener.ChangeMode += OnChangeMode;
        ArrayListEventListener.ChangeDimension += OnChangeDimension;
        ArrayListEventListener.AddFirst += OnAddFirst;
        ArrayListEventListener.AddLast += OnAddLast;
    }

    public virtual void OnStartQuest()
    {

    }

    private void OnAddLast(int obj)
    {
        throw new NotImplementedException();
    }

    private void OnAddFirst(int obj)
    {
        throw new NotImplementedException();
    }

    private void OnChangeDimension(ArrayDimension dimension)
    {
        throw new NotImplementedException();
    }

    private void OnChangeMode(OperatorMode mode)
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

