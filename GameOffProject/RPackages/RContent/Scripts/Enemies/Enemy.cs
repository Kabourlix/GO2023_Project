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

    public abstract class Enemy : MonoBehaviour, IDisposable, IKActor, IKHealth
    {
        #region Events
        /// <inheritdoc />
        public event Action<IKActor>? OnActorDie;
        /// <inheritdoc />
        public event Action<IKActor>? OnIncomingDamage;
        /// <inheritdoc />
        public event Action<int>? OnHealthChanged;
        /// <inheritdoc />
        public event Action<int>? OnMaxHealthChanged;
        #endregion

        public Guid ID => Guid.NewGuid(); //REVIEW : Might need to be regenerated on initialization.
        public abstract EnemyType Type { get; }

        public int Health { get; protected set; }
        public int MaxHealth { get; protected set; }
        public float HealthPercentage => (float)Health / MaxHealth;

        protected EnemyData? data = null;

        public virtual void Initialize(EnemyData _data, Vector2 _position, Quaternion _rotation)
        {
            data = _data;
            Health = data.MaxHealth;
            MaxHealth = data.MaxHealth;

            gameObject.transform.SetPositionAndRotation(_position, _rotation);
        }

        /// <inheritdoc />
        public virtual void Dispose()
        {
            data = null;
            Health = 0;
            MaxHealth = 0;
        }

        public void SetActive(bool _isActive) => gameObject.SetActive(_isActive);

        public abstract void Attack(IKHealth _health);

        #region Health
        /// <inheritdoc />
        public void Heal(int _brutAmount, Func<int, int>? _amountModifierFunc = null)
        {
            int newAmount = _brutAmount;
            if (_amountModifierFunc != null)
            {
                newAmount = _amountModifierFunc(_brutAmount);
            }
            //TODO : To be refactored.
        }

        /// <inheritdoc />
        public void TakeDamage(int _brutAmount, Func<int, int>? _amountModifierFunc = null)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    public class BasicEnemy : Enemy
    {
        /// <inheritdoc />
        public override EnemyType Type => EnemyType.Basic;

        /// <inheritdoc />
        public override void Attack(IKHealth _health)
        {
            throw new NotImplementedException();
        }
    }
}