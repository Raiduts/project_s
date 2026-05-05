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
    [SerializeField]
    private Image Locked, Ring;

    public Action<int> OnSelectLevel;

    private void Awake()
    {
        Ring.gameObject.SetActive(false);
    }

    private void Start()
    {
        button.onClick.AddListener(OnClickButton);
    }

    public void SetLevel(int level)
    {
        this.level = level;
        textNumber.text = level.ToString();
    }

    public void SetInteractable(bool isInteractable)
    {
        button.interactable = isInteractable;
        Locked.gameObject.SetActive(!isInteractable);
    }

    public int GetLevel() 
    { 
        return level; 
    }

    public void OnClickButton()
    {
        Select();
        //print("Selected Level: " + level);
        OnSelectLevel?.Invoke(level);
    }

    public void Reposition(int pos)
    {
        button.transform.localPosition = new(0, pos, 0);
        Ring.transform.localPosition = new(0, pos, 0);
    }

    public void Select()
    {
        Ring.gameObject.SetActive(true);
        Ring.DOFade(1, 0.25f);
        button.transform.DOScale(1.5f, 0.25f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            DashboardMarker.Instance.MoveToTarget(button.GetComponent<RectTransform>());
        });
    }

    public void Deselect()
    {
        Ring.DOFade(0, 0.25f);
        Ring.gameObject.SetActive(false);
        button.transform.DOScale(1, 0.25f).SetEase(Ease.InBack);
    }
}
