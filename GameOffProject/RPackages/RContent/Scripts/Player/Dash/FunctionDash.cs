// Copyright (c) Asobo Studio, All rights reserved. www.asobostudio.com


#nullable enable

using System;
using UnityEngine;

namespace Rezoskour.Content
{
    internal class FunctionDash : DashStrategy
    {
        private Action<float> trajFunction;

        /// <inheritdoc />
        public FunctionDash(LayerMask _layerMask, float _playerRadius, DashData _data, Action<float> _trajFunction) : base(_layerMask, _playerRadius, _data)
        {
            trajFunction = _trajFunction;
        }

        /// <inheritdoc />
        public override Vector3[] GetTrajectories(Vector2 _origin, Vector2 _direction, float _maxDistance)
        {
            //TODO
            return Array.Empty<Vector3>();
        }
    }
}