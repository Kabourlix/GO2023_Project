// Created by Kabourlix Cendrée on 19/11/2023

#nullable enable

using UnityEngine;

// Created by Kabourlix Cendrée on 19/11/2023

namespace Rezoskour.Content
{
    public class BouncingDash : DashStrategy
    {
        public override float DashSpeed => 15f;
        public override float DashDuration => 1f;
        public override float DashCooldown => 0.1f;

        public BouncingDash(LayerMask _layerMask) : base(_layerMask)
        {
        }

        public override Vector3[] GetTrajectories(Vector2 _origin, Vector2 _direction, float _maxDistance)
        {
            ResetTrajectory(_origin, _direction);
            float remainingDistance = _maxDistance;
            Vector2 start = _origin;
            Vector2 currentDir = _direction;
            while (remainingDistance > 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(start, currentDir, remainingDistance, ~layerMask);
                if (hit.collider)
                {
                    start = hit.point;
                    currentDir = ComputeBouncingDirection(currentDir, hit.normal);
                    Debug.Log($"Adding {start} with direction {currentDir}");
                    Trajectory.Add(start, currentDir);
                    remainingDistance -= hit.distance;
                }
                else
                {
                    Trajectory.Add(start + remainingDistance * currentDir, Vector2.zero);
                    remainingDistance = 0;
                }
            }

            return ConvertTrajectoryToLineRendererPoints(_origin);
        }

        /// <summary>
        /// Provide the new direction when bouncing on a wall using optic laws to get the new direction.
        /// The wall normal shall be directed toward the player.
        /// </summary>
        private Vector2 ComputeBouncingDirection(Vector2 _initDirection, Vector2 _wallNormal)
        {
            Vector2 unitNormal = _wallNormal.normalized;
            return _initDirection - 2 * Vector2.Dot(_initDirection, unitNormal) * unitNormal;
        }
    }
}