// Copyrighted by team Rézoskour
// Created by alexandre buzon on 25

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rezoskour.Content
{
    public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Image image;
        public int index;

        public Transform parentOnDrag;
        [HideInInspector] public Transform parentAfterDrag;

        public void OnBeginDrag(PointerEventData _eventData)
        {
            Transform tf = transform;
            parentAfterDrag = tf.parent;
            transform.SetParent(parentOnDrag);
            transform.SetAsLastSibling();
            image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData _eventData)
        {
            transform.position = _eventData.position;
        }

        public void OnEndDrag(PointerEventData _eventData)
        {
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
        }
    }
}