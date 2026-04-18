using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FpsLocker : MonoBehaviour
{
    [SerializeField]
    private int frameRate;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.debugMode = true;
        Application.targetFrameRate = frameRate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
