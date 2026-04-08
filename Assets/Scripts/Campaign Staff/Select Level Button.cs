using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevelButton : MonoBehaviour
{
    private Button button;
    public Action OnStartLevel;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(StartLevel);
    }

    public void StartLevel()
    {
        OnStartLevel?.Invoke();
    }
}
