// Created by Kabourlix Cendrée on 30/11/2023

#nullable enable
using System;
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

        public Enemy SpawnEnemy(EnemyData _data, Vector2 _position, Quaternion _rotation);

        /// <summary>
        /// Spawn an enemy by type with its default data.
        /// </summary>
        public Enemy SpawnEnemy(EnemyType _type, Vector2 _position, Quaternion _rotation);

        public void ReleaseEnemy(Enemy _enemy);
    }
}