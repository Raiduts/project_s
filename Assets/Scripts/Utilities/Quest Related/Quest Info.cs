using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class QuestInfo : MonoBehaviour
{
    private RectTransform rect;

    [SerializeField]
    private TextMeshProUGUI questTitle;
    [SerializeField]
    private TextMeshProUGUI questObjective;
    private float showPosition, hidePosition;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();

        // X
        //showPosition = rect.anchoredPosition.x;
        //hidePosition = showPosition - 600;

        // Y
        showPosition = rect.anchoredPosition.y;
        hidePosition = -showPosition;

        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, hidePosition);
    }

    public void ShowQuestInfo(QuestBase currentQuest)
    {
        questTitle.text = currentQuest.questTitle;
        questObjective.text = currentQuest.questObjective;

        //rect.DOAnchorPosX(showPosition, 0.5f);
        rect.DOAnchorPosY(showPosition, 0.5f);
    }

    public void HideQuestInfo(Action action)
    {
        //rect.DOAnchorPosX(hidePosition, 0.5f);
        rect.DOAnchorPosY(hidePosition, 0.5f).OnComplete(() =>
        {
            action?.Invoke();
            questTitle.text = "";
            questObjective.text = "";
        });
    }
}
