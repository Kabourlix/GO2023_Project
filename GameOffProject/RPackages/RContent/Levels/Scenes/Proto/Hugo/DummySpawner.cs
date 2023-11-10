// Created by Kabourlix Cendrée on 11/11/2023

#nullable enable

using System;
using SDKabu.KCore;
using UnityEngine;

namespace Rezoskour.Content.Levels.Scenes.Proto.Hugo
{
    public class DummySpawner : MonoBehaviour
    {
        private IEnemyManager? manager;

        private void Start()
        {
            manager = KServiceInjection.Get<IEnemyManager>();
            if (manager == null)
            {
                Debug.LogError("No enemy manager found");
                return;
            }

            manager.SpawnEnemy(EnemyType.Basic, Vector2.zero, Quaternion.identity);
        }
    }
}