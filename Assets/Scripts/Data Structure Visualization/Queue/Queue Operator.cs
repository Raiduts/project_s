using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueOperator : MonoBehaviour
{
    [SerializeField]
    private List<CardQueue> cards;

    private QueueManager queueManager;

    private bool isOperating;

    private void Start()
    {
        queueManager = FindObjectOfType<QueueManager>();

        queueManager.isOperating += CheckIsOperating;
    }

    private void CheckIsOperating(bool isOperating)
    {
        this.isOperating = isOperating;
    }

    public void AddByIndex(int index)
    {
        if (isOperating) return;

        CardQueue temp = Instantiate(cards[index]);

        queueManager.AddCard(temp);

        CodePrinter.Instance.AddTextCode($"queue.Enqueue({temp.GetNumber()})");
    }

    public void TakeCard()
    {
        if (isOperating) return;

        CardQueue temp = queueManager.TakeCard();
        
        CodePrinter.Instance.AddTextCode($"queue.Dequeue()");

        CodePrinter.Instance.AddTextCode($"return : {temp.GetNumber()}");
    }
}
