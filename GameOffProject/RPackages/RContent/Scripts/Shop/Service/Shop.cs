// Created by Kabourlix Cendrée on 12/11/2023

#nullable enable

using SDKabu.KCore;
using UnityEngine;

namespace Rezoskour.Content.Shop
{
    public class Shop : MonoBehaviour
    {
        private void Awake()
        {
            ShopSystem service = new();
            KServiceInjection.Add<IShop>(service);
        }

        private void OnDestroy()
        {
            KServiceInjection.Remove<IShop>();
        }
    }
}