using System.Collections;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ArrayListOperator : MonoBehaviour
{
    public static ArrayListOperator instance;

    public DSType OperatorType;

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

    [SerializeField] private Image[] dimensionButtons;
    [SerializeField] private Sprite toggle1D,toggle2D;

    [SerializeField] private Image buatButton, editButton;

    private bool is2D = true, isOpen = true, isOnAction, isCreating = true;

    public Locomotive GetLocomotive()
    {
        return ArrayListManager.Instance.currentLocomotive;
    }

    public void SetArrayDimension(bool is2DNow)
    {
        is2D = is2DNow;

        for (int i = 0; i < dimensionButtons.Length; i++)
        {
            dimensionButtons[i].sprite = is2D ? toggle2D : toggle1D;
        }

        yLength.gameObject.SetActive(is2DNow);
        yIndex.gameObject.SetActive(is2DNow);
    }

    private void Start()
    {
        instance = this;

        if (OperatorType == DSType.Array)
        {
            createButton.onClick.AddListener(CreateArray);
        }
        else
        {
            createButton.onClick.AddListener(CreateList);
        }

        setValueButton.onClick.AddListener(SetValue);

        SetArrayDimension(false);
    }

    public void CreateArray()
    {
        int x = int.Parse(xLength.text);
        int y = int.Parse(yLength.text);

        x = x == 0 ? 1 : x;
        y = y == 0 || !is2D ? 1 : y;

        EventListener.CreateArray(new(x, y));

        // ArrayListManager.Instance.CreateNewLocomotive(x, y);

        CodePrinter.Instance.AddTextCode($"array.Clear()");

        if (is2D)
        {
            CodePrinter.Instance.AddTextCode($"array = new[{x},{y}]");
        }
        else
        {
            CodePrinter.Instance.AddTextCode($"array = new[{x}]");
        }
    }

    public void AccessByIndex()
    {
        int x = int.Parse(xIndex.text);
        int y = int.Parse(yIndex.text);

        y = !is2D ? 0 : y;

        int value;

        try
        {
            value = GetLocomotive().array[x, y].value;
        }
        catch
        {
            ErrorPopper.Instance.ShowError("index tidak dapat diakses");
            return;
        }

        if (is2D)
        {
            CodePrinter.Instance.AddTextCode($"print(array[{x},{y}])");
        }
        else
        {
            CodePrinter.Instance.AddTextCode($"print(array[{x}])");
        }

        CodePrinter.Instance.AddTextReturn($"return: {value}");
    }

    public void GetSize()
    {
        int length = 0;

        try
        {
            length = GetLocomotive().array.Length;
        }
        catch
        {
            ErrorPopper.Instance.ShowError("Kereta belum dibuat!");
            return;
        }

        if (OperatorType == DSType.Array)
        {
            CodePrinter.Instance.AddTextCode($"print(array.Length)");
        }
        else
        {
            CodePrinter.Instance.AddTextCode($"print(linkedlist.Size())");
        }

        EventListener.GetSize?.Invoke(length);

        CodePrinter.Instance.AddTextReturn($"return: {length}");
    }

    public void IsEmpty()
    {
        bool isEmpty = false;

        EventListener.IsEmpty?.Invoke();

        try
        {
            isEmpty = GetLocomotive().array.Length == 0;
        }
        catch
        {
            isEmpty = true;
            //ErrorPopper.Instance.ShowError("Kereta belum dibuat!");
            //return;
        }

        if (OperatorType == DSType.Array)
        {
            CodePrinter.Instance.AddTextCode("print(array.IsEmpty())");
        }
        else
        {
            CodePrinter.Instance.AddTextCode("print(linkedlist.IsEmpty())");
        }

        CodePrinter.Instance.AddTextReturn($"return: {isEmpty}");
    }

    public void CreateList()
    {
        EventListener.CreateArray(new(0, 1));

        // ArrayListManager.Instance.CreateNewLocomotive(x, y);

        CodePrinter.Instance.AddTextCode($"linkedlist.Clear()");

        CodePrinter.Instance.AddTextCode($"linkedlist = new linkedlist<int>");
    }

    public void SetValue()
    {
        Vector2Int xy = new(int.Parse(xIndex.text), int.Parse(yIndex.text));
        
        if (!GetLocomotive())
        {
            ErrorPopper.Instance.ShowError("Kereta belum dibuat!");
            return;
        }
        else if (xy.x >= GetLocomotive().GetLength() || xy.y >= GetLocomotive().GetHeight())
        {
            ErrorPopper.Instance.ShowError("Indeks tidak dapat diakses");
            return;
        }

        if (!is2D)
        {
            xy.y = 0;
        }

        int value = 0;

        try
        {
            value = int.Parse(valueInput.text);
        }
        catch
        {
            valueInput.text = "0";
            value = 0;
        }

        if (is2D)
        {
            CodePrinter.Instance.AddTextCode($"array[{xy.x},{xy.y}] = {valueInput.text}");
        }
        else
        {
            if (OperatorType == DSType.Array)
            {
                CodePrinter.Instance.AddTextCode($"array[{xy.x}] = {valueInput.text}");
            }
            else
            {
                CodePrinter.Instance.AddTextCode($"traversing...");
            }
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

    public void TraverseInput()
    {
        ArrayListManager.Instance.EditLocomotive(EditType.AddFirst);

        CodePrinter.Instance.AddTextCode("transversal...");
    }

    public void AddFirst()
    {
        if (GetLocomotive() == null || GetLocomotive().array.Length >= 5)
        {
            ErrorPopper.Instance.ShowError("Kereta belum dibuat!");
            return;
        }

        int value = int.Parse(valueInput.text);

        ArrayListManager.Instance.EditLocomotive(EditType.AddFirst, value);

        EventListener.AddFirst?.Invoke(value);

        CodePrinter.Instance.AddTextCode($"linkedlist.AddFirst({value})");
    }

    public void AddLast()
    {
        if (GetLocomotive() == null || GetLocomotive().array.Length >= 5)
        {
            ErrorPopper.Instance.ShowError("Kereta belum dibuat!");
            return;
        }

        int value = int.Parse(valueInput.text);

        ArrayListManager.Instance.EditLocomotive(EditType.AddLast, value);

        EventListener.AddLast?.Invoke(value);

        CodePrinter.Instance.AddTextCode($"linkedlist.AddLast({value})");
    }

    public void RemoveFirst()
    {
        if (GetLocomotive() == null || GetLocomotive().array.Length <= 0)
        {
            ErrorPopper.Instance.ShowError("Kereta belum dibuat!");
            return;
        }

        ArrayListManager.Instance.EditLocomotive(EditType.RemoveFirst);

        EventListener.RemoveFirst?.Invoke();

        CodePrinter.Instance.AddTextCode("linkedlist.RemoveFirst()");
    }

    public void RemoveLast()
    {
        if (GetLocomotive() == null || GetLocomotive().array.Length <= 0)
        {
            ErrorPopper.Instance.ShowError("Kereta belum dibuat!");
            return;
        }

        ArrayListManager.Instance.EditLocomotive(EditType.RemoveLast);

        EventListener.RemoveLast?.Invoke();

        CodePrinter.Instance.AddTextCode("linkedlist.RemoveLast()");
    }

    private IEnumerator ChangeModeWhileOpen()
    {
        buatButton.color = isCreating ? Color.white : Color.gray;
        editButton.color = !isCreating ? Color.white : Color.gray;

        OpenCloseTablet();

        yield return new WaitForSeconds(0.5f);

        createSection.gameObject.SetActive(isCreating);
        setSection.gameObject.SetActive(!isCreating);

        OpenCloseTablet();
    }

    public void ChangeMode(bool isCreate)
    {
        if (isOnAction || isCreating == isCreate)
        {
            return;
        }

        isCreating = isCreate;

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
