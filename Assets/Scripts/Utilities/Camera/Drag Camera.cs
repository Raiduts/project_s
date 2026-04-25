using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DragCamera : MonoBehaviour
{
    public float dragSpeed = 0.01f;
    private Vector3 lastPos;

    public Transform pillar, cloud;
    public LayerMask backgroundLayer;
    private bool isDragging = false;

    public float zoomSpeed = 5f;
    public float minZoom = 3f;
    public float maxZoom = 10f;

    private void Start()
    {
        if (pillar)
        {
            pillar.DOLocalMoveX(0.1f, 3).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);        
        }
        if (cloud)
        {
            cloud?.DOLocalMoveY(0.25f, 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);        
        }
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
    if (Input.GetMouseButtonDown(0))
    {
        if (IsTouchingBackground(Input.mousePosition))
        {
            isDragging = true;
            lastPos = Input.mousePosition;
        }
    }

    if (Input.GetMouseButton(0) && isDragging)
    {
        Vector3 delta = Input.mousePosition - lastPos;
        transform.position += new Vector3(-delta.x, -delta.y, 0f) * dragSpeed;
        lastPos = Input.mousePosition;
    }

    if (Input.GetMouseButtonUp(0))
        isDragging = false;

#else
    if (Input.touchCount == 1)
    {
        Touch t = Input.GetTouch(0);

        if (t.phase == TouchPhase.Began)
        {
            isDragging = IsTouchingBackground(t.position);
        }

        if (t.phase == TouchPhase.Moved && isDragging)
        {
            transform.position += new Vector3(
                -t.deltaPosition.x,
                -t.deltaPosition.y,
                0f) * dragSpeed;
        }

        if (t.phase == TouchPhase.Ended)
            isDragging = false;
    }
#endif
    }

    bool IsTouchingBackground(Vector2 pos)
    {
        return !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }

    public void BackToCenter()
    {
        transform.DOMove(Vector3.zero, 0.5f).SetEase(Ease.OutQuad);
        Camera.main.DOOrthoSize((minZoom + maxZoom) / 2f, 0.5f);
    }

    private void OnDestroy()
    {
        cloud?.DOKill();
        pillar?.DOKill();
    }
}
