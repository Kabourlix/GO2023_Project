// Created by Kabourlix Cendrée on 10/11/2023

#nullable enable
using System;
using System.Collections.Generic;
using SDKabu.KCore;
using UnityEngine;

namespace Rezoskour.Content
{
    internal class EnemyManagerServiceInjector : MonoBehaviour
    {
        [SerializeField] private Enemy[] enemies = Array.Empty<Enemy>();

        private void Awake()
        {
            IEnemyManager manager = new EnemyManager(GeneratePrefabPerType());
            KServiceInjection.Add<IEnemyManager>(manager);
        }

        private void OnDestroy()
        {
            KServiceInjection.Remove<IEnemyManager>();
        }

        private Dictionary<EnemyType, GameObject> GeneratePrefabPerType()
        {
            Dictionary<EnemyType, GameObject> prefabPerType = new();
            foreach (Enemy enemy in enemies)
            {
                EnemyType type = enemy.Type;
                GameObject? prefab = enemy.gameObject;
                if (prefabPerType.ContainsKey(type))
                {
                    Debug.LogError($"There is already a prefab for {type}");
                    continue;
                }

                prefabPerType.Add(type, prefab);
            }

            return prefabPerType;
        }
    }
}