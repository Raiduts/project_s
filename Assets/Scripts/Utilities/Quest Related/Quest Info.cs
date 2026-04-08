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

        showPosition = rect.anchoredPosition.x;
        hidePosition = showPosition - 600;

        rect.anchoredPosition = new Vector2(hidePosition, rect.anchoredPosition.y);
    }

    public void ShowQuestInfo(QuestBase currentQuest)
    {
        rect.DOAnchorPosX(showPosition, 0.5f);

        questTitle.text = currentQuest.questTitle;
        questObjective.text = currentQuest.questObjective;
    }

    public void HideQuestInfo()
    {
        rect.DOAnchorPosX(hidePosition, 0.5f);

        questTitle.text = "";
        questObjective.text = "";
    }
}
