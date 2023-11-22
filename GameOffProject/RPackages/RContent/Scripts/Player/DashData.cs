// Created by Kabourlix Cendrée on 22/11/2023

using UnityEngine;

namespace Rezoskour.Content
{
    [CreateAssetMenu(fileName = "DashData", menuName = "Rezoskour/Dash Data", order = 0)]
    public class DashData : ScriptableObject
    {
        [SerializeField] private float dashSpeed = 15f;
        [SerializeField] private float dashDuration = 1f;
        [SerializeField] private float dashCooldown = 0.1f;
        [SerializeField] private DashNames name = DashNames.Basic;

        public float DashSpeed => dashSpeed;
        public float DashDuration => dashDuration;
        public float DashCooldown => dashCooldown;
        public DashNames Name => name;
    }
}