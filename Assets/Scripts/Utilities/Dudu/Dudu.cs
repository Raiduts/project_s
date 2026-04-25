using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dudu : MonoBehaviour
{
    public static Dudu Instance;

    [SerializeField]
    private TextMeshProUGUI duduText, taskTitleText, taskProgressText;

    [SerializeField]
    private Image duduImage, duduTextPanel, background;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ShowDudu(string duduSpeech)
    {
        print("Showing dudu");

        duduImage.gameObject.SetActive(true);
        background.gameObject.SetActive(true);

        background.DOFade(0.9f, 0.5f);
        duduImage.transform.DOMoveY(-1000, 0.5f).From();
        duduImage.DOFade(1, 0.5f).From(0); 
        duduTextPanel.DOFade(1, 0.5f).From(0).SetDelay(0.5f).OnComplete(() => 
        { 
            TextAnimation(duduSpeech);
        });
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
        // duduImage.DOFade(0, 0.5f).From(1).SetDelay(0.5f);
        // duduTextPanel.DOFade(0, 0.5f).From(1);

        duduImage.transform.DOMoveY(-1000, 0.5f).SetDelay(0.5f).OnComplete(() =>
        {
            background.DOFade(0, 0.5f).OnComplete(() =>
            { 
                background.gameObject.SetActive(false);
                duduImage.gameObject.SetActive(false);
                duduImage.transform.position = new(duduImage.transform.position.x, 232, 0);
                duduText.text = "";
            });
        });
    }
}
