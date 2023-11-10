// Created by Kabourlix Cendrée on 11/11/2023

using System;
using SDKabu.KCore;
using UnityEngine;

namespace Rezoskour.Content
{
    public interface IGameManager : IKService
    {
        public Transform PlayerTransform { get; }
    }

    public class GameManager : IGameManager
    {
        public Transform PlayerTransform { get; }

        public GameManager(Transform _playerTransform)
        {
            PlayerTransform = _playerTransform;
        }

        public void Dispose()
        {
            // TODO release managed resources here
        }
    }
}