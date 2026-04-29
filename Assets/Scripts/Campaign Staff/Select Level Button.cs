using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelButton : MonoBehaviour
{
    public Button button;
    public Action OnStartLevel;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(StartLevel);

        button.interactable = false;
    }

    public void StartLevel()
    {
        OnStartLevel?.Invoke();
    }
}
