// Created by Kabourlix Cendrée on 22/11/2023


using UnityEngine;
using UnityEngine.Serialization;

namespace Rezoskour.Content
{
    [CreateAssetMenu(fileName = "DashData", menuName = "Rezoskour/Dash Data", order = 0)]
    public class DashData : ScriptableObject
    {
        public float DashSpeed => dashSpeed;
        public float DashDuration => dashDuration;
        public float DashCooldown => dashCooldown;
        public virtual DashNames DashName => dashName;
        public Sprite Icon => icon;
        [SerializeField] private float dashSpeed = 15f;
        [SerializeField] private float dashDuration = 1f;
        [SerializeField] private float dashCooldown = 0.1f;

        [FormerlySerializedAs("name")] [SerializeField]
        protected DashNames dashName = DashNames.Basic;

        [SerializeField] private Sprite icon = null!;
    }
}