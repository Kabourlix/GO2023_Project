// Created by Kabourlix Cendrée on 10/11/2023

#nullable enable
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rezoskour.Content
{
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
        public Enemy SpawnEnemy(EnemyData _data, Vector2 _position, Quaternion _rotation)
        {
            //Spawn an enemy
            Enemy enemy = enemyFactoryPerType[_data.Type].Get();

            enemy.Initialize(_position, _rotation, _data);
            currentEnemies.Add(enemy);
            return enemy;
        }

        /// <inheritdoc />
        public Enemy SpawnEnemy(EnemyType _type, Vector2 _position, Quaternion _rotation)
        {
            //Spawn an enemy
            Enemy enemy = enemyFactoryPerType[_type].Get();

            enemy.Initialize(_position, _rotation);
            currentEnemies.Add(enemy);
            return enemy;
        }

        /// <inheritdoc />
        public void ReleaseEnemy(Enemy _enemy)
        {
            if (!currentEnemies.Contains(_enemy))
            {
                Debug.LogWarning(
                    $"{_enemy.name} (type {_enemy.Type}) is not in the current enemies list. Can't release it.");
                return;
            }

            _enemy.Dispose();
            currentEnemies.Remove(_enemy);
            enemyFactoryPerType[_enemy.Type].Release(_enemy);
        }
    }
}