using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackOperator : MonoBehaviour
{
    public static StackOperator Instance;

    private BurgerStack burgerStack;

    [SerializeField]
    private List<BurgerPart> partsPref;
    private BurgerPart selectedPartPref;

    private bool isOperating;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        burgerStack = FindAnyObjectByType<BurgerStack>();

        burgerStack.isOperating += ChangeOnOperating;
    }

    private void ChangeOnOperating(bool isOperating)
    {
        this.isOperating = isOperating;
    }

    public void ChoosePart(int partIndex)
    {
        selectedPartPref = partsPref[partIndex];
    }

    public void PushByType()
    {
        if (isOperating || !selectedPartPref) return;

        BurgerPart tempPart = Instantiate(selectedPartPref, transform);

        burgerStack.PushBurger(tempPart);

        CodePrinter.Instance.AddTextCode($"stack.Push({burgerStack.GetTopPart().GetName()})");
    }

    public void PopBurger()
    {
        if (isOperating || burgerStack.IsEmpty()) return;

        BurgerPart temp = burgerStack.PopBurger();

        CodePrinter.Instance.AddTextCode($"stack.Pop()");

        CodePrinter.Instance.AddTextCode($"return : {temp.GetName()}");
    }

    public void CheckSize()
    {
        CodePrinter.Instance.AddTextCode($"stack.Size()");

        CodePrinter.Instance.AddTextCode($"return : {burgerStack.GetSize()}");
    }

    public void PeekBurger()
    {
        if (burgerStack.IsEmpty()) return;

        CodePrinter.Instance.AddTextCode($"stack.Peek()");

        CodePrinter.Instance.AddTextCode($"return : {burgerStack.GetTopPart().GetName()}");
    }

    public void IsEmpty()
    {
        CodePrinter.Instance.AddTextCode($"stack.IsEmpty()");

        CodePrinter.Instance.AddTextCode($"return : {(burgerStack.IsEmpty()? "true" : "false")}");
    }
}

public enum BurgerPartType
{
    BunAbove,
    Mayo,
    Patties,
    Tomato,
    Lettuce,
    BunBottom,
}
