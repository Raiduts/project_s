using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour
{
    [SerializeField] Transform[] itemButtons;

    public void OnClickButton(int id)
    {
        for (int i = 0; i < itemButtons.Length; i++)
        {
            if (i == id)
            {
                Enlarge(itemButtons[i]);
            }
            else
            {
                ResetSize(itemButtons[i]);
            }
        }
    }

    private void Enlarge(Transform buttonTransform)
    {
        buttonTransform.DOScale(1.15f, 0.25f).SetEase(Ease.OutBack);
    }

    private void ResetSize(Transform buttonTransform)
    {
        buttonTransform.DOScale(1, 0.25f).SetEase(Ease.InBack);
    }
}
