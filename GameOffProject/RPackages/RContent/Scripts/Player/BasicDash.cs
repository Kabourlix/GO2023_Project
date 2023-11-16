// Copyright (c) Asobo Studio, All rights reserved. www.asobostudio.com


#nullable enable

using System;
using System.Collections;
using UnityEngine;

namespace Rezoskour.Content
{
    public class BasicDash : DashStrategy
    {
        /// <inheritdoc />
        public override float DashSpeed => 15f;
        /// <inheritdoc />
        public override float DashDuration => 0.2f;
        /// <inheritdoc />
        public override float DashCooldown => 0.1f;

        /// <inheritdoc />
        public BasicDash(LayerMask _layerMask) : base(_layerMask)
        {
        }

        public override Vector3[] GetTrajectories(Vector2 _origin, Vector2 _direction, float _maxDistance)
        {
            RaycastHit2D hit = Physics2D.Raycast(_origin, _direction, _maxDistance, ~layerMask);
            Vector3 origin3D = _origin;
            return hit.collider ? new[] { origin3D, (Vector3)hit.point } : new[] { origin3D, origin3D + _maxDistance * (Vector3)_direction };
        }
    }
}