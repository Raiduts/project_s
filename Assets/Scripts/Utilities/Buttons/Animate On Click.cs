using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AnimateOnClick : MonoBehaviour
{
    [SerializeField] private OnClickAnimation OnClickAnimation;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlayAnimation);        
    }

    private void PlayAnimation()
    {
        Sequence seq = DOTween.Sequence();

        switch (OnClickAnimation)
        {
            case OnClickAnimation.Bounce:
                seq.Append(transform.DOScale(1.15f, 0.25f).SetEase(Ease.OutBack));
                break;
        }

        seq.Append(transform.DOScale(1, 0.25f));

        seq.Play();
    }
}

public enum OnClickAnimation
{
    Bounce
}
