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
        isOperating!(true);

        float prefPosition = burgerStack.Count > 0? burgerStack.Peek().transform.position.y : transform.position.y;

        burgerStack.Push(part);

        part.transform.SetParent(partsContainer, false);

        Vector3 spawnPosition = new(transform.position.x, prefPosition + 5, burgerStack.Count * -0.25f);

        part.transform.position = spawnPosition;

        part.FadeIn();

        part.transform.DOMoveY(prefPosition + part.GetTransformOffset() + 0.5f, 1).SetEase(Ease.OutQuad).OnComplete(()=> 
        {
            isOperating!(false);

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

        part.FadeOut(0.5f); 
        
        part.transform.DOMoveY(part.transform.position.y + 5, 1).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            isOperating!(false);

            // Destroy(part.gameObject);
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
}
