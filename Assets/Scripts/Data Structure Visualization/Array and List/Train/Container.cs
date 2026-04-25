using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Container : MonoBehaviour
{
    public int value;
    public TextMeshProUGUI valueText;

    private Sequence seq;

    public void DoPopAnimation()
    {
        seq = DOTween.Sequence();

        seq.Append(transform.DOScale(1.15f, 0.25f).SetEase(Ease.OutBack));
        seq.Append(transform.DOScale(1, 0.25f).SetEase(Ease.OutBack));
    }

    public void SetValue(int newValue)
    {
        value = newValue;
        valueText.text = newValue.ToString();

        //DoPopAnimation();
    }

    public void RandomizeValue()
    {
        int random = Random.Range(0,100);
        SetValue(random);
    }

    private void OnDestroy()
    {
        seq.Kill();
    }
}
