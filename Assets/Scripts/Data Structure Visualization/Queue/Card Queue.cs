using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CardQueue : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI numberText;
    [SerializeField]
    private SpriteRenderer cardRenderer;
    private int number;

    public int GetNumber()
    {
        return number;
    }

    private void Start()
    {
        number = Random.Range(0, 100);

        numberText.text = number.ToString();
        SlideIn();
    }

    public void SlideIn()
    {
        DoFadeCard(0, 1);
        transform.DOMoveX(transform.position.x + 5, 1).From().SetEase(Ease.OutQuad);
    }

    public void SlideOut()
    {
        DoFadeCard(1, 0);
        transform.DOMoveX(transform.position.x - 10,1).SetEase(Ease.InQuad);
    }

    public void DoFadeCard(float start, float end)
    {
        cardRenderer.DOFade(end, 0.5f).From(start).SetEase(Ease.OutBack).SetDelay(0.5f * start);
        numberText.DOFade(end, 0.5f).From(start).SetEase(Ease.OutBack).SetDelay(0.5f * start);
    }
}
