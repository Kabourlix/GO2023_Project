// Copyright (c) Asobo Studio, All rights reserved. www.asobostudio.com


#nullable enable
using System;
using UnityEngine.Pool;

namespace Rezoskour.Content
{
    //We could have a factory per difficulty level or only
    internal abstract class EnemyFactory : IDisposable
    {
        protected ObjectPool<Enemy> enemyPool;

        public EnemyFactory(int _defaultCapacity = 5, int _maxCapacity = 50)
        {
            enemyPool = new ObjectPool<Enemy>(OnCreateEnemy, OnGetEnemy, OnReleaseEnemy, null, true, _defaultCapacity, _maxCapacity);
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