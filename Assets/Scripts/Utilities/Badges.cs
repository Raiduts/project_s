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
        arrayBadge.color = UserData.Instance.completedArray ? Color.white : new Color(0,0,0,1f);
        listBadge.color = UserData.Instance.completedLinkedlist ? Color.white : new Color(0, 0, 0, 1f);
        stackBadge.color = UserData.Instance.completedStack ? Color.white : new Color(0, 0, 0, 1f);
        queueBadge.color = UserData.Instance.completedQueue ? Color.white : new Color(0,0,0,1f);

    }
}
