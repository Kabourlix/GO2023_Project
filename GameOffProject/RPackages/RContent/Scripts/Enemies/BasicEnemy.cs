// Created by Kabourlix Cendrée on 10/11/2023

#nullable enable

using System;
using SDKabu.KCharacter;
using SDKabu.KCore;
using UnityEngine;
using UnityEngine.InputSystem;

// Created by Kabourlix Cendrée on 10/11/2023

namespace Rezoskour.Content
{
    public class BasicEnemy : Enemy
    {
        public override EnemyType Type => EnemyType.Basic;
        private Transform? playerTransform;

        public override void Initialize(Vector2 _position, Quaternion _rotation, EnemyData? _data = null)
        {
            base.Initialize(_position, _rotation, _data);

            playerTransform = KServiceInjection.Get<IGameManager>()?.PlayerTransform;
        }

        public override void Strategy()
        {
            if (playerTransform == null)
            {
                Debug.Log("Null");
                return;
            }

            MoveTo(playerTransform.position);
        }

        public override void Attack(IKHealth _health)
        {
            //Nothing, rely on collision.
        }

        public override void MoveTo(Vector2 _pos)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                _pos, data.Speed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D _other)
        {
            if (!_other.gameObject.TryGetComponent(out IKActor actor))
            {
                return;
            }

            Attack(actor.HealthComp);
            HealthComp.TakeDamage(100); //TODO : Use a better value
        }
    }
}