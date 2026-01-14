using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;   

    [SerializeField] private Image backgoundImage;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void DoLoadingScreen(string newScene = "None")
    {
        backgoundImage.raycastTarget = true;
        backgoundImage.DOFade(1, 0.5f).OnComplete(() =>
        {
            if (newScene != "None")
            {
                SceneManager.LoadScene(newScene);
            }
        });

    }
}
