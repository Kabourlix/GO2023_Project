// Created by Kabourlix Cendrée on 19/11/2023


#nullable enable

using UnityEngine;

// Created by Kabourlix Cendrée on 19/11/2023

namespace Rezoskour.Content
{
    public class BouncingDash : DashStrategy
    {
        public BouncingDash(LayerMask _layerMask, float _playerRadius, DashData _data) : base(_layerMask, _playerRadius,
            _data)
        {
        }

        public override Vector3[] GetTrajectories(Vector2 _origin, Vector2 _direction, float _maxDistance)
        {
            ResetTrajectory(_origin, _direction);
            float remainingDistance = _maxDistance;
            Vector2 start = GetCloseToWall(_origin, _direction);
            Vector2 currentDir = _direction;
            for (int i = 0; i < 10 && remainingDistance > 0; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(start, currentDir, remainingDistance, ~layerMask);
                if (hit.collider)
                {
                    start = GetCloseToWall(hit.point, currentDir);
                    currentDir = ComputeBouncingDirection(currentDir, hit.normal);
                    Debug.Log($"Adding {start} with direction {currentDir}");
                    Trajectory.Add((start, currentDir, hit.distance));
                    remainingDistance -= hit.distance;
                }
                else
                {
                    Trajectory.Add((start + remainingDistance * currentDir, Vector3.zero, remainingDistance));
                    remainingDistance = 0;
                }
            }

            return ConvertTrajectoryToLineRendererPoints(_origin);
        }

        /// <summary>
        ///     Provide the new direction when bouncing on a wall using optic laws to get the new direction.
        ///     The wall normal shall be directed toward the player.
        /// </summary>
        public static Vector2 ComputeBouncingDirection(Vector2 _initDirection, Vector2 _wallNormal)
        {
            Vector2 unitNormal = _wallNormal.normalized;
            return _initDirection - 2 * Vector2.Dot(_initDirection, unitNormal) * unitNormal;
        }
    }
}