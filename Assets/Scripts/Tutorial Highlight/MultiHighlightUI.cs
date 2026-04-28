using UnityEngine;
using System.Collections.Generic;

public class MultiHighlightUI : MonoBehaviour
{
    public Material material;
    public Canvas canvas;

    [SerializeField]
    public List<RectTransform> targets = new List<RectTransform>();

    public float radius = 0.1f;

    private void Start()
    {
        
    }

    void Update()
    {
        UpdateTargets();
    }

    void UpdateTargets()
    {
        int count = Mathf.Min(targets.Count, 10);

        Vector4[] data = new Vector4[10];

        for (int i = 0; i < count; i++)
        {
            Vector2 viewportPos = GetViewportPosition(targets[i]);
            data[i] = new Vector4(viewportPos.x, viewportPos.y, radius, 0);
        }

        material.SetInt("_TargetCount", count);
        material.SetVectorArray("_Targets", data);
    }

    Vector2 GetViewportPosition(RectTransform target)
    {
        Vector3 worldPos = target.position;
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(null, worldPos);

        return new Vector2(screenPos.x / Screen.width, screenPos.y / Screen.height);
    }
}