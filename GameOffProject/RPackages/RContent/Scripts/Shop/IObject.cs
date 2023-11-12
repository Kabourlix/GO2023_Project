// Created by Kabourlix Cendrée on 11/11/2023

using System;

namespace Rezoskour.Content.Shop
{
    public interface IObject
    {
        public string ObjectName { get; }
        public string ObjectDescription { get; }
        public string ObjectIconPath { get; }
        public int Price { get; }
    }
}