// Copyrighted by team Rézoskour
// Created by Kabourlix Cendrée on 07

#nullable enable

using System;
using UnityEngine;
using UnityEngine.InputSystem;


    public class PlayerMovement : MonoBehaviour
    {
        private PlayerController input = null!;
        private Vector2 moveVector = Vector2.zero;
        private Rigidbody2D rb = null!;
        private bool facingRight = true;
        public float moveSpeed = 10f;
        private void Awake()
        {
            input = new PlayerController();
            rb = GetComponent<Rigidbody2D>();
        }
        

        private void OnEnable()
        {
            input.Enable();
            
            input.Player.Movement.performed += OnMovementPerformed;
            input.Player.Movement.canceled += OnMovementCanceled;
        }

        private void FixedUpdate()
        {
            rb.velocity = moveVector * moveSpeed;
        }


        private void OnDisable()
        {
            input.Disable();
            input.Player.Movement.performed -= OnMovementPerformed;
            input.Player.Movement.canceled -= OnMovementCanceled;
        }

        

       

        private void OnMovementPerformed(InputAction.CallbackContext _ctx)
        {
            Vector2 readValue = _ctx.ReadValue<Vector2>().normalized;
            moveVector = readValue != Vector2.zero ? readValue : moveVector;
            
        }

        

        private void OnMovementCanceled(InputAction.CallbackContext _ctx)
        {
            moveVector = Vector2.zero;
        }
        
        
    }
