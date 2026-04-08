using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnEndQuest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        QuestEvent.CompletedLevel += DisableObject;
    }

    public void DisableObject()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        QuestEvent.CompletedLevel -= DisableObject;
    }
}
