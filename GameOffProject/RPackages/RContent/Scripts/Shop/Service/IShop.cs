// Created by Kabourlix Cendrée on 12/11/2023

#nullable enable
using SDKabu.KCore;

namespace Rezoskour.Content.Shop
{
    public interface IShop : IKService
    {
        public bool TryBuy(IObject _object);
        public IObject[] SampleForShop(int _amount);
    }
}