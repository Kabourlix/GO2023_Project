// Copyright (c) Asobo Studio, All rights reserved. www.asobostudio.com


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
        public void Initialize(Vector2 _position, Quaternion _rotation);
        public void SetActive(bool _isActive);
    }

    public class Enemy : MonoBehaviour, IEnemy, IKHealth, IKActor
    {
        public EnemyType Type => EnemyType.Basic;

        public event Action<string> OnActorDie;
        public event Action<string> OnIncomingDamage;
        public event Action<int> OnHealthChanged;
        public event Action<int> OnMaxHealthChanged;
        public Guid ID { get; } = Guid.NewGuid();

        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public float HealthPercentage => (float)Health / MaxHealth;


        public void Initialize(Vector2 _position, Quaternion _rotation)
        {
        }

        public void SetActive(bool _isActive) => gameObject.SetActive(_isActive);

        /// <inheritdoc />
        public void Dispose()
        {
        }

        /// <inheritdoc />
        public void Damage(int _brutAmount, Func<int, int> _amountModifierFunc = null)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Heal(int _brutAmount, Func<int, int> _amountModifierFunc = null)
        {
            throw new NotImplementedException();
        }
    }
}