// Copyright (c) Asobo Studio, All rights reserved. www.asobostudio.com


#nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rezoskour.Content
{
    public abstract class DashStrategy
    {
        public const float TOLERANCE = 0.01f;
        public abstract float DashSpeed { get; }
        public abstract float DashDuration { get; }
        public abstract float DashCooldown { get; }

        public float DashDistance => DashSpeed * DashDuration;

        protected Dictionary<Vector2, Vector2> Trajectory = new();
        protected Queue<Vector2> TrajectoryQueue = new();

        protected Vector2? currentTarget = null;

        protected LayerMask layerMask;

        public DashStrategy(LayerMask _layerMask)
        {
            layerMask = _layerMask;
        }

        public abstract Vector3[] GetTrajectories(Vector2 _origin, Vector2 _direction, float _maxDistance);

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