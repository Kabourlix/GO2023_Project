// Created by Kabourlix Cendrée on 11/11/2023

using System;
using Rezoskour.Content.Shop;
using SDKabu.KCore;
using UnityEngine;

namespace Rezoskour.Content
{
    public interface IGameManager : IKService
    {
        public Transform PlayerTransform { get; }

        public ICurrencyUser PlayerCurrency { get; }
    }

    public class GameManager : IGameManager
    {
        public Transform PlayerTransform { get; }
        public ICurrencyUser PlayerCurrency { get; }

        public GameManager(Transform _playerTransform)
        {
            PlayerTransform = _playerTransform;
            PlayerCurrency = _playerTransform.GetComponent<PlayerBrain>();
        }

        public void Dispose()
        {
            // TODO release managed resources here
        }
    }
}