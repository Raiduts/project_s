using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArrayListQuestBase : MonoBehaviour
{
    void Start()
    {
        ArrayListEventListener.AddValue += OnAddValue;
        ArrayListEventListener.CreateArray += OnCreateArray;
        ArrayListEventListener.ChangeMode += OnChangeMode;
        ArrayListEventListener.ChangeDimension += OnChangeDimension;
        ArrayListEventListener.AddFirst += OnAddFirst;
        ArrayListEventListener.AddLast += OnAddLast;
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

    private void OnCreateArray(Vector2Int @int)
    {
        //throw new NotImplementedException();
    }

    public virtual void OnAddValue(Vector2Int vector, int arg2)
    {
        throw new NotImplementedException();
    }
}

