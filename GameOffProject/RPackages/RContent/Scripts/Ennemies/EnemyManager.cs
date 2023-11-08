// Copyright (c) Asobo Studio, All rights reserved. www.asobostudio.com


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
        /// Wether or not enemies are still present in the room.
        /// </summary>
        public bool IsThereEnemy { get; }

        public event Action<IEnemy> OnEnemySpawn;
        public event Action OnEnemiesCleared;

        public IEnemy SpawnEnemy(EnemyType _type, Vector2 _position, Quaternion _rotation);
        public void ReleaseEnemy(IEnemy _enemy);
    }

    internal class EnemyManager : IEnemyManager
    {
        private readonly List<IEnemy> currentEnemies = new();
        /// <inheritdoc />
        public bool IsThereEnemy => currentEnemies.Count == 0;

        /// <inheritdoc />
        public event Action<IEnemy>? OnEnemySpawn;
        /// <inheritdoc />
        public event Action? OnEnemiesCleared;

        private Dictionary<EnemyType, EnemyFactory> enemyFactoryPerType = new();

        public EnemyManager()
        {
            //TODO : Populate the dictionary with each factory
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
        public IEnemy SpawnEnemy(EnemyType _type, Vector2 _position, Quaternion _rotation)
        {
            //Spawn an enemy
            IEnemy enemy = enemyFactoryPerType[_type].Get();

            enemy.Initialize(_position, _rotation);
            currentEnemies.Add(enemy);
            return enemy;
        }

        /// <inheritdoc />
        public void ReleaseEnemy(IEnemy _enemy)
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