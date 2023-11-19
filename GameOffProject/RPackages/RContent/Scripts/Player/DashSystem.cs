// Created by Kabourlix Cendrée on 14/11/2023

#nullable enable

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Rezoskour.Content.Inputs;
using SDKabu.KCore;
using UnityEngine;
using UnityEngine.Events;

namespace Rezoskour.Content
{
    public class DashSystem : MonoBehaviour
    {
        public const float TOLERANCE = 0.01f;
        private const string COOLDOWN_ID = "DashSystem";

        [SerializeField] private InputReader inputReader = null!;
        [SerializeField] private Camera cam = null!;
        [SerializeField] private LayerMask ignoredMask;

        private readonly List<DashStrategy> dashList = new();

        //Getter
        public List<DashStrategy> DashList => dashList;

        private int currentDashIndex = 0;

        //Getter
        public int CurrentDashIndex => currentDashIndex;

        public event Action? OnDashEvent;

        private LineRenderer lineRenderer = null!;
        private IKCoolDown? cdSystem;

        private Vector2 lastCursorPos;

        public bool IsDashing { get; set; } = false;
        public bool CanDash { get; set; } = true;
        private bool IsWaitingForRelease { get; set; } = false;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            cdSystem = KServiceInjection.Get<IKCoolDown>();
            inputReader.DashEvent += OnDash;
            inputReader.DashMoveEvent += OnDashUpdate;
            dashList.Add(new BouncingDash(ignoredMask));
            dashList.Add(new BouncingDash(ignoredMask));
            dashList.Add(new BouncingDash(ignoredMask));
            cdSystem?.TryRegisterCoolDown(COOLDOWN_ID, dashList[currentDashIndex].DashCooldown);
        }

        private void Update()
        {
            if (!IsDashing)
            {
                return;
            }

            if (!dashList[currentDashIndex].PerformMovement(transform))
            {
                return;
            }

            cdSystem?.StartCoolDown(COOLDOWN_ID, true);
            IsDashing = false;
            CanDash = true;
            currentDashIndex = (currentDashIndex + 1) % dashList.Count;
            OnDashEvent?.Invoke();
        }

        private void OnDashUpdate(Vector2 _mousePos)
        {
            if (!IsWaitingForRelease || IsDashing) //Use cooldown correctly
            {
                cdSystem?.UpdateCoolDownDuration(COOLDOWN_ID, dashList[currentDashIndex].DashCooldown, true);
                return;
            }

            DashStrategy currentDash = dashList[currentDashIndex];
            Vector2 cursorPos = cam.ScreenToWorldPoint(_mousePos);

            Vector2 direction = (cursorPos - (Vector2)transform.position).normalized;
            Vector3[] trajPoints = currentDash.GetTrajectories(transform.position, direction, currentDash.DashDistance);
            lineRenderer.positionCount = trajPoints.Length;
            lineRenderer.SetPositions(trajPoints);
        }

        private void OnDash(bool _isReleased)
        {
            if (IsDashing || (!cdSystem?.IsCoolDownFinished(COOLDOWN_ID) ?? false))
            {
                return;
            }

            if (!_isReleased)
            {
                IsWaitingForRelease = true;
                lineRenderer.enabled = true;
                OnDashUpdate(inputReader.DashPos);
                return;
            }

            //Perform movement
            lineRenderer.enabled = false;
            lineRenderer.positionCount = 0;

            IsDashing = true;
            IsWaitingForRelease = false;

            dashList[currentDashIndex].FillQueue();
        }
    }
}