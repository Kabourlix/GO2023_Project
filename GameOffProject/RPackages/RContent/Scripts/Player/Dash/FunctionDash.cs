// Created by Hugo Da Maïa on 23/11/2023

#nullable enable

using System;
using UnityEngine;

namespace Rezoskour.Content
{
    internal class FunctionDash : DashStrategy
    {
        private const int SAMPLE_POINTS = 200;
        private readonly Func<float, float> trajFunction;

        /// <inheritdoc />
        public FunctionDash(LayerMask _layerMask, float _playerRadius, DashData _data, Func<float, float> _trajFunction)
            : base(_layerMask, _playerRadius, _data)
        {
            trajFunction = _trajFunction;
        }

        /// <inheritdoc />
        public override Vector3[] GetTrajectories(Vector2 _origin, Vector2 _direction, float _maxDistance)
        {
            Vector2 ud = _direction.normalized;
            Vector2 un = Quaternion.Euler(0, 0, 90) * ud;
            float deltaD = _maxDistance / (SAMPLE_POINTS - 1);

            Vector2 start = _origin;
            Vector2 dir = Vector2.zero;
            for (int i = 0; i < SAMPLE_POINTS; i++)
            {
                dir = GetDirection((i + 1) * deltaD, ud, un);
                //RaycastHit2D hit = Physics2D.Raycast(start,dir,) Réflechir pour la distance.
            }

            return Array.Empty<Vector3>();
        }

        private Vector2 GetDirection(float _d, Vector2 _dirUnit, Vector2 _normalUnit)
        {
            return _d * _dirUnit + trajFunction.Invoke(_d) * _normalUnit;
        }
    }
}