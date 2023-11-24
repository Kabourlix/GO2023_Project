// Created by Hugo Da Maïa on 16/11/2023

#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rezoskour.Content
{
    public abstract class DashStrategy : IDisposable
    {
        public const float TOLERANCE = 0.1f;
        public float DashSpeed { get; }
        public float DashDuration { get; }
        public float DashCooldown { get; }
        public DashNames Name { get; }

        protected readonly float playerRadius;

        public float DashDistance => DashSpeed * DashDuration;

        protected List<(Vector3, Vector3, float)> Trajectory { get; } = new();
        protected int currentTrajectoryIndex = 0;
        protected Vector2? currentTarget = null;

        protected LayerMask layerMask;

        private float distanceDone;

        protected DashStrategy(LayerMask _layerMask, float _playerRadius, DashData _data)
        {
            layerMask = _layerMask;
            playerRadius = _playerRadius;
            DashSpeed = _data.DashSpeed;
            DashDuration = _data.DashDuration;
            DashCooldown = _data.DashCooldown;
            Name = _data.DashName;
        }

        public void Dispose()
        {
            Trajectory.Clear();
            currentTrajectoryIndex = 0;
            currentTarget = null;
        }

        public abstract Vector3[] GetTrajectories(Vector2 _origin, Vector2 _direction, float _maxDistance);


        /// <summary>
        /// Return true if the movement is complete.
        /// </summary>
        /// <returns></returns>
        public bool PerformMovement(Transform _player, float _deltaTime)
        {
            if (currentTarget == null)
            {
                if (currentTrajectoryIndex >= Trajectory.Count)
                {
                    return true;
                }

                currentTarget = Trajectory[currentTrajectoryIndex].Item1;
                distanceDone = 0;
            }

            Vector2 direction = Trajectory[currentTrajectoryIndex - 1].Item2;
            distanceDone += DashSpeed * _deltaTime;
            _player.position = Vector3.MoveTowards(_player.position, currentTarget.Value, DashSpeed * _deltaTime);
            //_player.position += DashSpeed * _deltaTime * (Vector3)direction;


            if (distanceDone >= Trajectory[currentTrajectoryIndex].Item3)
            {
                currentTarget = null;
                currentTrajectoryIndex++;
                distanceDone = 0;
            }

            return false;
        }

        protected void ResetTrajectory(Vector2 _origin, Vector2 _direction)
        {
            Trajectory.Clear();
            currentTrajectoryIndex = 1;
            Trajectory.Add((_origin, _direction, 0));
        }

        protected Vector3[] ConvertTrajectoryToLineRendererPoints(Vector2 _origin)
        {
            Vector3[] traj = new Vector3[Trajectory.Count];
            for (int i = 0; i < Trajectory.Count; i++)
            {
                traj[i] = Trajectory[i].Item1 - (Vector3)_origin;
            }

            traj[0] = Vector3.zero;
            return traj;
        }

        protected Vector3 GetCloseToWall(Vector3 _wallHit, Vector3 _direction)
        {
            return _wallHit - playerRadius * _direction;
        }
    }
}