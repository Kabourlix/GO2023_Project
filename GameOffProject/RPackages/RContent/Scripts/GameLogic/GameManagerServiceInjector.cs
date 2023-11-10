// Created by Kabourlix Cendrée on 11/11/2023

#nullable enable

using System;
using SDKabu.KCore;
using UnityEngine;

namespace Rezoskour.Content
{
    public class GameManagerServiceInjector : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform = null!;

        private void Awake()
        {
            IGameManager manager = new GameManager(playerTransform);
            KServiceInjection.Add<IGameManager>(manager);
        }

        private void OnDestroy()
        {
            KServiceInjection.Remove<IGameManager>();
        }
    }
}