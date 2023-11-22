// Created by Kabourlix Cendrée on 14/11/2023

#nullable enable

using System;
using System.Collections.Generic;
using SDKabu.KCore;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public enum DashNames
{
    Basic,
    Short,
    Long,
    Bouncing,
    Zigzag
}

namespace Rezoskour.Content
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class DashSystem : MonoBehaviour
    {
        public const float TOLERANCE = 0.01f;
        private const string COOLDOWN_ID = "DashSystem";

        [SerializeField] private Camera cam = null!;
        [SerializeField] private LayerMask ignoredMask;

        [FormerlySerializedAs("dashData")] [SerializeField]
        private DashData[] dashDataArray = null!;

        private Dictionary<DashNames, DashData> dashDataDict = new();
        private PlayerInputs Inputs { get; set; } = null!;

        private CircleCollider2D collider = null!;
        public List<DashStrategy> DashList => dashList;
        private readonly List<DashStrategy> dashList = new();
        private int currentDashIndex = 0;
        public int CurrentDashIndex => currentDashIndex;

        public event Action? OnDashEvent;

        private LineRenderer lineRenderer = null!;
        private IKCoolDown? cdSystem;

        private bool wantToDash;
        private bool isDashing;
        private bool isReleaseToBeConsidered;

        private readonly Dictionary<DashNames, DashStrategy> possibleDashes = new();

        private void Awake()
        {
            collider = GetComponent<CircleCollider2D>();
            lineRenderer = GetComponent<LineRenderer>();
            Inputs = new PlayerInputs();
            Inputs.Player.Enable();
            Inputs.Player.Dash.started += DashStartedHandler;
            Inputs.Player.Dash.canceled += DashReleasedHandler;
            Inputs.Player.DashPos.performed += OnDashUpdate;

            foreach (DashData data in dashDataArray)
            {
                dashDataDict.Add(data.DashName, data);
            }
        }

        private void OnDestroy()
        {
            Inputs.Player.Disable();
            Inputs.Player.Dash.started -= DashStartedHandler;
            Inputs.Player.Dash.canceled -= DashReleasedHandler;
        }

        private void Start()
        {
            cdSystem = KServiceInjection.Get<IKCoolDown>();


            possibleDashes.Add(DashNames.Basic,
                new BasicDash(ignoredMask, collider.radius, dashDataDict[DashNames.Basic]));
            possibleDashes.Add(DashNames.Short,
                new BasicDash(ignoredMask, collider.radius, dashDataDict[DashNames.Short]));
            possibleDashes.Add(DashNames.Long,
                new BasicDash(ignoredMask, collider.radius, dashDataDict[DashNames.Long]));
            possibleDashes.Add(DashNames.Bouncing,
                new BouncingDash(ignoredMask, collider.radius, dashDataDict[DashNames.Bouncing]));
            //possibleDashes.Add(DashNames.Zigzag,
            //new BasicDash(ignoredMask, collider.radius, dashDataDict[DashNames.Zigzag]));


            AddDash(DashNames.Basic);
            AddDash(DashNames.Bouncing);
            AddDash(DashNames.Long);
            AddDash(DashNames.Bouncing);
            cdSystem?.TryRegisterCoolDown(COOLDOWN_ID, dashList[currentDashIndex].DashCooldown);
        }

        private void Update()
        {
            if (!isDashing)
            {
                return;
            }

            if (dashList[currentDashIndex].PerformMovement(transform, Time.deltaTime))
            {
                Debug.Log("Finished dashing");
                isDashing = false;
                currentDashIndex = (currentDashIndex + 1) % dashList.Count;
                cdSystem?.UpdateCoolDownDuration(COOLDOWN_ID, dashList[currentDashIndex].DashCooldown, true);
                cdSystem?.StartCoolDown(COOLDOWN_ID, true);
            }
        }

        public void AddDash(DashNames _name, int _indexInList = -1)
        {
            if (!possibleDashes.ContainsKey(_name))
            {
                Debug.LogError("The dash name is not in the list of possible dashes");
                return;
            }

            if (_indexInList >= dashList.Count)
            {
                Debug.LogWarning("Index out of range, the dash is set at the end.");
                _indexInList = -1;
            }

            if (_indexInList <= -1)
            {
                dashList.Add(possibleDashes[_name]);
            }
            else
            {
                dashList.Insert(_indexInList, possibleDashes[_name]);
            }
        }

        public void RemoveDash(int _indexToRemove)
        {
            if (_indexToRemove >= dashList.Count || _indexToRemove < 0)
            {
                Debug.LogError("Index out of range");
                return;
            }

            dashList.RemoveAt(_indexToRemove);
        }

        public void SwapDash(int _indexA, int _indexB)
        {
        }

        #region Inputs callbacks

        private void OnDashUpdate(InputAction.CallbackContext _ctx)
        {
            if (!wantToDash)
            {
                return;
            }

            Vector2 mousePos = _ctx.ReadValue<Vector2>();
            UpdateLineRenderer(mousePos);
            //Debug.Log($"Cursor pos: {mousePos}");
        }

        private void DashStartedHandler(InputAction.CallbackContext _ctx)
        {
            if (isDashing || !(cdSystem?.IsCoolDownFinished(COOLDOWN_ID) ?? true))
            {
                return;
            }

            isReleaseToBeConsidered = true;
            wantToDash = true;
            UpdateLineRenderer(Inputs.Player.DashPos.ReadValue<Vector2>());
        }

        private void DashReleasedHandler(InputAction.CallbackContext _ctx)
        {
            if (isDashing || !isReleaseToBeConsidered)
            {
                return;
            }

            isReleaseToBeConsidered = false;
            wantToDash = false;
            isDashing = true;
            OnDashEvent?.Invoke();
            lineRenderer.positionCount = 0;
        }

        #endregion

        private void UpdateLineRenderer(Vector2 _mousePos)
        {
            Vector2 targetPos = cam.ScreenToWorldPoint(_mousePos);
            Vector2 originPos = transform.position;
            Vector2 direction = (targetPos - originPos).normalized;
            float maxDistance = dashList[currentDashIndex].DashDistance;
            Vector3[] points = dashList[currentDashIndex].GetTrajectories(originPos, direction, maxDistance);
            lineRenderer.positionCount = points.Length;
            lineRenderer.SetPositions(points);
        }
    }
}