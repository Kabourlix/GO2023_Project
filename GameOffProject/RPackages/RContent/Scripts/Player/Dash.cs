// Copyrighted by team Rézoskour
// Created by alexandre buzon on 12

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rezoskour.Content
{
    public class Dash : Strategy
    {
        public override IEnumerator Execute(Vector2 _direction, Rigidbody2D _rb, DashManager _dashManager)
        {
            _dashManager.canDash = false;
            _dashManager.isDashing = true;
            _rb.AddForce(new Vector2(_direction.x * dashSpeed, _direction.y * dashSpeed),
                ForceMode2D.Impulse);
            yield return new WaitForSeconds(dashDuration);
            _dashManager.isDashing = false;

            yield return new WaitForSeconds(dashCooldown);
            _dashManager.canDash = true;
        }
    }
}