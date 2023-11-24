// Created by Hugo Da Maïa on 23/11/2023

#nullable enable

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rezoskour.Content
{
    public enum TrajFunction
    {
        Sin,
        Cos
    }

    internal class FunctionDash : DashStrategy
    {
        private const int SAMPLE_POINTS = 300;
        private readonly Func<float, float> trajDerivativeFunction;


        private Dictionary<TrajFunction, Func<float, float>> derivativePreTrajFunction = new()
        {
            { TrajFunction.Sin, _x => 2 * Mathf.PI * 0.1f * 0.1f * Mathf.Cos(2 * Mathf.PI * 0.1f * _x) },
            { TrajFunction.Cos, _x => -Mathf.Sin(_x) }
        };

        /// <inheritdoc />
        public FunctionDash(LayerMask _layerMask, float _playerRadius, FuncDashData _data)
            : base(_layerMask, _playerRadius, _data)
        {
            trajDerivativeFunction = derivativePreTrajFunction[_data.TypeOfTraj];
        }

        /// <inheritdoc />
        public override Vector3[] GetTrajectories(Vector2 _origin, Vector2 _direction, float _maxDistance)
        {
            Vector2 ud = _direction.normalized;
            Vector2 un = Quaternion.Euler(0, 0, 90) * ud;
            float deltaD = _maxDistance / (SAMPLE_POINTS - 1);

            Vector2 start = _origin;
            Vector2 dir = GetDirection(0, deltaD, ud, un);
            ResetTrajectory(_origin, dir);
            float remaingDist = _maxDistance;
            for (int i = 0; i < SAMPLE_POINTS; i++)
            {
                float distance = dir.magnitude;
                RaycastHit2D hit = Physics2D.Raycast(start, dir, distance, ~layerMask);
                dir = GetDirection(i + 1, deltaD, ud, un);
                remaingDist -= distance;
                if (hit.collider == null)
                {
                    start += dir;
                    Trajectory.Add((GetCloseToWall(start,dir), dir, distance));
                }
                else
                {
                    Trajectory.Add((GetCloseToWall(hit.point, dir), Vector3.zero, hit.distance));
                    break;
                }

                if (remaingDist <= 0)
                {
                    break;
                }
            }

            return ConvertTrajectoryToLineRendererPoints(_origin);
        }

        private Vector2 GetDirection(int _i, float _delta, Vector2 _dirUnit, Vector2 _normalUnit)
        {
            return _delta * _dirUnit + trajDerivativeFunction.Invoke(_i * _delta) * _normalUnit;
        }
    }
}