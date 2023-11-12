// Created by Kabourlix Cendrée on 12/11/2023

#nullable enable
using SDKabu.KCore;

namespace Rezoskour.Content.Shop
{
    public interface IShop : IKService
    {
        public bool TryBuy(ICurrencyUser _customer, IShopItem _shopItem);
        public IShopItem[] SampleForShop(int _amount);
    }
}