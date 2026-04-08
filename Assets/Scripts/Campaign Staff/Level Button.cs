using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField]
    private int level = 0;
    [SerializeField]
    private Button button;
    [SerializeField]
    private TextMeshProUGUI textNumber;

    public Action<int> OnSelectLevel;

    private void Start()
    {
        button.onClick.AddListener(OnClickButton);
    }

    public void SetLevel(int level)
    {
        this.level = level;
        textNumber.text = level.ToString();
    }

    public int GetLevel() 
    { 
        return level; 
    }

    private void OnClickButton()
    {
        Select();
        print("Selected Level: " + level);
        OnSelectLevel?.Invoke(level);
    }

    public void Reposition(int pos)
    {
        button.transform.localPosition = new(0, pos, 0);
    }

    public void Select()
    {
        button.transform.DOScale(1.5f, 0.25f).SetEase(Ease.OutBack);
    }

    public void Deselect()
    {
        button.transform.DOScale(1, 0.25f).SetEase(Ease.InBack);
    }
}
