// Created by Kabourlix Cendrée on 10/11/2023

#nullable enable

using SDKabu.KCharacter;
using UnityEngine;

// Created by Kabourlix Cendrée on 10/11/2023

namespace Rezoskour.Content
{
    public class BasicEnemy : Enemy
    {
        public override EnemyType Type => EnemyType.Basic;

        public override void Attack(IKHealth _health)
        {
            throw new System.NotImplementedException();
        }

        public override void MoveTo(Vector2 _pos)
        {
            throw new System.NotImplementedException();
        }

        public override void Strategy()
        {
            throw new System.NotImplementedException();
        }
    }
}