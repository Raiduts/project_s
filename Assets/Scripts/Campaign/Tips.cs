using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] private Image tipsImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TextMeshProUGUI tipsText;
    [SerializeField] private GameObject tipsCanvas;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;

    [SerializeField] private Sprite[] tipsSprites;
    [SerializeField] private string[] tipsTexts;

    private int currentIndex = 0;

    public void OpenTips()
    {
        tipsCanvas.SetActive(true);

        // RESET STATE
        tipsCanvas.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

        backgroundImage.color = new Color(0, 0, 0, 0);
        tipsText.alpha = 0f;
        tipsImage.color = new Color(1, 1, 1, 0);
        closeButton.transform.localScale = Vector3.zero;
        nextButton.transform.localScale = Vector3.zero;
        prevButton.transform.localScale = Vector3.zero;

        tipsImage.sprite = tipsSprites[0];

        // ANIMATION
        Sequence seq = DOTween.Sequence();

        // pop up panel
        seq.Join(tipsCanvas.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack).SetDelay(1.5f));

        // fade background + content
        seq.Join(backgroundImage.DOFade(0.9f, 0.3f));
        seq.Join(tipsText.DOFade(1f, 0.3f));
        seq.Join(tipsImage.DOFade(1f, 0.3f));
        seq.Append(nextButton.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack));
        seq.Join(prevButton.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack));

        // tombol muncul belakangan
        seq.Append(closeButton.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack));
    }

    public void CloseTips()
    {
        closeButton.interactable = false;

        Sequence seq = DOTween.Sequence();

        // fade out semua elemen
        seq.Join(backgroundImage.DOFade(0f, 0.25f));
        seq.Join(tipsText.DOFade(0f, 0.25f));
        seq.Join(tipsImage.DOFade(0f, 0.25f));

        // scale down panel
        seq.Join(tipsCanvas.transform.DOScale(0.8f, 0.25f).SetEase(Ease.InBack));

        // tombol shrink
        seq.Join(closeButton.transform.DOScale(0f, 0.2f).SetEase(Ease.InBack));
        seq.Join(nextButton.transform.DOScale(0f, 0.2f));
        seq.Join(prevButton.transform.DOScale(0f, 0.2f));

        seq.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    public void OnClickNext()
    {
        nextButton.transform.DOPunchScale(Vector3.one * 0.2f, 0.2f, 10, 1);
        NextTips();
    }

    public void OnClickPrev()
    {
        prevButton.transform.DOPunchScale(Vector3.one * 0.2f, 0.2f, 10, 1);
        PrevTips();
    }

    public void NextTips()
    {
        int nextIndex = currentIndex + 1;

        if (nextIndex >= tipsSprites.Length)
            nextIndex = 0; // loop (optional)

        AnimateChange(nextIndex);
    }

    public void PrevTips()
    {
        int prevIndex = currentIndex - 1;

        if (prevIndex < 0)
            prevIndex = tipsSprites.Length - 1;

        AnimateChange(prevIndex);
    }

    private void AnimateChange(int newIndex)
    {
        float dir = newIndex > currentIndex ? 1f : -1f;

        Sequence seq = DOTween.Sequence();

        // keluar (slide + fade)
        seq.Append(tipsImage.rectTransform.DOAnchorPosX(-dir * 100f, 0.2f));
        seq.Join(tipsImage.DOFade(0f, 0.2f));
        seq.Join(tipsText.DOFade(0f, 0.2f));

        // ganti content
        seq.AppendCallback(() =>
        {
            tipsImage.rectTransform.anchoredPosition = new Vector2(dir * 100f, 0);
            ShowTipsByIndex(newIndex);
        });

        // masuk
        seq.Append(tipsImage.rectTransform.DOAnchorPosX(0f, 0.25f).SetEase(Ease.OutCubic));
        seq.Join(tipsImage.DOFade(1f, 0.2f));
        seq.Join(tipsText.DOFade(1f, 0.2f));
    }

    public void ShowTipsByIndex(int index)
    {
        if (tipsSprites.Length == 0) return;

        currentIndex = Mathf.Clamp(index, 0, tipsSprites.Length - 1);

        tipsImage.sprite = tipsSprites[currentIndex];

        if (tipsTexts != null && tipsTexts.Length > currentIndex)
            tipsText.text = tipsTexts[currentIndex];
    }
}