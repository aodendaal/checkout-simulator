using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour, IDropHandler
{
    private Transform gridTransform;

    void Start()
    {
        if (GetComponent<GridLayoutGroup>() != null)
        {
            gridTransform = transform;
        }
        else
        {
            gridTransform = transform.Find("Grid").transform;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        DragHandler.ItemBeingDragged.transform.SetParent(gridTransform);
    }
}
