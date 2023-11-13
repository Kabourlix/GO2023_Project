// Copyrighted by team Rézoskour
// Created by alexandre buzon on 12

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rezoskour.Content
{
    public class Strategy : MonoBehaviour
    {
        protected float dashSpeed = 15f;
        protected float dashDuration = 0.2f;
        protected float dashCooldown = 0.15f;

        public virtual IEnumerator Execute(Vector2 _direction, Rigidbody2D _rb, DashManager _dashManager)
        {
            return null;
        }

        public float GetDashSpeed()
        {
            return dashSpeed;
        }

        public float GetDashDuration()
        {
            return dashDuration;
        }

        public float GetDashCooldown()
        {
            return dashCooldown;
        }
    }
}