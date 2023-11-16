// Copyright (c) Asobo Studio, All rights reserved. www.asobostudio.com


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
        [SerializeField] private LayerMask ignoredMask;
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
            dashList.Add(new BasicDash(ignoredMask));
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
            if (!IsControl)
            {
                return;
            }
            Debug.Log("OnDashUpdate");
            DashStrategy currentDash = dashList[index];
            Vector2 cursorPos = cam.ScreenToWorldPoint(_mousePos);
            if ((cursorPos - lastCursorPos).sqrMagnitude < TOLERANCE)
            {
                return;
            }
            lastCursorPos = cursorPos;
            Vector2 direction = (cursorPos - (Vector2)transform.position).normalized;
            Debug.Log($"CurrentDash distance {currentDash.DashDistance}");
            Vector3[] trajPoints = currentDash.GetTrajectories(transform.position, direction, currentDash.DashDistance);
            Debug.Log("TRAJPOINTS");
            foreach (Vector3 point in trajPoints)
            {
                Debug.Log(point);
            }
            Debug.Log("----");
            lineRenderer.SetPositions(trajPoints);
        }

        private void OnDash(bool _isReleased)
        {
            Debug.Log("On Dash"); //Appelé mais ne marche pas.
            if (!_isReleased)
            {
                IsControl = true;
                return;
            }

            //Perform movement
            lineRenderer.positionCount = 0;
            IsDashing = true;
            IsControl = false;
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