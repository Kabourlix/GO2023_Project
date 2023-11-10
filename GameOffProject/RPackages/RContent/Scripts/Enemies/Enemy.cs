// Created by Kabourlix Cendrée on 10/11/2023

#nullable enable

using System;
using SDKabu.KCharacter;
using SDKabu.KCore;
using UnityEngine;

namespace Rezoskour.Content
{
    public enum EnemyType
    {
        Basic,
        Test
    }

    public abstract class Enemy : MonoBehaviour, IDisposable, IKActor
    {
        [SerializeField] private EnemyData defaultData = null!;

        public Guid ID { get; } = Guid.NewGuid(); //REVIEW : Might need to be regenerated on initialization.
        public abstract EnemyType Type { get; }
        public KHealthComp HealthComp { get; private set; } = null!;
        protected EnemyData data = null!;
        protected Collider2D collider = null!;
        private EnemyManager manager = null!;

        protected virtual void Awake()
        {
            collider = GetComponent<Collider2D>();
            data = defaultData;
        }

        protected void Start()
        {
            manager = KServiceInjection.Get<IEnemyManager>() as EnemyManager ??
                      throw new NullReferenceException("No enemy manager found on enemy start");
            Initialize(Vector2.zero, Quaternion.identity);
        }

        protected virtual void Update()
        {
            Strategy();
        }

        public virtual void Initialize(Vector2 _position, Quaternion _rotation, EnemyData? _data = null)
        {
            data = _data == null ? defaultData : _data;
            HealthComp = new KHealthComp(data.MaxHealth, this);

            HealthComp.OnActorDie += HandleDeath;

            gameObject.transform.SetPositionAndRotation(_position, _rotation);
        }

        /// <inheritdoc />
        public virtual void Dispose()
        {
            HealthComp.OnActorDie -= HandleDeath;
            HealthComp.Dispose();
        }

        public void SetActive(bool _isActive) => gameObject.SetActive(_isActive);

        public abstract void Attack(IKHealth _health);

        public abstract void MoveTo(Vector2 _pos);

        public abstract void Strategy();

        protected virtual void HandleDeath(IKActor _actor)
        {
            manager.ReleaseEnemy(this);
        }
    }
}