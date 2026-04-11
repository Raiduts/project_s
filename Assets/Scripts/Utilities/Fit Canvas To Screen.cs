using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitCanvasToScreen : MonoBehaviour
{
    private RectTransform rectTransform;
    // Start is called before the first frame update
    private void Awake()
    {
        RectTransform rect = GetComponent<RectTransform>();

        float targetRatio = 9f / 16f;
        float screenRatio = (float)Screen.width / Screen.height;

        if (screenRatio > targetRatio)
        {
            // layar lebih lebar
            float scale = targetRatio / screenRatio;
            rect.localScale = new Vector3(scale, 1, 1);
        }
        else
        {
            // layar lebih tinggi
            float scale = screenRatio / targetRatio;
            rect.localScale = new Vector3(1, scale, 1);
        }
    }
}
