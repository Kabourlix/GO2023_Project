// Copyrighted by team Rézoskour
// Created by alexandre buzon on 27

using System;
using System.Collections;
using System.Collections.Generic;
using Rezoskour.Content.Shop;
using SDKabu.KCore;
using UnityEngine;

namespace Rezoskour.Content
{
    public class CreateShop : MonoBehaviour
    {
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private GameObject ShopDashPrefab;
        [SerializeField] private Transform ShopGridItemTransform;
        [SerializeField] private GameObject shopUI;
        [SerializeField] private GameObject shopButton;

        private void Start()
        {
            IShop shopManager = KServiceInjection.Get<IShop>();
            IShopItem[] items = shopManager.SampleForShop(3);
            foreach (IShopItem item in items)
            {
                if (string.IsNullOrEmpty(item.ObjectDescription))
                {
                    GameObject go = Instantiate(ShopDashPrefab, ShopGridItemTransform);
                    go.GetComponent<ShopItemUI>().SetItem((ShopItem)item);
                }
                else
                {
                    GameObject go = Instantiate(itemPrefab, ShopGridItemTransform);
                    go.GetComponent<ShopItemUI>().SetItem((ShopItem)item);
                }
            }
        }

        public void ShowShop()
        {
            shopButton.SetActive(false);
            shopUI.SetActive(true);
        }

        public void HideShop()
        {
            shopButton.SetActive(true);
            shopUI.SetActive(false);
        }
    }
}