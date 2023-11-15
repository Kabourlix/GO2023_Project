// Created by Kabourlix Cendrée on 14/11/2023

#nullable enable

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Rezoskour.Content.Inputs
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Rezoskour/Events/Input Reader", order = 0)]
    public class InputReader : ScriptableObject, PlayerInputs.IPlayerActions
    {
        public UnityAction<Vector2>? MoveEvent;

        /// <summary>
        /// Event triggered when dash action is performed(false) or released(true).
        /// The parameter could be called _isReleased.
        /// </summary>
        public UnityAction<bool>? DashEvent;

        public UnityAction<Vector2>? DashMoveEvent;

        public Vector2 DashPos => inputs?.Player.DashPos.ReadValue<Vector2>() ?? Vector2.zero;

        private PlayerInputs? inputs = null!;

        private void OnEnable()
        {
            inputs ??= new PlayerInputs();
            inputs.Player.SetCallbacks(this);
            inputs.Player.Enable();
            Debug.Log("Enabled InputReader");
        }

        private void OnDisable()
        {
            Debug.Log("Disabled InputReader");
            inputs?.Player.RemoveCallbacks(this);
            inputs?.Player.Disable();
        }

        public void OnDash(InputAction.CallbackContext _context)
        {
            if (_context.performed)
            {
                DashEvent?.Invoke(false);
                return;
            }

            if (_context.canceled)
            {
                DashEvent?.Invoke(true);
            }
        }

        public void OnDashPos(InputAction.CallbackContext _context)
        {
            if (!_context.performed)
            {
                return;
            }

            DashMoveEvent?.Invoke(_context.ReadValue<Vector2>());
        }
    }
}