// Copyrighted by team Rézoskour
// Created by alexandre buzon on 25

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rezoskour.Content
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private bool isTrashBin;
        private DashSystem dashSystem;

        private void Start()
        {
            dashSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<DashSystem>();
        }

        public void OnDrop(PointerEventData _eventData)
        {
            if (isTrashBin)
            {
                dashSystem.RemoveDash(_eventData.pointerDrag.GetComponent<DraggableItem>().index);
                Destroy(_eventData.pointerDrag);
            }
            else
            {
                if (transform.childCount == 0) //if slot is empty (never happens in normal conditions)
                {
                    GameObject dropped = _eventData.pointerDrag;
                    DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
                    draggableItem.parentAfterDrag = transform;
                }
                else if (transform.childCount == 1)
                {
                    GameObject dropped = _eventData.pointerDrag;
                    DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
                    dashSystem.SwapDash(draggableItem.index, transform.GetChild(0).GetComponent<DraggableItem>().index);
                    Transform saveTransform = draggableItem.parentAfterDrag;
                    draggableItem.parentAfterDrag = transform;
                    GameObject child = transform.GetChild(0).gameObject;
                    child.transform.SetParent(saveTransform);
                    child.transform.SetAsLastSibling();
                }
            }
        }
    }
}