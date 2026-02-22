using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CodePrinter : MonoBehaviour
{
    public static CodePrinter Instance;

    [SerializeField] 
    private TextMeshProUGUI codeText;
    [SerializeField]
    private Transform codePanel;
    private int counter;
    private bool hided = false;

    private Vector3 startPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } 
        else 
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        startPosition = codePanel.transform.position;
    }

    public void HideShow()
    {
        if (hided) 
            Show();
        else 
            Hide();

        hided = !hided;
    }

    public void Show()
    {
        codePanel.DOMoveX(startPosition.x - 720, 1);
    }

    public void Hide()
    {
        codePanel.DOMoveX(startPosition.x, 1);
    }

    public void AddTextCode(string text)
    {
        counter++;
        if (counter == 8)
        {
            codeText.alignment = TextAlignmentOptions.BottomLeft;
        }
        codeText.text += $"<system>: {text}\n";
    }
}
