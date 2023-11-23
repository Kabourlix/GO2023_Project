// Copyright (c) Asobo Studio, All rights reserved. www.asobostudio.com


#nullable enable

using UnityEngine;

namespace Rezoskour.Content
{
    [CreateAssetMenu(fileName = "ZigZagDashData", menuName = "Rezoskour/ZigZag Dash Data", order = 0)]
    internal class ZigZagDashData : DashData
    {
        /// <inheritdoc />
        public override DashNames DashName => DashNames.Zigzag;
        public int ZigCount => zigCount;
        public float Angle => angleInDegree;
        [SerializeField] private int zigCount = 0;
        [SerializeField] private float angleInDegree = 0;
    }
}