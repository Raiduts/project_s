using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton : MonoBehaviour
{
    public AudioSource audioButton;

    public void OnClick()
    {
        audioButton.Play();
    }
}
