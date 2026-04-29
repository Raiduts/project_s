using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    private Queue<FishQueue> cards = new Queue<FishQueue>();

    [SerializeField] private Transform queueContainer;
    
    public event Action<bool> isOperating;

    public void AddCard(FishQueue newCard)
    {
        if (cards.Count >= 5)
        {
            ErrorPopper.Instance.ShowError("Tidak bisa menabmah ikan lagi, peraturan Developer! (max 5)");
            Destroy(newCard.gameObject);
            return;
        }

        CodePrinter.Instance.AddTextCode($"queue.Enqueue({newCard.GetName()})");

        StartCoroutine(operationCooldown());

        cards.Enqueue(newCard);

        EventListener.Edited?.Invoke(GetIntData());

        EventListener.AddFirst?.Invoke(newCard.id);

        newCard.transform.parent = queueContainer;
        newCard.transform.localPosition = Vector3.zero + new Vector3(cards.Count * 2, cards.Count * 1.25f,0);
    }

    public FishQueue TakeCard()
    {
        StartCoroutine(operationCooldown());

        FishQueue temp = cards.Dequeue();

        temp.SlideOut();

        SyncPosition();

        EventListener.Edited?.Invoke(GetIntData());

        return temp;
    }

    private void SyncPosition()
    {
        int i = 0;

        foreach (FishQueue item in cards)
        {
            i++;
            item.transform.DOLocalMove(new Vector3(i * 2, i * 1.25f, 0), 1).SetDelay(0.5f);
            //item.transform.DOMove(new Vector3(item.transform.position.x - 2, item.transform.position.y - 1.25f, 0), 1).SetDelay(0.5f);
        }
    }

    private IEnumerator operationCooldown()
    {
        isOperating?.Invoke(true);

        yield return new WaitForSeconds(1f);

        isOperating?.Invoke(false);
    }

    public int GetQueueSize()
    {
        return cards.Count;
    }

    public FishQueue GetFirstFish()
    {
        return cards.Peek();
    }

    private int[,] GetIntData()
    {
        int[,] data = new int[cards.Count, 1];

        int index = 0;

        foreach (FishQueue item in cards)
        {
            data[index, 0] = item.id;
            index++;
        }

        return data;
    }
}
