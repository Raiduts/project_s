using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotive : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private int x;
    [SerializeField] private int y;
    [SerializeField] private int gapX;
    [SerializeField] private int gapY;
    private Container[,] array;

    [Header("Assets")]
    [SerializeField] private GameObject trolley;
    [SerializeField] private Container containerPref;

    public void SetLength(int x, int y)
    {
        this.x = x;
        this.y = y;

        array = new Container[this.x,this.y];
    }

    public void GenerateTrains()
    {
        for (int i = 0; i < x; i++)
        {
            Vector3 carriageGap = new(gapX * (i + 1), 0, 0);
            
            GameObject trolleyObj = Instantiate(trolley, transform);
            trolleyObj.transform.localPosition += carriageGap;

            for (int j = 0; j < y; j++)
            {
                Vector3 containerGap = new(gapX * (i + 1) - 0.5f, gapY * j + 2.25f, 0);
                CreateContainer(i, j, containerGap);
            }
        }
    }

    private void CreateContainer(int x, int y, Vector3 gap)
    {
        Container containerBottom = Instantiate(containerPref, transform);
        containerBottom.transform.localPosition += gap;
        containerBottom.RandomizeValue();
        array[x,y] = containerBottom;
    }
}
