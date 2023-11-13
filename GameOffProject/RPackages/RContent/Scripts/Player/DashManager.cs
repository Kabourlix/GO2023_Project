// Copyrighted by team Rézoskour
// Created by alexandre buzon on 12

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rezoskour.Content
{
    public class DashManager : MonoBehaviour
    {
        private List<Strategy> dashList = new();
        private int index = 0;
        private Rigidbody2D rb;
        private LineRenderer lr;
        public bool isDashing = false;
        public bool canDash = true;
        private bool isControl = false;
        private Vector2 input;
        private float distance;

        private static PlayerInput playerInput;
        //private Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            playerInput = GetComponent<PlayerInput>();
            lr = GetComponent<LineRenderer>();
            dashList.Add(gameObject.AddComponent<Dash>());
        }

        private void FixedUpdate()
        {
            playerInput.actions["Dash"].started += context =>
            {
                if (canDash)
                {
                    isControl = true;
                }
            };

            if (isControl)
            {
                input = playerInput.actions["Dash"].ReadValue<Vector2>();
                distance = dashList[index].GetDashSpeed() * dashList[index].GetDashDuration();
                lr.SetPosition(1, new Vector3(input.x * distance, input.y * distance, 0));
            }

            playerInput.actions["Dash"].performed += context =>
            {
                if (canDash && isControl)
                {
                    isControl = false;
                    lr.SetPosition(1, new Vector3(0, 0, 0));
                    StartCoroutine(dashList[index]
                        .Execute(new Vector2(input.x * distance, input.y * distance), rb, this));
                }
            };
        }
    }
}