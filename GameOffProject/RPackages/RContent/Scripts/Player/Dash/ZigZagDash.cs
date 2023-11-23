// Copyright (c) Asobo Studio, All rights reserved. www.asobostudio.com


#nullable enable

using UnityEngine;

namespace Rezoskour.Content
{
    internal class ZigZagDash : DashStrategy
    {
        private readonly int maxZigCount;
        private readonly Quaternion angleUpQuaternion;
        private readonly Quaternion angleDownQuaternion;
        private readonly float reductionFactor;
        private int currentZigCount;

        /// <inheritdoc />
        public ZigZagDash(LayerMask _layerMask, float _playerRadius, ZigZagDashData _data) : base(_layerMask, _playerRadius, _data)
        {
            maxZigCount = _data.ZigCount;
            angleUpQuaternion = Quaternion.Euler(0, 0, _data.Angle);
            angleDownQuaternion = Quaternion.Euler(0, 0, -_data.Angle);
            reductionFactor = 1 / (maxZigCount * Mathf.Cos(_data.Angle * Mathf.Deg2Rad));
        }

        /// <inheritdoc />
        public override Vector3[] GetTrajectories(Vector2 _origin, Vector2 _direction, float _maxDistance)
        {
            Vector2 upDir = angleUpQuaternion * _direction.normalized;
            Vector2 downDir = angleDownQuaternion * _direction.normalized;

            ResetTrajectory(_origin, upDir);

            float distance = _maxDistance * reductionFactor;
            Vector2 start = _origin;
            for (int i = 0; i < maxZigCount; i++)
            {
                Vector2 dir = i % 2 == 0 ? upDir : downDir;
                Vector2 nextDir = i % 2 == 0 ? downDir : upDir;
                RaycastHit2D hit = Physics2D.Raycast(start, dir, distance, ~layerMask);
                if (hit.collider == null)
                {
                    start += distance * dir;
                    Trajectory.Add((start, nextDir, distance));
                    continue;
                }
                start = GetCloseToWall(hit.point, dir);
                Trajectory.Add((start, Vector3.zero, hit.distance));
                break;
            }
            return ConvertTrajectoryToLineRendererPoints(_origin);
        }
    }
}