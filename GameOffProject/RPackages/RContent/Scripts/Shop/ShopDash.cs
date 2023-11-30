// Copyrighted by team Rézoskour
// Created by alexandre buzon on 27

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rezoskour.Content
{
    public class ShopDash : MonoBehaviour
    {
        private DashSystem dashSystem;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        [SerializeField] private GameObject inventorySlot;
        [SerializeField] private GameObject draggableItem;
        [SerializeField] private GameObject trashDash;
        private readonly Dictionary<DashNames, Sprite> dashSpriteDict = new();

        private void Awake()
        {
            //get all dashData
            DashData[] dashData = Resources.LoadAll<DashData>("Dash");
            //fill the dictionary
            foreach (DashData dash in dashData)
            {
                dashSpriteDict.Add(dash.DashName, dash.Icon);
            }
        }

        private void Start()
        {
            dashSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<DashSystem>();
            dashSystem.OnDashAdded += UpdateDashList;
            dashSystem.OnDashRemoved += UpdateDashList;
            UpdateDashList();
        }

        private void OnDestroy()
        {
            dashSystem.OnDashAdded -= UpdateDashList;
            dashSystem.OnDashRemoved -= UpdateDashList;
        }

        private void UpdateDashList()
        {
            //Clear all child
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            dashSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<DashSystem>();
            rectTransform.rect.Set(0, 0, dashSystem.DashList.Count * rectTransform.rect.height, 0);
            gridLayoutGroup.cellSize = new Vector2(rectTransform.rect.height, rectTransform.rect.height);
            for (int i = 0; i < dashSystem.DashList.Count; i++)
            {
                GameObject invSlot = Instantiate(inventorySlot, transform);
                GameObject dash = Instantiate(draggableItem, invSlot.transform);
                DraggableItem dragItem = dash.GetComponent<DraggableItem>();
                dragItem.index = i;
                dragItem.parentAfterDrag = transform.parent;
                dash.GetComponent<DraggableItem>().image.sprite = dashSpriteDict[dashSystem.DashList[i].Name];
            }

            DisableTrashDash();
        }

        private void DisableTrashDash()
        {
            if (dashSystem.DashList.Count <= 1)
            {
                trashDash.SetActive(false);
            }
            else
            {
                trashDash.SetActive(true);
            }
        }
    }
}