// Created by Kabourlix Cendrée on 10/11/2023

#nullable enable
using System;
using System.Collections.Generic;
using SDKabu.KCore;
using UnityEngine;

namespace Rezoskour.Content
{
    internal interface IEnemyManager : IKService
    {
        /// <summary>
        /// Whether or not enemies are still present in the room.
        /// </summary>
        public bool IsThereEnemy { get; }

        public event Action<Enemy>? OnEnemySpawn;
        public event Action? OnEnemiesCleared;

        public Enemy SpawnEnemy(Vector2 _position, Quaternion _rotation, EnemyData? _data = null);
        public void ReleaseEnemy(Enemy _enemy);
    }

    internal class EnemyManager : IEnemyManager
    {
        private readonly List<Enemy> currentEnemies = new();

        /// <inheritdoc />
        public bool IsThereEnemy => currentEnemies.Count == 0;

        /// <inheritdoc />
        public event Action<Enemy>? OnEnemySpawn;

        /// <inheritdoc />
        public event Action? OnEnemiesCleared;

        private Dictionary<EnemyType, EnemyFactory> enemyFactoryPerType = new();

        public EnemyManager(Dictionary<EnemyType, GameObject> _prefabPerType, Transform _enemyParent)
        {
            foreach (KeyValuePair<EnemyType, GameObject> kvPair in _prefabPerType)
            {
                EnemyType type = kvPair.Key;
                GameObject prefab = kvPair.Value;
                EnemyFactory? factory = EnemyFactory.Create(type, prefab, _enemyParent);
                if (factory is null)
                {
                    continue;
                }

                enemyFactoryPerType.Add(type, factory);
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            foreach (KeyValuePair<EnemyType, EnemyFactory> kvPair in enemyFactoryPerType)
            {
                kvPair.Value.Dispose();
            }
        }

        /// <inheritdoc />
        public Enemy SpawnEnemy(Vector2 _position, Quaternion _rotation, EnemyData? _data = null)
        {
            //Spawn an enemy
            Enemy enemy = enemyFactoryPerType[_data.Type].Get();

            enemy.Initialize(_position, _rotation, _data);
            currentEnemies.Add(enemy);
            return enemy;
        }

        /// <inheritdoc />
        public void ReleaseEnemy(Enemy _enemy)
        {
            if (!currentEnemies.Contains(_enemy))
            {
                return;
            }

            _enemy.Dispose();
            currentEnemies.Remove(_enemy);
            enemyFactoryPerType[_enemy.Type].Release(_enemy);
        }
    }
}