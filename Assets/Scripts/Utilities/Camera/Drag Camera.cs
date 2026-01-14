using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCamera : MonoBehaviour
{
    public float dragSpeed = 0.01f;
    private Vector3 lastPos;

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
            lastPos = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastPos;
            transform.position += new Vector3(-delta.x, -delta.y, 0f) * dragSpeed;
            lastPos = Input.mousePosition;
        }
#else
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Moved)
            {
                transform.position += new Vector3(
                    -t.deltaPosition.x,
                    -t.deltaPosition.y,
                    0f) * dragSpeed;
            }
        }
#endif
    }
}
