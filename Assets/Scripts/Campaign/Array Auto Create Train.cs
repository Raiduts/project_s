using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayAutoCreateTrain : MonoBehaviour, I_OnStartQuest
{
    [Header("Array Starter")]
    private int lengthX, lengthY;
    [SerializeField] private indeks[] verticalIndex;

    // Start is called before the first frame update

    public void OnStartQuest()
    {
        lengthY = verticalIndex.Length;
        lengthX = verticalIndex[0].horizontalIndex.Length;

        LoadTrain();
    }

    private void LoadArray()
    {

        for (int y = 0; y < lengthY; y++)
        {
            for (int x = 0; x < lengthX; x++)
            {
                EventListener.AddValue(new(x, y), verticalIndex[y].horizontalIndex[x]);
            }
        }
    }

    private void LoadTrain()
    {
        EventListener.CreateArray(new(lengthX, lengthY));

        Invoke(nameof(LoadArray),0.25f);
    }
}

[System.Serializable]
public class indeks
{
    public int[] horizontalIndex;
}
