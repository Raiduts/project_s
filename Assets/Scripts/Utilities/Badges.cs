using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Badges : MonoBehaviour
{
    [Header("Badges")]
    [SerializeField] private Image arrayBadge, listBadge, stackBadge, queueBadge;
    

    private void Start()
    {
        LoadBadges();
    }

    public void LoadBadges()
    {
        arrayBadge.color = UserData.Instance.completedArray ? Color.white : new Color(33f/255f, 34f/255f, 32f/255f, 1f);
        listBadge.color = UserData.Instance.completedLinkedlist ? Color.white : new Color(33f / 255f, 34f / 255f, 32f / 255f, 1f);
        stackBadge.color = UserData.Instance.completedStack ? Color.white : new Color(33f / 255f, 34f / 255f, 32f / 255f, 1f);
        queueBadge.color = UserData.Instance.completedQueue ? Color.white : new Color(33f / 255f, 34f / 255f, 32f / 255f, 1f);

    }
}
