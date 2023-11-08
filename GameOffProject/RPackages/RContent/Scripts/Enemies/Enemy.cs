// Copyright (c) Asobo Studio, All rights reserved. www.asobostudio.com


#nullable enable

using System;
using SDKabu.KCharacter;
using UnityEngine;

namespace Rezoskour.Content
{
    public enum EnemyType
    {
        Basic,
        Test
    }

    public interface IEnemy : IDisposable
    {
        public EnemyType Type { get; }
        public void Initialize(EnemyData _data, Vector2 _position, Quaternion _rotation);
        public void SetActive(bool _isActive);

        public void Attack(IKHealth _health);
    }

    public class Enemy : MonoBehaviour, IEnemy, IKHealth, IKActor
    {
        #region Events
        public event Action<string>? OnActorDie;
        public event Action<string>? OnIncomingDamage;
        public event Action<int>? OnHealthChanged;
        public event Action<int>? OnMaxHealthChanged;
        #endregion

        public EnemyType Type => EnemyType.Basic;
        public Guid ID { get; } = Guid.NewGuid();

        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public float HealthPercentage => (float)Health / MaxHealth;

        public void Initialize(EnemyData _data, Vector2 _position, Quaternion _rotation)
        {
        }

        public void SetActive(bool _isActive) => gameObject.SetActive(_isActive);

        /// <inheritdoc />
        public void Attack(IKHealth _health)
        {
        }

        /// <inheritdoc />
        public void Dispose()
        {
        }

        #region Health
        /// <inheritdoc />
        public void TakeDamage(int _brutAmount, Func<int, int>? _amountModifierFunc = null)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Heal(int _brutAmount, Func<int, int>? _amountModifierFunc = null)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}