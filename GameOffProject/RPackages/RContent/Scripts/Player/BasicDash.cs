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
        public BasicDash(LayerMask _layerMask, float _playerRadius, DashData _data) : base(
            _layerMask, _playerRadius, _data)
        {
        }

        public override Vector3[] GetTrajectories(Vector2 _origin, Vector2 _direction, float _maxDistance)
        {
            ResetTrajectory(_origin, _direction);

            RaycastHit2D hit = Physics2D.Raycast(_origin, _direction, _maxDistance, ~layerMask);

            if (hit.collider)
            {
                Trajectory.Add((GetCloseToWall(hit.point, _direction), Vector3.zero, hit.distance));
            }
            else
            {
                Trajectory.Add((GetCloseToWall(_origin + _maxDistance * _direction, _direction), Vector3.zero,
                    _maxDistance));
            }

            return ConvertTrajectoryToLineRendererPoints(_origin);
        }
    }
}