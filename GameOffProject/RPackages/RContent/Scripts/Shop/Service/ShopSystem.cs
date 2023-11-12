// Created by Kabourlix Cendrée on 12/11/2023

#nullable enable

using System;

// Created by Kabourlix Cendrée on 12/11/2023

namespace Rezoskour.Content.Shop
{
    public class ShopSystem : IShop
    {
        public void Dispose()
        {
            // TODO release managed resources here
        }

        public bool TryBuy(IObject _object)
        {
            return false;
        }

        public IObject[] SampleForShop(int _amount)
        {
            return Array.Empty<IObject>();
        }
    }
}