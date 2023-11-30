// Copyrighted by team Rézoskour
// Created by alexandre buzon on 17

using UnityEngine;

namespace Rezoskour.Content.Shop
{
    [CreateAssetMenu(fileName = "ShopItem", menuName = "Rezoskour/Shop Item", order = 0)]
    public abstract class ShopItem : ScriptableObject, IShopItem
    {
        [SerializeField] private string objectName;
        [SerializeField] private string objectDescription;
        [SerializeField] private string objectIconPath;
        [SerializeField] private int price;

        public string ObjectName => objectName;
        public string ObjectDescription => objectDescription;
        public string ObjectIconPath => objectIconPath;
        public int Price => price;
        public bool IsAvailable { get; set; }

        public abstract void Buy();
    }
}