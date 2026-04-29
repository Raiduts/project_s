using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BurgerStack : MonoBehaviour
{
    //public List<BurgerPart> burgerList;
    //public Stack<BurgerPart> burgerTemp = new Stack<BurgerPart>();

    public Transform partsContainer;

    private Stack<BurgerPart> burgerStack = new Stack<BurgerPart>();

    public event Action<bool> isOperating;

    private void Start()
    {

    }

    public void PushBurger(BurgerPart part)
    {
        if (burgerStack.Count >= 9)
        {
            ErrorPopper.Instance.ShowError($"Tidak bisa menambah box lagi, peraturan Developer! (max 9)");
            Destroy(part.gameObject);
            return;
        }

        EventListener.AddFirst?.Invoke(part.id);

        CodePrinter.Instance.AddTextCode($"stack.Push({part.GetName()})");

        isOperating!(true);

        burgerStack.Push(part);

        part.transform.SetParent(partsContainer, false);

        part.transform.position = new(transform.position.x, transform.position.y + (5 * burgerStack.Count), burgerStack.Count * -0.25f);

        part.FadeIn();

        part.transform.DOMoveY(0.75f * burgerStack.Count - 2, 1).SetEase(Ease.OutQuad).OnComplete(()=> 
        {
            isOperating!(false);

            EventListener.Edited?.Invoke(GetIntData());
            //if (burgerTemp.Count == 0)
            //{
            //    PopBurger();
            //}
            //else
            //{
            //    PushBurger(burgerTemp?.Pop());            
            //}
        });
    }
    public BurgerPart PopBurger()
    {
        isOperating!(true);
     
        BurgerPart part = burgerStack.Pop();

        EventListener.Edited?.Invoke(GetIntData());

        part.FadeOut(0.5f); 
        
        part.transform.DOMoveY(part.transform.position.y + 5, 1).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            isOperating!(false);
        });

        return part;
    }

    public int GetSize()
    {
        return burgerStack.Count;
    }

    public BurgerPart GetTopPart()
    {
        return burgerStack.Peek();
    }

    public bool IsEmpty()
    {
        return GetSize() == 0;
    }

    private int[,] GetIntData()
    {
        int[,] data = new int[burgerStack.Count, 1];

        int index = 0;

        foreach (BurgerPart item in burgerStack)
        {
            data[index, 0] = item.id;
            index++;
        }

        return data;
    }
}
