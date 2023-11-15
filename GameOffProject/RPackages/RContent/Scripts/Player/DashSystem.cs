// Created by Kabourlix Cendrée on 14/11/2023

using System;
using System.Collections.Generic;
using Rezoskour.Content.Inputs;
using UnityEngine;

namespace Rezoskour.Content
{
    public class DashSystem : MonoBehaviour
    {
        public const float TOLERANCE = 0.01f;

        [SerializeField] private InputReader inputReader;
        [SerializeField] private Camera cam;
        private List<DashStrategy> dashList = new();
        private int index = 0;

        private LineRenderer lineRenderer;

        private Vector2 lastCursorPos;

        public bool IsDashing { get; set; } = false;
        public bool CanDash { get; set; } = true;
        private bool IsControl { get; set; } = false;
        private Vector2 input;
        private float distance;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            inputReader.DashEvent += OnDash;
            inputReader.DashMoveEvent += OnDashUpdate;
            dashList.Add(new BasicDash());
        }

        private void Update()
        {
            if (!IsDashing)
            {
                return;
            }

            if (!dashList[index].PerformMovement(transform))
            {
                return;
            }

            IsDashing = false;
            CanDash = true;
            index = (index + 1) % dashList.Count;
        }

        private void OnDashUpdate(Vector2 _mousePos)
        {
            DashStrategy currentDash = dashList[index];
            Vector2 cursorPos = cam.ScreenToWorldPoint(_mousePos);
            if ((cursorPos - lastCursorPos).sqrMagnitude < TOLERANCE)
            {
                return;
            }

            Vector2 direction = (cursorPos - (Vector2)transform.position).normalized;
            Vector3[] trajPoints = currentDash.GetTrajectories(direction, currentDash.DashDistance);
            lineRenderer.SetPositions(trajPoints);
        }

        private void OnDash(bool _isReleased)
        {
            Debug.Log("On Dash"); //Appelé mais ne marche pas.
            if (!_isReleased)
            {
                return;
            }

            //Perform movement
            IsDashing = true;
            dashList[index].FillQueue();
        }

        // private void FixedUpdate()
        // {
        //     playerInput.actions["Dash"].started += context =>
        //     {
        //         if (canDash)
        //         {
        //             isControl = true;
        //         }
        //     };
        //
        //     if (isControl)
        //     {
        //         input = playerInput.actions["Dash"].ReadValue<Vector2>();
        //         distance = dashList[index].DashSpeed * dashList[index].DashDuration;
        //         lineRenderer.SetPosition(1, new Vector3(input.x * distance, input.y * distance, 0));
        //     }
        //
        //     playerInput.actions["Dash"].performed += context =>
        //     {
        //         if (canDash && isControl)
        //         {
        //             isControl = false;
        //             lineRenderer.SetPosition(1, new Vector3(0, 0, 0));
        //             StartCoroutine(dashList[index]
        //                 .Execute(new Vector2(input.x * distance, input.y * distance), rb, this));
        //         }
        //     };
        // }
    }
}