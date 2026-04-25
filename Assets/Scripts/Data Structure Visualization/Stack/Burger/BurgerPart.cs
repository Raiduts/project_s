using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BurgerPart : MonoBehaviour
{
    [SerializeField]
    private string partName;
    public int id;

    [SerializeField]
    private float transformOffset;
    private int layerIndex;
    
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public string GetName()
    {
        return partName;
    }

    public void SetLayerIndex(int layerIndex)
    {
        this.layerIndex = layerIndex;
        spriteRenderer.sortingLayerID = layerIndex;
    }

    public void FadeIn()
    {
        spriteRenderer.DOFade(1, 0.5f).From(0);
    }

    public void FadeOut(float delay = 0)
    {
        spriteRenderer.DOFade(0, 0.5f).From(1).SetDelay(delay);
    }

    public int GetLayerIndex()
    {
        return layerIndex;
    }

    public float GetTransformOffset()
    {
        return transformOffset;
    }
}
