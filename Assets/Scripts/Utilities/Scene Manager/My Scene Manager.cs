using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager instance;

    private LoadingScreen mLoadingScreen;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        mLoadingScreen = LoadingScreen.instance;
    }

    public void ChangeScene(string sceneName)
    {
        mLoadingScreen.DoLoadingScreen(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
