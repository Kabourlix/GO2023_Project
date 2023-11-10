// Created by Kabourlix Cendrée on 10/11/2023

#nullable enable

using SDKabu.KCharacter;
using UnityEngine;
using UnityEngine.InputSystem;

// Created by Kabourlix Cendrée on 10/11/2023

namespace Rezoskour.Content
{
    public class BasicEnemy : Enemy
    {
        public override EnemyType Type => EnemyType.Basic;

        public override void Attack(IKHealth _health)
        {
            //Nothing
        }

        public override void MoveTo(Vector2 _pos)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                _pos, data.Speed * Time.deltaTime);
        }

        public override void Strategy()
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();

            MoveTo(mousePos);
        }

        private void OnCollisionEnter2D(Collision2D _other)
        {
            if (_other.gameObject.TryGetComponent(out IKHealth healthComp))
            {
                Attack(healthComp);
            }
        }
    }
}