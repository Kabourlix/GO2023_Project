// Created by Kabourlix Cendrée on 10/11/2023

#nullable enable
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Rezoskour.Content
{
    internal class BasicEnemyFactory : EnemyFactory
    {
        public BasicEnemyFactory(GameObject _enemyPrefab, int _defaultCapacity, int _maxCapacity,
            Transform _enemyParent) :
            base(_enemyPrefab, _enemyParent, _defaultCapacity, _maxCapacity)
        {
        }

        protected override Enemy OnCreateEnemy()
        {
            Enemy? enemy = Object.Instantiate(enemyPrefab, enemyParent).GetComponent<Enemy>();

            return enemy;
        }
    }
}