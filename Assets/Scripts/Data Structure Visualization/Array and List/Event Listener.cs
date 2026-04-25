using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventListener
{
    // Data Structure

    // Segmented
    public static Action<Vector2Int> CreateArray; // Array and List
    public static Action CreateList; // List
    public static Action<Vector2Int, int> AddValue; // Array and List
    public static Action<ArrayDimension> ChangeDimension; // Array
    public static Action<OperatorMode> ChangeMode; // Array and List

    // Many
    public static Action<int> AddFirst, AddLast; // Except Array
    public static Action RemoveFirst, RemoveLast; // Except Array
    public static Action AccessData; // All
    public static Action DeleteArray, IsEmpty; // All
    public static Action<int> GetSize; // All
    public static Action<int[,]> Edited; // All

    // Utilities
    public static Action<DashboardPage> ChangeDashboardPage;
}

public enum OperatorMode
{
    Create,
    SetValue
}

public enum ArrayDimension
{
    OneDimension,
    TwoDimension
}

[Serializable]
public enum DashboardPage
{
    Leaderboard,
    Home,
    Game
}
