// Created by Kabourlix Cendrée on 14/11/2023

#nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rezoskour.Content
{
    public abstract class DashStrategy
    {
        public const float TOLERANCE = 0.01f;
        public float DashSpeed { get; } = 15f;
        public float DashDuration { get; } = 0.2f;
        public float DashCooldown { get; } = 0.15f;

        public float DashDistance => DashSpeed * DashDuration;

        protected Dictionary<Vector2, Vector2> Trajectory = new();
        protected Queue<Vector2> TrajectoryQueue = new();

        protected Vector2? currentTarget = null;

        public abstract IEnumerator Execute(Vector2 _direction, Rigidbody2D _rb, DashSystem _dashSystem);

        public abstract Vector3[] GetTrajectories(Vector2 _direction, float _maxDistance);

        public void FillQueue()
        {
            TrajectoryQueue.Clear();
            foreach (KeyValuePair<Vector2, Vector2> kv in Trajectory)
            {
                TrajectoryQueue.Enqueue(kv.Key);
            }
        }

        /// <summary>
        /// Return true if the movement is complete.
        /// </summary>
        /// <returns></returns>
        public virtual bool PerformMovement(Transform _player)
        {
            if (currentTarget == null)
            {
                if (TrajectoryQueue.Count == 0)
                {
                    return true;
                }

                currentTarget = TrajectoryQueue.Dequeue();
            }

            Vector2 direction = (currentTarget.Value - (Vector2)_player.position).normalized;
            _player.position += DashSpeed * Time.deltaTime * (Vector3)direction;


            if ((_player.position - (Vector3)currentTarget).magnitude < TOLERANCE)
            {
                currentTarget = null;
            }

            return false;
        }
    }
}