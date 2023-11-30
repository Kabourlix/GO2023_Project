// Copyrighted by team Rézoskour
// Created by alexandre buzon on 12

#nullable enable

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;


namespace Rezoskour.Content.Shop
{
    public class ShopSystem : IShop
    {
        private List<IShopItem> accessibleObjects = new();
        private IShopItem nullShopItem;
        private Random rd = new();

        public ShopSystem(IShopItem _nullShopItem)
        {
            nullShopItem = _nullShopItem;
            ScrapAllObjects();
        }

        public void Dispose()
        {
            // TODO release managed resources here
        }

        public bool TryBuy(ICurrencyUser _customer, IShopItem _shopItem)
        {
            if (!_shopItem.IsAvailable)
            {
                return false;
            }

            return _customer.TrySpendCurrency(_shopItem.Price);
        }

        public IShopItem[] SampleForShop(int _amount)
        {
            //Sample _amount of random objects from accessibleObjects, provide nullObject if not enough objects
            if (accessibleObjects.Count > _amount)
            {
                return accessibleObjects.OrderBy(_item => rd.Next()).Take(_amount).ToArray();
            }

            if (accessibleObjects.Count == _amount)
            {
                return accessibleObjects.ToArray();
            }

            IShopItem[] objects = new IShopItem[_amount];
            for (int i = 0; i < accessibleObjects.Count; i++)
            {
                objects[i] = i >= accessibleObjects.Count ? nullShopItem : accessibleObjects[i];
            }

            return objects;
        }

        private void ScrapAllObjects()
        {
            //TODO Collect all objects from the game
            Resources.LoadAll<ShopItem>("ShopItems").ToList().ForEach(_item => accessibleObjects.Add(_item));
        }
    }
}