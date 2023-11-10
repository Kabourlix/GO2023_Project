// Created by Kabourlix Cendrée on 10/11/2023

#nullable enable
using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Rezoskour.Content
{
    //We could have a factory per difficulty level or only (play with prefab for this)
    internal abstract class EnemyFactory : IDisposable
    {
        protected ObjectPool<Enemy> enemyPool;
        protected GameObject enemyPrefab;
        protected Transform enemyParent;

        public EnemyFactory(GameObject _enemyPrefab, Transform _enemyParent, int _defaultCapacity = 5,
            int _maxCapacity = 50)
        {
            enemyPrefab = _enemyPrefab;
            enemyParent = _enemyParent;
            enemyPool = new ObjectPool<Enemy>(OnCreateEnemy, OnGetEnemy, OnReleaseEnemy, null, true, _defaultCapacity,
                _maxCapacity);
        }

        public static EnemyFactory? Create(EnemyType _type, GameObject _enemyPrefab, Transform _enemyParent,
            int _defaultCapacity = 5,
            int _maxCapacity = 50)
        {
            switch (_type)
            {
                case EnemyType.Basic:
                    return new BasicEnemyFactory(_enemyPrefab, _defaultCapacity, _maxCapacity, _enemyParent);
                default:
                    Debug.LogError("No factory for this type of enemy");
                    return null;
            }
        }

        public Enemy Get() => enemyPool.Get();
        public void Release(Enemy _enemy) => enemyPool.Release(_enemy);

        protected abstract Enemy OnCreateEnemy();

        protected virtual void OnGetEnemy(Enemy _enemy)
        {
            _enemy.SetActive(true);
        }

        protected virtual void OnReleaseEnemy(Enemy _enemy)
        {
            _enemy.Dispose();
            _enemy.SetActive(false);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            enemyPool.Dispose();
        }
    }
}