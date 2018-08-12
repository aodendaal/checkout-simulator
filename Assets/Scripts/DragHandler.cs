using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject ItemBeingDragged;

    private Transform startParent;
    private Transform panelParent;

    private Transform canvas;

    void Start()
    {
        canvas = GameObject.Find("Canvas").transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ItemBeingDragged = gameObject;

        GetComponent<CanvasGroup>().blocksRaycasts = false;

        transform.SetParent(canvas);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ItemBeingDragged = null;

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}