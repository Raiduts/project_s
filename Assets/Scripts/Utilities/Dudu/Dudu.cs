using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dudu : MonoBehaviour
{
    public static Dudu Instance;

    [SerializeField] private TextMeshProUGUI duduText;
    [SerializeField] private Image duduImage, duduTextPanel, background;
    [SerializeField] private Button skipButton;

    private Sequence duduSequence;
    private bool isShowing;

    public Action OnHidingEvent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        skipButton.onClick.AddListener(HideDudu);
    }

    public void ShowDudu(string duduSpeech, Action actionDudu)
    {
        if (isShowing) return;

        isShowing = true;

        skipButton.gameObject.SetActive(isShowing);
        
        print("Showing dudu");

        duduImage.gameObject.SetActive(true);
        background.gameObject.SetActive(true);

        OnHidingEvent = actionDudu;
        
        duduSequence?.Kill();

        duduSequence = DOTween.Sequence();

        duduSequence.Join(background.DOFade(0.9f, 0.25f));
        duduSequence.Join(duduImage.transform.DOMoveY(-1000, 0.25f).From());
        duduSequence.Join(duduImage.DOFade(1, 0.25f).From(0)); 
        duduSequence.Append(duduTextPanel.DOFade(1, 0.25f).From(0).OnComplete(() => 
            { 
                TextAnimation(duduSpeech);
            }
        ));
    }

    public void TextAnimation(string text)
    {
        duduText.text = "";

        StartCoroutine(TextAnimationIE(text));
    }

    private IEnumerator TextAnimationIE(string text)
    {
        foreach (char item in text)
        {
            duduText.text += item;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(2.5f);

        HideDudu();
    }

    private void HideDudu()
    {
        if (!isShowing) return;

        isShowing = false;

        skipButton.gameObject.SetActive(isShowing);
        
        duduSequence?.Kill();

        duduSequence = DOTween.Sequence();

        duduSequence.Join(duduImage.transform.DOMoveY(-1000, 0.5f).SetDelay(0.5f).OnComplete(() =>
            {
                background.DOFade(0, 0.5f).OnComplete(() =>
                { 
                    background.gameObject.SetActive(false);
                    duduImage.gameObject.SetActive(false);
                    duduImage.transform.position = new(duduImage.transform.position.x, 232, 0);
                    duduText.text = "";
                    OnHidingEvent?.Invoke();
                });
            }
        ));
    }
}
