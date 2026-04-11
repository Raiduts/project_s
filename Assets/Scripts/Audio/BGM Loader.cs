using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMLoader : MonoBehaviour
{
    [SerializeField] AudioClip bgm;
    [SerializeField] float duration;

    private void Start()
    {
        if (AudioManager.Instance) 
            AudioManager.Instance.PlayBGM(bgm, duration);
    }
}
