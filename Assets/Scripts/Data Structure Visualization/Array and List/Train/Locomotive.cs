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
    [SerializeField] private GameObject headPref;
    [SerializeField] private GameObject trolley;
    [SerializeField] private Container containerPref;

    private void Start()
    {
        ArrayListEventListener.AddValue += SetContainerValue;
    }

    public void SetLength(int x, int y)
    {
        this.x = x;
        this.y = y;

        array = new Container[this.x, this.y];
    }

    public void GenerateTrains()
    {
        for (int j = 0; j < y; j++)
        {
            GameObject tempHead = Instantiate(headPref);

            tempHead.transform.parent = transform;
            tempHead.transform.localPosition = new(0, -gapY * j, 0);
            tempHead.transform.localScale = Vector3.one;

            for (int i = 0; i < x; i++)
            {

                Vector3 carriageGap = new(gapX * (i + 1), -gapY * j, 0);

                // GameObject trolleyObj = Instantiate(trolley, transform);
                // trolleyObj.transform.localPosition += carriageGap;

                //for (int j = 0; j < y; j++)
                //{
                Vector3 containerGap = new(gapX * (i + 1) - 0.5f, -gapY * j, 0);
                CreateContainer(i, j, containerGap);
                //}
            }
        }
    }

    private void CreateContainer(int x, int y, Vector3 gap)
    {
        Container containerBottom = Instantiate(containerPref, transform);
        containerBottom.transform.localPosition += gap;
        containerBottom.RandomizeValue();
        array[x, y] = containerBottom;
    }

    public void SetContainerValue(Vector2Int xy, int value)
    {
        if (xy.x >= x || xy.y >= y)
        {
            return;
        }

        array[(int) xy.x, (int) xy.y].SetValue(value);

        // ArrayListEventListener.AddValue?.Invoke(new(x, y), value);
    }

    private Container[] IncreaseLength()
    {
        Container[] prefArray = new Container[x];
        int[] values = new int[x];

        for (int i = 0; i < x; i++)
        {
            prefArray[i] = array[i, 0];
            Destroy(array[i, 0].gameObject);
        }

        //foreach (Container item in array)
        //{
        //    Destroy(item.gameObject);
        //}

        x++;

        array = new Container[x, y];

        GenerateTrains();

        return prefArray;
    }

    public void AddFirst()
    {
        Container[] prefArray = IncreaseLength();

        for (int i = 0; i < x - 1; i++)
        {
            array[i + 1, 0].SetValue(prefArray[i].value);
        }
    }

    public void AddLast()
    {
        Container[] prefArray = IncreaseLength();

        for (int i = 0; i < x - 1; i++)
        {
            array[i, 0].SetValue(prefArray[i].value);
        }
    }

    private void OnDestroy()
    {
        ArrayListEventListener.AddValue -= SetContainerValue;
    }
}
