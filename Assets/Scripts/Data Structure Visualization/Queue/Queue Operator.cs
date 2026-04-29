using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueOperator : MonoBehaviour
{
    public static QueueOperator Instance;

    [SerializeField]
    private List<FishQueue> cards;

    private int selectedIndex = -1;

    private QueueManager queueManager;

    private bool isOperating;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        queueManager = FindObjectOfType<QueueManager>();

        queueManager.isOperating += CheckIsOperating;
    }

    private void CheckIsOperating(bool isOperating)
    {
        this.isOperating = isOperating;
    }

    public void SelectByIndex(int index)    
    {
        selectedIndex = index;
    }

    public void BypassEnqueue(int id)
    {
        FishQueue temp = Instantiate(cards[id]);
        queueManager.AddCard(temp);
    }

    public void Enqueue()
    {
        if (isOperating)
        {
            return;
        }

        if (selectedIndex == -1)
        {
            ErrorPopper.Instance.ShowError("Tidak dapat melakukan Enqueue, pilih ikan terlebih dahulu!");
            return;
        }

        FishQueue temp = Instantiate(cards[selectedIndex]);

        queueManager.AddCard(temp);
    }

    public void Dequeue()
    {
        if (isOperating) return;

        FishQueue temp;
        
        try
        {
            temp = queueManager.TakeCard();
        }
        catch
        {
            ErrorPopper.Instance.ShowError("Queue Kosong, tidak dapat melakukan Dequeue!");
            return;
        }

        EventListener.RemoveFirst?.Invoke();

        CodePrinter.Instance.AddTextCode($"queue.Dequeue()");

        CodePrinter.Instance.AddTextCode($"return : {temp.GetName()}");
    }

    public void Peek()
    {
        if (isOperating) return;

        FishQueue temp;

        try
        {
            temp = queueManager.GetFirstFish();
        }
        catch
        {
            ErrorPopper.Instance.ShowError("Queue kosong, tidak dapat melakukan Peek!");
            return;
        }

        EventListener.AccessData?.Invoke();

        CodePrinter.Instance.AddTextCode($"queue.Peek()");

        CodePrinter.Instance.AddTextCode($"return : {temp.GetName()}");
    }

    public void IsEmpty()
    {
        if (isOperating) return;

        bool isEmpty = queueManager.GetQueueSize() == 0;

        CodePrinter.Instance.AddTextCode($"queue.IsEmpty()");

        CodePrinter.Instance.AddTextCode($"return : {isEmpty}");
    }

    public void Size()
    {
        if (isOperating) return;

        int size = queueManager.GetQueueSize();

        EventListener.GetSize?.Invoke(size);

        CodePrinter.Instance.AddTextCode($"queue.GetSize()");

        CodePrinter.Instance.AddTextCode($"return : {size}");
    }
}
