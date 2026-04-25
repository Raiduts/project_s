using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizCountdown : MonoBehaviour
{
    public static QuizCountdown Instance;

    [SerializeField] private RectTransform timerPanel;
    [SerializeField] private TextMeshProUGUI timerText;

    public Action StartQuiz;

    private Sequence seq;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

    }

    public void StartCountdown()
    {
        timerText.autoSizeTextContainer = true;

        seq = DOTween.Sequence().SetLink(gameObject);

        for (int i = 3; i >= 1; i--)
        {
            int value = i; // penting biar ga ke-overwrite

            seq.AppendCallback(() =>
            {

                timerText.text = value.ToString();

                // reset scale dulu
                timerText.transform.localScale = Vector3.one;
                timerPanel.localScale = Vector3.one;

                // animasi pop text
                timerText.transform
                    .DOScale(1.3f, 0.2f)
                    .SetEase(Ease.OutBack)
                    .OnComplete(() =>
                    {
                        timerText.transform
                            .DOScale(1f, 0.2f)
                            .SetEase(Ease.InBack);
                    });

                timerPanel
                    .DOScaleY(1.15f, 0.2f)
                    .SetEase(Ease.OutBack)
                    .OnComplete(() =>
                    {
                        timerPanel
                            .DOScaleY(1f, 0.2f)
                            .SetEase(Ease.InBack);
                    });
            });

            seq.AppendInterval(1f); // delay 1 detik tiap angka
        }

        // selesai countdown
        seq.AppendCallback(() =>
        {
            timerText.text = "GO!";

            timerText.transform
                .DOScale(1.5f, 0.3f)
                .SetEase(Ease.OutBack).OnComplete(() =>
                {
                    timerText.DOFade(0, 0.25f);
                });

            timerPanel
                .DOScaleY(7f, 0.75f).OnComplete(() =>
                {
                    timerPanel.GetComponent<Image>().DOFade(0, 0.25f).OnComplete(() =>
                    {
                        Invoke(nameof(EndOfCountdown), 0.5f);
                    });
                });
        });
    }

    private void EndOfCountdown()
    {
        StartQuiz?.Invoke();

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        seq.Kill();   
    }
}
