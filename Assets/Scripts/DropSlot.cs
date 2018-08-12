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
            var grid = transform.Find("Grid");

            if (grid != null)
            {
                gridTransform = grid.transform;
            }
            else
            {
                gridTransform = transform;
            }
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        DragHandler.ItemBeingDragged.transform.SetParent(gridTransform);
    }
}
