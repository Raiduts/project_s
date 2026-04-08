using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsSumonner : MonoBehaviour
{
    [SerializeField] private Tips tipsPref;

    public void SpawnTips()
    {
        Instantiate(tipsPref);
    }
}
