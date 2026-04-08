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

        EventListener.AddFirst?.Invoke(0);

        CodePrinter.Instance.AddTextCode($"queue.Enqueue({temp.GetNumber()})");
    }

    public void TakeCard()
    {
        if (isOperating) return;

        CardQueue temp = queueManager.TakeCard();

        EventListener.RemoveFirst?.Invoke();

        CodePrinter.Instance.AddTextCode($"queue.Dequeue()");

        CodePrinter.Instance.AddTextCode($"return : {temp.GetNumber()}");
    }

    public void IsEmpty()
    {
        if (isOperating) return;

        bool isEmpty = queueManager.GetQueueSize() == 0;

        CodePrinter.Instance.AddTextCode($"queue.IsEmpty()");

        CodePrinter.Instance.AddTextCode($"return : {isEmpty}");
    }

    public void GetSize()
    {
        if (isOperating) return;

        int size = queueManager.GetQueueSize();

        EventListener.GetSize?.Invoke(size);

        CodePrinter.Instance.AddTextCode($"queue.GetSize()");

        CodePrinter.Instance.AddTextCode($"return : {size}");
    }
}
