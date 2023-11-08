// Copyright (c) Asobo Studio, All rights reserved. www.asobostudio.com


#nullable enable
using System;
using UnityEngine.Pool;

namespace Rezoskour.Content
{
    internal abstract class EnemyFactory : IDisposable
    {
        protected ObjectPool<IEnemy> enemyPool;

        public EnemyFactory(int _defaultCapacity = 5, int _maxCapacity = 50)
        {
            enemyPool = new ObjectPool<IEnemy>(OnCreateEnemy, OnGetEnemy, OnReleaseEnemy, null, true, _defaultCapacity, _maxCapacity);
        }

        public IEnemy Get() => enemyPool.Get();
        public void Release(IEnemy _enemy) => enemyPool.Release(_enemy);

        protected abstract IEnemy OnCreateEnemy();

        protected virtual void OnGetEnemy(IEnemy _enemy)
        {
            _enemy.SetActive(true);
        }

        protected virtual void OnReleaseEnemy(IEnemy _enemy)
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