// Created by Kabourlix Cendrée on 14/11/2023

#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rezoskour.Content
{
    public class BasicDash : DashStrategy
    {
        /// <inheritdoc />
        public override float DashSpeed => 15f;

        /// <inheritdoc />
        public override float DashDuration => 1f;

        /// <inheritdoc />
        public override float DashCooldown => 0.1f;

        /// <inheritdoc />
        public BasicDash(LayerMask _layerMask) : base(_layerMask)
        {
        }

        public override Vector3[] GetTrajectories(Vector2 _origin, Vector2 _direction, float _maxDistance)
        {
            Trajectory.Clear();
            Trajectory.Add(_origin, _direction);

            RaycastHit2D hit = Physics2D.Raycast(_origin, _direction, _maxDistance, ~layerMask);

            if (hit.collider)
            {
                Trajectory.Add(hit.point, Vector2.zero);
            }
            else
            {
                Trajectory.Add(_origin + _maxDistance * _direction, Vector2.zero);
            }

            Vector3[] traj = Trajectory.Keys.Select(_dummy => (Vector3)(_dummy - _origin)).ToArray();
            traj[0] = Vector3.zero;

            return traj;
        }
    }
}