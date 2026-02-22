using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayListEventListener
{
    // Array
    public static Action<Vector2Int> CreateArray;
    public static Action DeleteArray;
    public static Action<Vector2Int, int> AddValue;
    public static Action<ArrayDimension> ChangeDimension;
    public static Action<OperatorMode> ChangeMode;

    // List
    public static Action<int> AddFirst, AddLast;
}

public enum OperatorMode
{
    Create,
    SetValue
}

public enum ArrayDimension
{
    TwoDimension,
    ThreeDimension
}
