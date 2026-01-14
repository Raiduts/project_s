using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ArrayManager : MonoBehaviour
{
    public static ArrayManager Instance;
    public Locomotive currentLocomotive, locomotivePref;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateNewLocomotive(int x, int y)
    {
        if (currentLocomotive)
        {
            ThrowOldLocomotive(currentLocomotive);  
        }

        currentLocomotive = Instantiate(locomotivePref);
        currentLocomotive.transform.position = new Vector3(50, 1, 0);
        currentLocomotive.SetLength(x, y);
        currentLocomotive.GenerateTrains();
    }

    public void ThrowOldLocomotive(Locomotive oldLocomotive)
    {
        oldLocomotive.transform.DOLocalMoveX(-500, 5).SetEase(Ease.InQuad);
    }
}
