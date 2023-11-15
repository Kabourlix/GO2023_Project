// Created by Kabourlix Cendrée on 14/11/2023

#nullable enable

using System;
using System.Collections;
using UnityEngine;

namespace Rezoskour.Content
{
    public class BasicDash : DashStrategy
    {
        public override IEnumerator Execute(Vector2 _direction, Rigidbody2D _rb, DashSystem _dashSystem)
        {
            _dashSystem.CanDash = false;
            _dashSystem.IsDashing = true;
            _rb.AddForce(new Vector2(_direction.x * DashSpeed, _direction.y * DashSpeed),
                ForceMode2D.Impulse);
            yield return new WaitForSeconds(DashDuration);
            _dashSystem.IsDashing = false;

            yield return new WaitForSeconds(DashCooldown);
            _dashSystem.CanDash = true;
        }

        public override Vector3[] GetTrajectories(Vector2 _direction, float _maxDistance)
        {
            return Array.Empty<Vector3>();
        }
    }
}