using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class FishQueue : MonoBehaviour
{
    [SerializeField]
    private string fishName;
    public int id;

    [SerializeField]
    private SpriteRenderer cardRenderer;

    //private int number;

    public string GetName()
    {
        return fishName;
    }

    private void Start()
    {
        //number = Random.Range(0, 100);

        //numberText.text = number.ToString();
        SlideIn();
    }

    public void SlideIn()
    {
        DoFadeCard(0, 1);
        transform.DOMove(new Vector3(transform.position.x + 4, transform.position.y + 2.5f, 0), 1).From().SetEase(Ease.OutQuad);
    }

    public void SlideOut()
    {
        DoFadeCard(1, 0);
        transform.DOMove(new Vector3(transform.position.x - 4, transform.position.y - 2.5f, 0), 1).SetEase(Ease.InQuad);
    }

    public void DoFadeCard(float start, float end)
    {
        cardRenderer.DOFade(end, 0.5f).From(start).SetEase(Ease.OutBack).SetDelay(0.5f * start);
        //numberText.DOFade(end, 0.5f).From(start).SetEase(Ease.OutBack).SetDelay(0.5f * start);
    }
}
