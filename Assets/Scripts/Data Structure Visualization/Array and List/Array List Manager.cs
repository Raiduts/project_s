using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ArrayListManager : MonoBehaviour
{
    public static ArrayListManager Instance;
    public Locomotive currentLocomotive, locomotivePref;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EventListener.CreateArray += CreateNewLocomotive;
    }

    public void CreateNewLocomotive(Vector2Int index)
    {
        if (currentLocomotive)
        {
            ThrowOldLocomotive(currentLocomotive);  
        }

        currentLocomotive = Instantiate(locomotivePref);
        currentLocomotive.transform.position = new Vector3(50, 1, 0);
        currentLocomotive.SetLength(index.x, index.y);
        currentLocomotive.GenerateTrains();
    }

    public void ThrowOldLocomotive(Locomotive oldLocomotive)
    {
        oldLocomotive.transform.DOLocalMoveX(-500, 5).SetEase(Ease.InQuad).OnComplete(() => 
        { 
            Destroy(oldLocomotive.gameObject); 
        });
    }

    public void EditLocomotive(EditType addType)
    {
        switch (addType)
        {
            case EditType.AddFirst:
                currentLocomotive.AddFirst();
                break;
            case EditType.AddLast:
                currentLocomotive.AddLast();
                break;
            case EditType.RemoveFirst:
                currentLocomotive.RemoveFirst();
                break;
            case EditType.RemoveLast:
                currentLocomotive.RemoveLast();
                break;
        }
    }
}

public enum EditType
{
    AddFirst,
    AddLast,
    RemoveFirst,
    RemoveLast
}
