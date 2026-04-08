using System.Collections;
using System.Collections.Generic;
using System.Security;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ArrayListOperator : MonoBehaviour
{
    public static ArrayListOperator instance;

    private CodePrinter printer;

    [Header("Create")]
    [SerializeField] private GameObject createSection;
    [SerializeField] private TMP_InputField xLength;
    [SerializeField] private TMP_InputField yLength;
    [SerializeField] Button createButton;

    [Header("Set")]
    [SerializeField] private GameObject setSection;
    [SerializeField] private TMP_InputField xIndex;
    [SerializeField] private TMP_InputField yIndex;
    [SerializeField] private TMP_InputField valueInput;
    [SerializeField] Button setValueButton;

    private bool is2D = true, isOpen = true, isOnAction, isCreating = true;

    int x, y, value;

    public void SetArrayDimension(bool is2DNow)
    {
        is2D = is2DNow;
        yLength.gameObject.SetActive(is2DNow);
        yIndex.gameObject.SetActive(is2DNow);
    }

    private void Start()
    {
        instance = this;

        createButton.onClick.AddListener(CreateArray);
        setValueButton.onClick.AddListener(SetValue);

        SetArrayDimension(false);

        printer = CodePrinter.Instance;
    }

    public void CreateArray()
    {
        int x = int.Parse(xLength.text);
        int y = int.Parse(yLength.text);

        //x = x == 0 ? 1 : x;
        y = y == 0 || !is2D ? 1 : y;

        EventListener.CreateArray(new(x, y));

        // ArrayListManager.Instance.CreateNewLocomotive(x, y);

        printer.AddTextCode($"array.Clear()");

        if (is2D)
        {
            printer.AddTextCode($"array = new[{x},{y}]");
        }
        else
        {
            printer.AddTextCode($"array = new[{x}]");
        }
    }

    public void SetValue()
    {
        Vector2Int xy = new(int.Parse(xIndex.text), int.Parse(yIndex.text));

        if (!is2D)
        {
            xy.y = 0;
        }

        int value = int.Parse(valueInput.text);

        if (is2D)
        {
            printer.AddTextCode($"array[{xy.x},{xy.y}] = {valueInput.text}");
        }
        else
        {
            printer.AddTextCode($"array[{xy.x}] = {valueInput.text}");
        }

        EventListener.AddValue(xy, value);

        // ArrayListManager.Instance.currentLocomotive.SetContainerValue(int.Parse(xIndex.text), int.Parse(yIndex.text), int.Parse(valueInput.text));
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
            transform.DOMoveY(-350, 0.5f).SetEase(Ease.InQuad).SetRelative(true).OnComplete(() =>
                {
                    isOnAction = false;
                }
            );
            isOpen = false;
        } else
        {
            transform.DOMoveY(350, 0.5f).SetEase(Ease.InQuad).SetRelative(true).OnComplete(() => 
            {
                isOnAction = false;
            }
            );
            isOpen = true;
        }
    }

    public void AddFirst()
    {
        ArrayListManager.Instance.EditLocomotive(EditType.AddFirst);

        EventListener.AddFirst?.Invoke(0);

        printer.AddTextCode("linkedlist.AddFirst()");
    }

    public void AddLast()
    {
        ArrayListManager.Instance.EditLocomotive(EditType.AddLast);

        EventListener.AddLast?.Invoke(0);

        printer.AddTextCode("linkedlist.AddLast()");
    }

    public void RemoveFirst()
    {
        ArrayListManager.Instance.EditLocomotive(EditType.RemoveFirst);

        EventListener.RemoveFirst?.Invoke();

        printer.AddTextCode("linkedlist.RemoveFirst()");
    }

    public void RemoveLast()
    {
        ArrayListManager.Instance.EditLocomotive(EditType.RemoveLast);

        EventListener.RemoveLast?.Invoke();

        printer.AddTextCode("linkedlist.RemoveLast()");
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

        EventListener.ChangeDimension?.Invoke(is2D? ArrayDimension.TwoDimension : ArrayDimension.OneDimension);
    }
}
