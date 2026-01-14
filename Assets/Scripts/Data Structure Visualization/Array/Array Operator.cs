using System.Collections;
using System.Collections.Generic;
using System.Security;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArrayOperator : MonoBehaviour
{
    int value;

    [Header("Create")]
    [SerializeField] private GameObject createSection;
    [SerializeField] private TMP_InputField xLength;
    [SerializeField] private TMP_InputField yLength;
    [SerializeField] Button createButton;

    [Header("Set")]
    [SerializeField] private GameObject setSection;
    [SerializeField] private TMP_InputField xIndex;
    [SerializeField] private TMP_InputField yIndex;
    [SerializeField] Button setValueButton;

    private bool is2D = true, isOpen = true, isOnAction, isCreating = true;

    public void SetArrayDimension(bool is2DNow)
    {
        is2D = is2DNow;
        yLength.gameObject.SetActive(is2DNow);
        yIndex.gameObject.SetActive(is2DNow);
    }

    private void Start()
    {
        createButton.onClick.AddListener(CreateArray);
        setValueButton.onClick.AddListener(SetValue);
    }

    public void CreateArray()
    {
        int x = int.Parse(xLength.text);
        int y = int.Parse(yLength.text);

        x = x == 0 ? 1 : x;
        y = y == 0 || !is2D ? 1 : y;

        ArrayManager.Instance.CreateNewLocomotive(x, y);
    }

    public void SetValue()
    {

    }

    public void OpenCloseTablet()
    {
        if (isOnAction)
        {
            return;
        }

        isOnAction = true;

        if (isOpen)
        {
            transform.DOMoveY(-500, 0.5f).SetEase(Ease.InQuad).SetRelative(true).OnComplete(() =>
                {
                    isOnAction = false;
                }
            );
            isOpen = false;
        } else
        {
            transform.DOMoveY(500, 0.5f).SetEase(Ease.InQuad).SetRelative(true).OnComplete(() => 
            {
                isOnAction = false;
            }
            );
            isOpen = true;
        }
    }

    private IEnumerator ChangeModeWhileOpen()
    {
        OpenCloseTablet();

        yield return new WaitForSeconds(0.5f);

        createSection.gameObject.SetActive(isCreating);
        setSection.gameObject.SetActive(!isCreating);

        OpenCloseTablet();
    }

    public void ChangeMode()
    {
        if (isOnAction)
        {
            return;
        }

        isCreating = !isCreating;

        if (isOpen)
        {
            StartCoroutine(ChangeModeWhileOpen());
        }
        else
        {
            createSection.gameObject.SetActive(isCreating);
            setSection.gameObject.SetActive(!isCreating);
        }
    }

    public void ChangeDimension()
    {
        is2D = !is2D;
        SetArrayDimension(is2D);
    }
}
