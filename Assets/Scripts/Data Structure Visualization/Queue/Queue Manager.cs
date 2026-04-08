using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    [SerializeField]
    private CardQueue cardQueuePref;

    private Queue<CardQueue> cards = new Queue<CardQueue>();
    
    public event Action<bool> isOperating;

    private void Start()
    {
        
    }

    public void AddCard(CardQueue newCard)
    {
        StartCoroutine(operationCooldown());

        cards.Enqueue(newCard);

        newCard.transform.position = Vector3.zero + new Vector3(cards.Count * 2.5f,0,0);
    }

    public CardQueue TakeCard()
    {
        StartCoroutine(operationCooldown());

        CardQueue temp = cards.Dequeue();

        temp.SlideOut();

        SyncPosition();

        return temp;
    }

    private void SyncPosition()
    {
        foreach (CardQueue item in cards)
        {
            item.transform.DOMoveX(item.transform.position.x - 2.5f, 1).SetDelay(0.5f);
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
}
